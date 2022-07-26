using System.Data;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.Library;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return PartialView();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var username = collection["username"];
                var email = collection["username"];
                var password = collection["password"];
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    DataRow User = new Users().GetUser(email, username, password);
                    if (User != null)
                    {
                        Session["Username"] = User["Username"].ToString();
                        Session["Fullname"] = User["FirstName"].ToString() +" "+ User["LastName"].ToString();
                        Session["RoleName"] = User["RoleName"].ToString();
                        Session["RoleSubName"] = User["RoleSubName"].ToString();
                        new Users().UpdateUserLastLogin(username);
                        new Users().LogUserLogin(username);
                        return RedirectToAction("Index", "Home", new { Area = "Home" });
                    }
                    else
                    {
                        TempData["Message"] = "Username atau Password tidak sesuai!";
                        TempData["MessageClass"] = "danger";
                        TempData["SVGClass"] = "exclamation-triangle"; //danger
                        return RedirectToAction("Login", "Account", new { Area = "" });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
