using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Library;

namespace WebApplication.Areas.Master.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: Master/UserManagement
        public ActionResult Index()
        {
            try
            {
                if (Session["Username"] == null && Session["RoleName"] == null)
                {
                    return RedirectToAction("Login", "Account", new { Area = "" });
                }
                else
                {
                    DataRowCollection ListDataUser = new Users().GetDataUser();
                    ViewData["ListDataUser"] = ListDataUser;

                }
                ViewData["Title"] = "User Management";
                return View();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        public ActionResult Create()
        {
            try
            {
                if (Session["Username"] == null && Session["RoleName"] == null)
                {
                    return RedirectToAction("Login", "Account", new { Area = "" });
                }

                DataRowCollection DataRole = new Users().GetAllRole();
                ViewData["DataRole"] = DataRole;

                return View();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection) 
        {
            try
            {
                var FirstName = collection["FirstName"].ToString();
                var LastName = collection["LastName"].ToString();
                string[] SplitFirstName = FirstName.Split(' ');
                string[] SplitLastName = LastName.Split(' ');
                var Username = "";
                if (SplitLastName[0].ToString()=="")
                {
                    Username = SplitFirstName[0].ToString().ToLower() + "." + "nefsalien";
                }
                else
                {
                    Username= SplitFirstName[0].ToString().ToLower() + "." + SplitLastName[0].ToString().ToLower();
                }
                var Email = collection["Email"].ToString();
                var PhoneNumber = collection["PhoneNumber"].ToString();
                var Role = collection["Role"].ToString();
                DataRowCollection DataRole = new Users().GetRole(Role);
                foreach (DataRow data in DataRole)
                {
                    Role = data["RoleId"].ToString();
                }
                var Password = Username+"@"+DateTime.Now.Year;
                var CreatedBy = "bagas";
                var CreatedAt = DateTime.Now;

                int InsertUser = new Users().InsertUser(FirstName, LastName, Email, PhoneNumber, Username, Password, Role, CreatedAt, CreatedBy);

                if (InsertUser != 0)
                {
                    TempData["Message"] = "Data Berhasil Disimpan. Dengan Username : "+Username+"";
                    TempData["MessageClass"] = "success";
                    TempData["SVGClass"] = "check-circle"; //Success
                    return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
                }
                else
                {
                    TempData["Message"] = "Data Gagal Disimpan, Silahkan Coba Lagi atau Hubungi Mas Bagas";
                    TempData["MessageClass"] = "danger";
                    TempData["SVGClass"] = "exclamation-triangle"; //danger

                    return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
                }
            }
            catch (Exception err)
            {
                TempData["Message"] = err;
                TempData["MessageClass"] = "danger";
                TempData["SVGClass"] = "exclamation-triangle"; //danger

                return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
            }
        }

        public ActionResult Edit(string UserID)
        {
            try
            {
                if (Session["Username"] == null && Session["RoleName"] == null)
                {
                    return RedirectToAction("Login", "Account", new { Area = "" });
                }
                else
                {
                    DataRowCollection DataUser = new Users().GetDataUserByID(UserID);
                    DataRowCollection DataRole = new Users().GetAllRole();
                    ViewData["DataUser"] = DataUser;
                    ViewData["DataRole"] = DataRole;

                    //foreach (DataRow Row in DataUser)
                    //{
                    //    ViewData["RoleName"] = Row["RoleName"].ToString();
                    //}

                    return View();
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                var UserId = collection["UserId"].ToString();
                var FirstName = collection["FirstName"].ToString();
                var LastName = collection["LastName"].ToString();
                string[] SplitFirstName = FirstName.Split(' ');
                string[] SplitLastName = LastName.Split(' ');
                var Username = SplitFirstName[0].ToString().ToLower() + "." + SplitLastName[0].ToString().ToLower();
                var Email = collection["Email"].ToString();
                var PhoneNumber = collection["PhoneNumber"].ToString();
                var Role = collection["Role"].ToString();
                var Password = Username + "@" + DateTime.Now.Year;
                var ModifiedBy = "bagas";
                var ModifiedAt = DateTime.Now;

                int InsertUser = new Users().EditUser(UserId, FirstName, LastName, Email, PhoneNumber, Role, ModifiedAt, ModifiedBy);

                if (InsertUser != 0)
                {
                    TempData["Message"] = "Data Berhasil Diubah";
                    TempData["MessageClass"] = "success";
                    TempData["SVGClass"] = "check-circle"; //Success
                    return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
                }
                else
                {
                    TempData["Message"] = "Data Gagal Diubah, Silahkan Coba Lagi atau Hubungi Mas Bagas";
                    TempData["MessageClass"] = "danger";
                    TempData["SVGClass"] = "exclamation-triangle"; //danger

                    return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
                }
            }
            catch (Exception err)
            {
                TempData["Message"] = err;
                TempData["MessageClass"] = "danger";
                TempData["SVGClass"] = "exclamation-triangle"; //danger

                return RedirectToAction("Index", "UserManagement", new { Area = "Master" });
            }
        }
    }
}