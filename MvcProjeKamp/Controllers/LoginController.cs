using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MvcProjeKamp.Models;
using Newtonsoft.Json;
using Utilities;


namespace MvcProjeKamp.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        private AdminManager adminManager = new AdminManager(new EfAdminDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        private WriterLoginManager writerLoginManager = new WriterLoginManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {

            string password = admin.AdminPassword.Trim();
            string hashPassword = new CryptoHelper().ComputeSha256Hash(password);

            string key = "AAAAAAAAAAAA111111111111AAAAAAAAAAAA111111111111";
            string iv = "AAAAAAAA11111111";

            string mail = admin.AdminUserName.Trim();
            string cryptedMail = new CryptoHelper().Encrypt(mail, key, iv);

            var adminUser = adminManager.GetList().FirstOrDefault(x => x.AdminUserName == cryptedMail && x.AdminPassword == hashPassword);

            if (adminUser != null)
            {
                FormsAuthentication.SetAuthCookie(adminUser.AdminUserName, false);
                Session["AdminUserName"] = adminUser.AdminUserName;

                var captcha = Request.Form["g-recaptcha-response"];

                const string secret = "6LdmkncbAAAAAAJ3JiXuJQLAg9G-3quKomE2O6xo";


                var restUrl = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, captcha);

                WebRequest req = WebRequest.Create(restUrl);
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                JsonSerializer serializer = new JsonSerializer();

                CaptchaResult result = null;

                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    string resultObject = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<CaptchaResult>(resultObject);
                }

                if (!result.Success)
                {
                    ModelState.AddModelError("", "captcha mesajınız hatalı");
                    if (result.ErrorCodes != null)
                    {
                        ModelState.AddModelError("", result.ErrorCodes[0]);
                    }
                }
                else
                {
                    ViewBag.MessageRobot = "Kayıt Başarılı.";
                    return RedirectToAction("Index", "AdminCategory");
                }

                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer writer)
        {

            string password = writer.WriterPassword.Trim();
            string hashPassword = new CryptoHelper().ComputeSha256Hash(password);

            string key = "AAAAAAAAAAAA111111111111AAAAAAAAAAAA111111111111";
            string iv = "AAAAAAAA11111111";

            string mail = writer.WriterMail.Trim();
            string cryptedMail = new CryptoHelper().Encrypt(mail, key, iv);


            //var writerUserInfo2 = writerLoginManager.GetWriter(writer.WriterMail, writer.WriterPassword);

            var writerUserInfo = writerManager.GetList().FirstOrDefault(x => x.WriterMail == cryptedMail && x.WriterPassword == hashPassword);

            if (writerUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(writerUserInfo.WriterMail, false);
                Session["WriterMail"] = writerUserInfo.WriterMail;

                var captcha = Request.Form["g-recaptcha-response"];

                const string secret = "6LdmkncbAAAAAAJ3JiXuJQLAg9G-3quKomE2O6xo";


                var restUrl = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, captcha);

                WebRequest req = WebRequest.Create(restUrl);
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

                JsonSerializer serializer = new JsonSerializer();

                CaptchaResult result = null;

                using (var reader = new StreamReader(resp.GetResponseStream()))
                {
                    string resultObject = reader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<CaptchaResult>(resultObject);
                }

                if (!result.Success)
                {
                    ModelState.AddModelError("", "captcha mesajınız hatalı");
                    if (result.ErrorCodes != null)
                    {
                        ModelState.AddModelError("", result.ErrorCodes[0]);
                    }

                }
                else
                {
                    ViewBag.MessageRobot = "Kayıt Başarılı.";
                    return RedirectToAction("MyContent", "WriterPanelContent");
                }

                return RedirectToAction("WriterLogin");

            }
            else
            {
                return RedirectToAction("WriterLogin");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}