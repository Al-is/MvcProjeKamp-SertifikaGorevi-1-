using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using PagedList;
using PagedList.Mvc;

namespace MvcProjeKamp.Controllers
{
    public class WriterPanelController : Controller
    {
        // GET: WriterPanel

        private HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        private CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        private WriterManager writerManager = new WriterManager(new EfWriterDal());
        private WriterValidator writerValidator = new WriterValidator();

        [HttpGet]
        public ActionResult WriterProfile(int id = 0)
        {
            using (Context context = new Context())
            {
                string session = (string)Session["WriterMail"];
                id = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId)
                    .FirstOrDefault();
                var writerValue = writerManager.GetById(id);
                return View(writerValue);
            }

        }
        [HttpPost]
        public ActionResult WriterProfile(Writer writer)
        {

            ValidationResult results = writerValidator.Validate(writer);

            if (results.IsValid)
            {
                writerManager.WriterUpdate(writer);
                return RedirectToAction("AllHeading", "WriterPanel");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
        public ActionResult MyHeading(string session)
        {
            using (Context context = new Context())
            {
                session = (string)Session["WriterMail"];
                var writerIdInfo = context.Writers.Where(x => x.WriterMail == session).Select(y => y.WriterId)
                     .FirstOrDefault();

                var values = headingManager.GetListByWriter(writerIdInfo);
                return View(values);
            }

        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valueCategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;

            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading heading)
        {
            using (Context context = new Context())
            {
                string writerMailInfo = (string)Session["WriterMail"];
                var writerIdInfo = context.Writers.Where(x => x.WriterMail == writerMailInfo).Select(y => y.WriterId)
                    .FirstOrDefault();

                heading.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                heading.WriterId = writerIdInfo;
                heading.HeadingStatus = true;
                headingManager.HeadingAdd(heading);
                return RedirectToAction("MyHeading");
            }
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in categoryManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryId.ToString()
                                                  }).ToList();

            ViewBag.vlc = valueCategory;

            var headingValue = headingManager.GetById(id);
            return View(headingValue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading heading)
        {
            headingManager.HeadingUpdate(heading);
            return RedirectToAction("MyHeading");

        }
        public ActionResult DeleteHeading(int id)
        {
            var headingValue = headingManager.GetById(id);
            headingManager.HeadingDelete(headingValue);

            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int? page = 1)
        {
            var headings = headingManager.GetList().ToPagedList(page ?? 1, 5);
            return View(headings);
        }
    }
}