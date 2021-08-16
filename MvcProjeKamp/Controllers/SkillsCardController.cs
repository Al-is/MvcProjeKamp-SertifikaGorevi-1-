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
    public class SkillsCardController : Controller
    {
        // GET: SkillsCard
        //Skilleri ekleme güncelleme

        private SkillManager skillManager = new SkillManager(new EfSkillDal());
        public ActionResult Index()
        {
            var skill = skillManager.GetList();
            return View(skill);
        }

        [HttpGet]
        public ActionResult AddSkills()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSkills(Skill skill)
        {
            skillManager.SkillAdd(skill);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditSkill(int id)
        {
            var skillValue = skillManager.GetById(id);
            return View(skillValue);
        }

        [HttpPost]
        public ActionResult EditSkill(Skill skill)
        {
            skillManager.SkillUpdate(skill);
            return RedirectToAction("Index");

        }
    }
}