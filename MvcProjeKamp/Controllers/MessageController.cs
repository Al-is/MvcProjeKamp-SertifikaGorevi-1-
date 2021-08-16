using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace MvcProjeKamp.Controllers
{//TODO: Taslaklara kaydet , gönderilen mesajların font renkleri öyle gelsin
    public class MessageController : Controller
    {
        // GET: Message


        private MessageManager messageManager = new MessageManager(new EfMessageDal());
        private MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox(string p)
        {
            var messageList = messageManager.GetListInbox(p);
            return View(messageList);
        }

        public ActionResult Sendbox(string p)
        {
            var messageList = messageManager.GetListSendbox(p);
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
            ValidationResult results = messageValidator.Validate(message);
            if (results.IsValid)
            {
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
        [HttpPost]
        public ActionResult AddDraft(Message message)
        {
            message.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString().ToString());
            messageManager.AddDraft(message);
            return RedirectToAction("Inbox");
        }

        public ActionResult ReadMessage(int id)
        {
            var message = messageManager.GetById(id);
            messageManager.ReadStatus(message);
            return RedirectToAction("ListUnReadMessage");
        }
        public ActionResult ListReadMessage()
        {
            var message = messageManager.ReadList();
            return View(message);
        }
        public ActionResult ListUnReadMessage()
        {
            var message = messageManager.UnReadList();
            return View(message);
        }
        public ActionResult ListDraft()
        {
            var message = messageManager.DraftList();
            return View(message);
        }
    }
}