using Microsoft.AspNetCore.Mvc;
using PhotoViewing.Data.Interfaces;
using PhotoViewing.Data.Models;
using PhotoViewing.ViewModels;
using System.Linq;

namespace PhotoViewing.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotos _photoRepository;
        private readonly ITags _tagRepository;
        public HomeController(IPhotos photoRepository, ITags tagRepository)
        {
            _photoRepository = photoRepository;
            _tagRepository = tagRepository;
        }

        public ViewResult Index(int? tag, int page = 1)
        {
            int pageSize = 6;
            IQueryable<Photo> _photos = (IQueryable<Photo>)_photoRepository.Photos;

            if (tag != null && tag != 0)
            {
                _photos = _photos.Where(p => p.TagId == tag);
            }
           
            var count = _photos.Count();
            var items = _photos.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            FilterViewModel filterViewModel = new FilterViewModel(_tagRepository.Tags.ToList(), tag);

            var photos = new PhotosViewModel
            {
                Photos = items,
                PageViewModel = pageViewModel,
                FilterViewModel = filterViewModel
            };

            return View(photos);
        }

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Photo _photo = _photoRepository.getPhotoById(id);
                if (_photo != null)
                {
                    var photo = new PhotoViewModel
                    {
                        Photo = _photo
                    };

                    return View(photo);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            _photoRepository.Delete(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
