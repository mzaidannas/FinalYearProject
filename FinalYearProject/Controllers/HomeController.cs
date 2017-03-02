using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DatabaseEntities db = new DatabaseEntities();
            var query = from x in db.Doctors
                        select new { x.Person_Id, x.Type, x.Person.Name, x.Person.Image_Path, x.Rating };
            List<Doctor_vs_Category> doctors = new List<Doctor_vs_Category>();
            List<string> category = new List<string>();
            foreach (var v in query)
            {
                Doctor_vs_Category obj = new Doctor_vs_Category();
                obj.Id = v.Person_Id;
                obj.Name = v.Name;
                obj.Category = v.Type;
                obj.Image_Path = v.Image_Path;
                obj.Rating = v.Rating;
                category.Add(v.Type);
                doctors.Add(obj);
            }
            ViewBag.Doctors = doctors;
            ViewBag.Category = category.Distinct().ToList();
            return View();
        }


        public ActionResult CountryList()
        {
            DatabaseEntities db = new DatabaseEntities();
            var query = from x in db.Doctors
                        select new
                        {
                            type = x.Type,
                            id = x.Person_Id
                        };
            List<string> categories = new List<string>();
            foreach (var v in query)
            {
                string str = v.type;
                categories.Add(str);
            }
            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                            categories,
                            "Type"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(categories);

            //IQueryable countries = Country.GetCountries();

            //if (HttpContext.Request.IsAjaxRequest())
            //{
            //    return Json(new SelectList(
            //                countries,
            //                "CountryCode",
            //                "CountryName"), JsonRequestBehavior.AllowGet
            //                );
            //}

            //return View(countries);
        }

        public ActionResult StateList(string type)
        {
            DatabaseEntities db = new DatabaseEntities();

            var query = db.Doctors.Where(x => x.Type == type);
            List<Person> doctors = new List<Person>();
            foreach (var v in query)
            {
                Person obj = new Person();
                obj.Person_Id = v.Person_Id;
                obj.Name = v.Person.Name;
                doctors.Add(obj);
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return Json(new SelectList(
                            doctors,
                            "Person_Id",
                            "Name"), JsonRequestBehavior.AllowGet
                    );
            }
            return View(doctors);
            //IQueryable states = State.GetStates().Where(x => x.CountryCode == CountryCode);

            //if (HttpContext.Request.IsAjaxRequest())
            //    return Json(new SelectList(
            //                    states,
            //                    "StateID",
            //                    "StateName"), JsonRequestBehavior.AllowGet
            //                );

            //return View(states);
        }
        public ActionResult LeaveMessage()
        {
            string password = Request.Form["password"];
            string email = Request.Form["email"];
            string message = Request.Form["message"];
            //string password = authenticate.GetPassword(email);
            //if (password.Equals(""))
            //{
            //    return Content("Invalid Email.");
            //}
            MailMessage mail = new MailMessage(email, "patdocas@gmail.com");
            //mail.From = "m.zaid.annas@gmail.com";
            //mail.To.Add("bcsf12a527@pucit.edu.pk");
            mail.Subject = "Leave a Message";
            mail.Body = message;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com"); //Don't setup port it is automatically setuped. Host ya yahan dee do ya .Host jese algi line pe he wahan
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new NetworkCredential(email, password);
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
            return View("Index", "Home");
        }
    }
}