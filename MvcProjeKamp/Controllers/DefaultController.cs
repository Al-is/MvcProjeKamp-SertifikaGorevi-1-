using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class DefaultController : Controller
    {
        private HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        private ContentManager contentManager = new ContentManager(new EfContentDal());

        public ActionResult Headings()
        {
            var headingList = headingManager.GetList();
            return View(headingList);
        }

        public PartialViewResult Index(int id)
        {
            var contentList = contentManager.GetListByHeadingId(id);
            return PartialView(contentList);
        }
    }
}