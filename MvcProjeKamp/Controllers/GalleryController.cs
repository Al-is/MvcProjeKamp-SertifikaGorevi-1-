using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery

        private ImageFileManager imageFileManager = new ImageFileManager(new EFImageFileDal());
        public ActionResult Index()
        {
            var files = imageFileManager.GetList();
            return View(files);
        }
    }
}