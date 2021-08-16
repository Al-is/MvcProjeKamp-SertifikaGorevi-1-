using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKamp.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        private ContentManager contentManager = new ContentManager(new EfContentDal());
        public ActionResult MyContent(string session)
        {
            using (Context context = new Context())
            {
                session = (string)Session["WriterMail"];
                var writerIdInfo = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId)
                    .FirstOrDefault();

                var contentValues = contentManager.GetListByWriter(writerIdInfo);
                return View(contentValues);
            }
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpGet]
        public ActionResult AddContent(Content content)
        {
            using (Context context = new Context())
            {
                string mail = (string)Session["WriterMail"];
                var writerIdInfo = context.Writers.Where(x => x.WriterMail == mail).Select(y => y.WriterId)
                    .FirstOrDefault();
                content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                content.WriterId = writerIdInfo;
                content.ContentStatus = true;

                contentManager.ContentAdd(content);
                return RedirectToAction("MyContent");
            }

        }


        public ActionResult ToDoList()
        {
            return View();
        }
    }
}