using _00003741_DBSD_CW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _00003741_DBSD_CW2.Models;
using _00003741_DBSD_CW2.DataAccess;
using System.Diagnostics;

namespace _00003741_DBSD_CW2.Controllers
{
    public class AccountController : Controller
    {
        private static bool Authenticated = false;

        public static string User = "";
        public static int PASSWORD_INCORRECT = -2;
        public static int USER_DOESNOT_EXIST= -1;
        public static int USER_ID;

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            USER_ID = new DatabaseManager().Login(user);

            if (USER_ID == USER_DOESNOT_EXIST)
            {
                ViewBag.Email = "Email does not exist";
            }else if (USER_ID == PASSWORD_INCORRECT)
            {
                ViewBag.Password = "Password you entered isn't correct";
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, false);
                User = user.Email;
                TemporaryData.userId = USER_ID;          
                setAuthentication(true);
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            setAuthentication(false);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.Titles = titles;

            return View() ;
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel User)
        {
            ViewBag.Titles = titles;
            try
            {
                DatabaseManager dbManager = new DatabaseManager();
                int result = dbManager.Register(User);

                if (User.Password != User.RePassword)
                {
                    ViewBag.RegError = "Passwords do not match";
                }else if (User.Dob == null)
                {
                    ViewBag.RegError = "Please select date of birth";
                }
                else if (User.Dob.Year > DateTime.Now.Year - 18)
                {
                    ViewBag.RegError = "You must be at least 18 years old to register";
                }else if (result == 1)
                {
                    ViewBag.RegError = User.Email + " is already registered";
                }
                else if(result == 0)
                {
                    return RedirectToAction("Login", User);
                }else
                {
                    ViewBag.RegError = User.Email + " Unknown Errror";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return View();
        }

        [Authorize]
        public ActionResult Update()
        {
            Customer cus = new DatabaseManager().GetCustomerById(TemporaryData.userId);
            return View(cus);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(Customer User)
        {
            DatabaseManager dbManager = new DatabaseManager();

            Customer oldCus = dbManager.GetCustomerById(TemporaryData.userId);
            try
            {

                if (User.Password != User.RePassword)
                {
                    return View();
                }
                else if (!User.Email.ToLower().Contains("@"))
                {
                    return View();
                }
                else
                {
                    User.Id = oldCus.Id;
                    User.Dob = oldCus.Dob;
                    User.Title = oldCus.Title;

                    Debug.WriteLine("" + User.Password);
                    dbManager.UpdateCustomer(User);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return Content(oldCus.Dob + "");
            return RedirectToAction("Index", "Home");
        }

        public void setAuthentication(bool UserAuthentication)
        {
            Authenticated = UserAuthentication;
        }

        public static bool isAuthenticated()
        {
            return Authenticated;
        }

        List<string> titles = new List<string>()
            {
                "DR",
                "MR",
                "MRS",
                "MS"
            };
    }
}