using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalYearProject.Models;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace FinalYearProject.Controllers
{
    public class AccountController : Controller
    {
        IUser authenticate;
        public AccountController( IUser obj )
        {
            authenticate = obj;
        }
        
        // Get: Account
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        // POST: Account
        [HttpPost]
        public ActionResult Signin(Login user)
        {
            if (ModelState.IsValid)
            {
                if (authenticate.Athenticate(user) && authenticate.isDoctor(user))
                {
                    Session["User_Name"] = user.Username;
                    Session["User_Password"] = user.Password;
                    int Id = authenticate.GetPersonID(user.Username, user.Password);
                    Session["Person_ID"] = Id;
                    return RedirectToAction("Index", "Doctor");
                }
            }
            return View();
        }

        // GET: Account
        [HttpGet]
        public ActionResult Signup()
        {

            return RedirectToAction("Signin");
        }

         [HttpPost]
        public ActionResult Signup(MainPerson mainPerson)
        {
            if (ModelState.IsValid)
            {
                authenticate.RegisterDoctor(mainPerson);
            }
            //return Content(person.Name + "\n" + person.CNIC + "\n" + person.Address + "\n" + person.DOB + "\n" + person.Gender + "\n" + person.Career_Start + "\n" + person.Type + "\n" + person.Average_Duration + "\n" + person.Phone_no + "\n" + person.UserName + "\n" + person.Password);
            return View("Signin");
        }
        public JsonResult UserExistance(string username)
        {
            if (authenticate.Exists(new Login() { Username = username }) )
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PasswordRecovery()
        {
            string email = Request.Form["email"];
            string password = authenticate.GetPassword(email);
            if (password.Equals(""))
            {
                return Content("Invalid Email.");
            }
            MailMessage mail = new MailMessage("patdocas@gmail.com", email);
            //mail.From = "m.zaid.annas@gmail.com";
            //mail.To.Add("bcsf12a527@pucit.edu.pk");
            mail.Subject = "PDA Password Recovery";
            mail.Body = "Pehle ye btao k apna password yaad ku ni rakhy??? Hain? Acha dfa kro ye lo apna Purana password. Next time care krna.\n " + password ;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com"); //Don't setup port it is automatically setuped. Host ya yahan dee do ya .Host jese algi line pe he wahan
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new NetworkCredential("patdocas@gmail.com", "hzqjpdapda");
            smtp.EnableSsl = true;

            //Agar error aya to matlab email address valid nahien ya server ka responce nahien aya.etc
            try
            {
                smtp.Send(mail);
            }
            catch (SmtpException except)
            {
                Console.WriteLine("{0} Exception caught.", except);
            }
            return RedirectToAction("Signin");
        }
        public ActionResult Rough()
        {
            return View();
        }
    }
}