using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        private ContentManager ContentManager = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ContentByHeading(int id)
        {
            var contentValues = ContentManager.GetListByHeadingId(id);
            return View(contentValues);
        }
    }
}