using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class SkillsCardController : Controller
    {
        // GET: SkillsCard

        private SkillManager skillManager = new SkillManager(new EfSkillDal());
        public ActionResult Index()
        {
            var skill = skillManager.GetList();
            return View(skill);
        }
    }
}