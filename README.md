# PhotoViewingApp

PhotoViewing - веб-приложение на asp .net core для просмотра, удаления и поиска фото по тегам.

PhotoViewongBot - веб-сервис для работы с Telegram.API(Bot), который забирает загруженные фото(с тегами к сообщению) для любого канала, сохраняет метаданные фото в БД(local, MS SQL).

Поиск по тегу показывает результат в виде 1 или нескольких фото.
Если тег в сообщении под фото отсутсвует, то генерируется тег в виде даты отправки(UTC).

Файлы из папки Database необходимо скопировать в папку MSSQLLocalDB
C:\Users\*имя пользователя*\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB
