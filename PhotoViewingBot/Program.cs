using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace PhotoViewingBot
{
    class Program
    {

        static ITelegramBotClient bot;

        public static void CreateBot(BotSettings botSettings)
        {
            bot = new TelegramBotClient(botSettings.Token);
        }
        public static BotSettings GetBotSettings()
        {
            using (StreamReader fs = new StreamReader("botsettings.json"))
            {
                string output = fs.ReadToEnd();

                return JsonConvert.DeserializeObject<BotSettings>(output);
            }
        }

        public static string GetTag(Message message)
        {
            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (message.Caption == null || message.Caption == "")
                {
                    return DateTime.Now.ToUniversalTime().ToString();
                }
                else
                {
                    return message.Caption.ToLower();
                }
            }
            else
            {
                throw new ArgumentException($"Message {message} type is not a photo");
            }
        }

        public static bool IsTagExsist(string tagName)
        {
            using(PhotoViewingDBContext db = new PhotoViewingDBContext())
            {
                Tag tag = db.Tags.FirstOrDefault(t => t.Name == tagName);
                if (tag != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public static async Task<string> GetPhotoDataAsync(Message message, ITelegramBotClient bot, CancellationToken cancellationToken)
        {
            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                var fileInfo = await bot.GetFileAsync(message.Photo[message.Photo.Length - 1].FileId, cancellationToken);
                MemoryStream stream = new MemoryStream();
                await bot.DownloadFileAsync(fileInfo.FilePath, stream, cancellationToken);
                string data = Convert.ToBase64String(stream.ToArray());
                stream.Close();
                return data;
            }
            else
            {
                throw new ArgumentException($"Message {message} type is not a photo");
            }
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                {
                    if (message.Text.ToLower() == "/start")
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                        return;
                    }
                    await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
                }
                else
                {
                    using (PhotoViewingDBContext db = new PhotoViewingDBContext())
                    {
                        string tagName = GetTag(message);
                        Tag tag = new Tag();

                        if (IsTagExsist(tagName))
                        {
                            tag = db.Tags.FirstOrDefault(t => t.Name == tagName);
                        }
                        else
                        {
                            tag = new Tag { Name = tagName };
                            db.Tags.Add(tag);
                            db.SaveChanges();
                        }

                        string photoData = await GetPhotoDataAsync(message, bot, cancellationToken);
                        Photo photo = new Photo { PhotoData = photoData, Tag = tag, TagId = tag.Id };
                        db.Photos.Add(photo);
                        db.SaveChanges();
                    }
                }
                
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine("------------------------\n" + Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            BotSettings botSettings = GetBotSettings();
            CreateBot(botSettings);

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );         

            Console.ReadLine();
        }
    }
}
