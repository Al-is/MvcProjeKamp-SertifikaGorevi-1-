using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjeKamp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private AdminManager AdminManager = new AdminManager(new EfAdminDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
           
                SHA1 sha = new SHA1CryptoServiceProvider();
                string password = admin.AdminPassword;
                string result = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
                admin.AdminPassword = result;

                var adminUser = AdminManager.GetList().FirstOrDefault(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == admin.AdminPassword);

                if (adminUser != null)
                {
                    FormsAuthentication.SetAuthCookie(adminUser.AdminUserName, false);
                    Session["AdminUserName"] = adminUser.AdminUserName;
                    return RedirectToAction("Index", "AdminCategory");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            

        }
    }
}