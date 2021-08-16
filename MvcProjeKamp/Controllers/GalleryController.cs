﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

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

        [HttpGet]
        public ActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddImage(ImageFile image)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string expansion = Path.GetExtension(Request.Files[0].FileName);
                string path = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                Request.Files[0].SaveAs(Server.MapPath(path));
                image.ImagePath = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                imageFileManager.Add(image);
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}