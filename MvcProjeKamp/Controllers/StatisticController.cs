using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic


        public ActionResult Index()
        {
            using (Context db = new Context())
            {
                //Toplam Kategori Sayısı
                var CategoryAll = db.Categories.Count();
                ViewBag.CategoryAll = CategoryAll;

                //Yazılım Kategorisine ait başlık sayısı
                var SoftwareByHeadings =
                    db.Headings.Count(x => x.CategoryId == db.Categories.Where(z => z.CategoryId == 13).Select(y => y.CategoryId).FirstOrDefault());
                ViewBag.SoftwareByHeadings = SoftwareByHeadings;

                //Yazar adında 'a' harfi geçen yazar sayısı
                var WriterLikeCount = db.Writers.Where(x => x.WriterName.Contains("a")).Select(y => y.WriterId).Count();
                ViewBag.WriterLikeCount = WriterLikeCount;

                //En fazla başlığa sahip olan kategori adı
                var CategoryHeading = db.Headings.GroupBy(x => x.CategoryId).Where(y => y.Count() > 1)
                    .Select(z => new { z.FirstOrDefault().Category.CategoryName, sayi = z.Count() }).Distinct()
                    .OrderByDescending(o => o.sayi).Select(a => a.CategoryName).FirstOrDefault();

                //var value4 = c.Categories.Where(u => u.CategoryID == c.Headings.GroupBy(x => x.CategoryID).OrderByDescending(x => x.Count())
                //    .Select(x => x.Key).FirstOrDefault()).Select(x => x.CategoryName).FirstOrDefault();
                ViewBag.CategoryHeading = CategoryHeading;

                //Kategori tablosunda durumu true olan kategoriler ile false olan kategoriler arasındaki sayısal fark
                var difference = db.Categories.Count(x => x.CategoryStatus == true) - db.Categories.Count(x => x.CategoryStatus == false);
                ViewBag.difference = difference;

            }

            return View();
        }
    }
}