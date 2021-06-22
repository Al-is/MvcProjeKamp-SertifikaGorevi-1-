using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKamp.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        private AboutManager aboutManager = new AboutManager(new EfAbaoutDal());
        public ActionResult Index()
        {

            var aboutValues = aboutManager.GetList();
            return View(aboutValues);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            aboutManager.AboutAdd(about);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartialView()
        {
            return PartialView();
        }
        public ActionResult DeleteAbout(int id)
        {
            var aboutValue = aboutManager.GetById(id);
            aboutManager.AboutDelete(aboutValue);

            return RedirectToAction("Index");
        }
    }
}