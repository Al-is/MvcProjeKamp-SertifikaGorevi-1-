using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content

        private ContentManager contentManager = new ContentManager(new EfContentDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllContent(string content)
        {
            var values = contentManager.GetList(content);
            return View(values);
        }

        [HttpGet]
        public ActionResult ContentByHeading(int id)
        {
            var contentValues = contentManager.GetListByHeadingId(id);
            return View(contentValues);
        }
    }
}