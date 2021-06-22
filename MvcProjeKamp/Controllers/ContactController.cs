using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.EntityFramework;

namespace MvcProjeKamp.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        private ContactManager contactManager = new ContactManager(new EfContactDal());
        private MessageManager messageManager = new MessageManager(new EfMessageDal());
        private ContactValidator contactValidator = new ContactValidator();
        public ActionResult Index()
        {
            //Partial Yap!
            var contactValues = contactManager.GetList();
            return View(contactValues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactValues = contactManager.GetById(id);

            return View(contactValues);
        }

        public PartialViewResult MessageListMenu()
        {
            var sendValue = messageManager.GetListSendbox().Count();
            ViewBag.sendValue = sendValue;

            var receiverValue = messageManager.GetListInbox().Count();
            ViewBag.receiverValue = receiverValue;

            var draftValue = messageManager.DraftList().Count();
            ViewBag.draftValue = draftValue;

            var unReadValue = messageManager.UnReadList().Count();
            ViewBag.unReadValue = unReadValue;

            var readValue = messageManager.ReadtList().Count();
            ViewBag.readValue = readValue;

            return PartialView();
        }
    }
}