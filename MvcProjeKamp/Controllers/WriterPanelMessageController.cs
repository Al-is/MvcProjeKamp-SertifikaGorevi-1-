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

namespace MvcProjeKamp.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        private MessageManager messageManager = new MessageManager(new EfMessageDal());
        private MessageValidator messageValidator = new MessageValidator();


        public ActionResult Inbox()
        {
            string session = (string)Session["WriterMail"];
            var messageList = messageManager.GetListInbox(session);
            return View(messageList);
        }

        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }

        public ActionResult Sendbox()
        {
            string session = (string)Session["WriterMail"];
            var messageList = messageManager.GetListSendbox(session);
            return View(messageList);
        }
        public ActionResult GetInboxMessageDetails(int id)
        {
            var messageValues = messageManager.GetById(id);

            return View(messageValues);
        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var messageValues = messageManager.GetById(id);

            return View(messageValues);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
                message.SenderMail = sender;
                message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
                messageManager.MessageAdd(message);
                return RedirectToAction("Sendbox");
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
    }
}