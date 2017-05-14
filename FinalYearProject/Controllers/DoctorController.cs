using FinalYearProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TableDependency.EventArgs;
using TableDependency.SqlClient;
using PagedList;
using PagedList.Mvc;

namespace FinalYearProject.Controllers
{
    public class DoctorController : Controller
    {
        private DatabaseEntities db;
        IDoctor doc;
        public DoctorController()
        {
        }
        public DoctorController(IDoctor d)
        {
            db = new DatabaseEntities();

            //Database Change Detection
            var connectionString = @"Data Source=810649c0-b374-4c9f-83f4-a72200d16193.sqlserver.sequelizer.com;Database=db810649c0b3744c9f83f4a72200d16193;User ID=ysuguooalqevqoko;Password=RRvkdxvyqvWeDjBw3H7DdZyWEPsc3yPuEMuHzT4Jvy8zVDDrkV7fK3DYsGNwesEZ;MultipleActiveResultSets=True;App=EntityFramework";
            var tableDependency = new SqlTableDependency<Appointment>(connectionString, "Appointments");

            tableDependency.OnChanged += tableDependency_Changed;
            tableDependency.OnError += tableDependency_OnError;

            tableDependency.Start();

            doc = d;
            //Get All(Count) Newly Notifications to display at Doctor's master page.
        }
        // GET: Doctor
        public ActionResult Index()
        {
            List<Appointment> list = new List<Appointment>();
            if (checkSession())
            {
                //Get id for logged in person.
                int id = (int)Session["Person_ID"];
                ViewBag.count = db.Notifications.Where(x => x.Doc_Id == id).Count();
                Session["NumOfNoti"] = ViewBag.count;
                list = doc.GetAppointmentList(id);
                if (list == null)
                {
                    return Content("No data found.");
                }
                return View(list);
            }
            return RedirectToAction("Signin", "Account");
        }


        [HttpGet]
        public ActionResult GetMyPatients(int? page)
        {
            if (!checkSession())
            {
                return RedirectToAction("Signin", "Account");
            }
            ViewBag.count = Session["NumOfNoti"];
            int id = (int)Session["Person_ID"];
            IPagedList<Full_Patient> patients = doc.GetAllPatients(id).ToPagedList(page ?? 1, 3);
            return View(patients);
        }

        [HttpPost]
        public ActionResult GetMyPatients(string searchBy, string search, int? page)
        {
            if ( !checkSession() )
            {
                return RedirectToAction("SignIn", "Account");
            }
            ViewBag.count = Session["NumOfNoti"];
            int id = (int)Session["Person_ID"];
            if (searchBy == "Insurance_No")
            {
                IPagedList<Full_Patient> result = doc.GetAllPatients(id).Where(x => x.Insurance_No.ToString() == search).AsQueryable().ToList().ToPagedList(page ?? 1, 3);
                if (result != null)
                {
                    return View(result);
                }
            }
            else
            {
                IPagedList<Full_Patient> result = doc.GetAllPatients(id).Where(x => x.Name.ToLower().StartsWith(search.ToLower())).ToList().ToPagedList(page ?? 1, 3);
                if (result != null)
                {
                    return View(result);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RegisterPatient()
        {
            if (checkSession())
            {
                ViewBag.count = Session["NumOfNoti"];
                return View();
            }
            return RedirectToAction("Signin", "Account");
        }

        [HttpPost]
        public ActionResult RegisterPatient(Full_Patient patient)
        {
            if (checkSession())
            {
                ViewBag.count = Session["NumOfNoti"];
                int DoctorID = (int)Session["Person_ID"];
                bool result = doc.AddPatient(patient, DoctorID);
                if (result)
                {
                    return RedirectToAction("GetMyPatients");
                }
                return View(patient);
            }
            return RedirectToAction("Signin", "Account");
        }
        [HttpGet]
        public ActionResult AddPrescription()
        {
            if (checkSession())
            {
                ViewBag.count = Session["NumOfNoti"];
                return View();
            }
            return RedirectToAction("Signin", "Account");
        }

        [HttpPost]
        public ActionResult AddPrescription(Prescription Prescription, int id)
        {
            if (checkSession())
            {
                ViewBag.count = Session["NumOfNoti"];
                int DoctorID = (int)Session["Person_ID"];
                Prescription.Pat_Id = id;
                bool result = doc.AddPrescription(Prescription,DoctorID);
                if (result)
                {
                    return RedirectToAction("GetMyPatients");
                }
                return View(Prescription);
            }
            return RedirectToAction("Signin", "Account");
        }

        public ActionResult ViewPrescription(int id , int? page)
        {
            if (checkSession())
            {
                List<Prescription> pres = doc.GetAllPrescriptions(id,(int)Session["Person_ID"]);
                if(pres.Count() != 0)
                {
                    return View(pres.ToPagedList(page ?? 1, 3));
                }
            }
            return RedirectToAction("GetMyPatients");
        }

        public ActionResult DoctorProfile(string Id)
        {
            ViewBag.count = Session["NumOfNoti"];
            Full_Doctor full_doctor = doc.GetFullDoctorForProfile(Convert.ToInt32(Id));
            if (full_doctor == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(full_doctor);
        }

        public ActionResult Notification()
        {
            if (checkSession())
            {
                ViewBag.count = Session["NumOfNoti"];
                int id = (int)Session["Person_ID"];
                List<Notification> list = doc.GetNotificationList(id);
                return View(list);
            }
            return RedirectToAction("Signin", "Account");
        }
        public ActionResult Details(int id)
        {
            if ( !checkSession() )
            {
                return RedirectToAction("SignIn", "Account");
            }
            Full_Patient patient = doc.PatientDetails(id);
            int doc_id = (int)Session["Person_ID"];
            ViewBag.History = doc.GetPatientHistory(id, doc_id);
            if (!(patient == null))
            {
                return View(patient);
            }
            return RedirectToAction("GetMyPatients");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!checkSession())
            {
                return RedirectToAction("SignIn", "Account");
            }
            Full_Patient student = doc.PatientDetails(id);
            return View(student);
        }
        [HttpPost]
        public ActionResult Edit(Full_Patient patient)
        {
            if (!checkSession())
            {
                return RedirectToAction("SignIn", "Account");
            }
            if (ModelState.IsValid)
            {
                doc.SaveEditedPatient(patient);
                return RedirectToAction("GetMyPatients");
            }
            return View(patient);
        }


        [HttpPost]
        public ActionResult DeletePatient(int id)
        {
            if (!checkSession())
            {
                return RedirectToAction("SignIn", "Account");
            }
            bool result = doc.DeletePatient(id,(int)Session["Person_ID"]);
            if (result)
            {
                return RedirectToAction("GetMyPatients","Doctor");
            }
            return RedirectToAction("GetMyPatients", "Doctor");
        }

        public bool checkSession()
        {
            if (Session["User_Name"] == null || Session["User_Password"] == null || Session["Person_ID"] == null)
            {
                return false;
            }
            return true;
        }
        public ActionResult Logout()
        {
            if (checkSession())
            {
                Session.Clear();
                Session.RemoveAll();
            }
            return RedirectToAction("Index", "Home");
        }


        //SqlTableDependency Functions
        public void tableDependency_Changed(object sender, RecordChangedEventArgs<Appointment> e)
        {
            if (e.ChangeType != TableDependency.Enums.ChangeType.None)
            {
                var changedEntity = e.Entity;
                RedirectToAction("Notification","Doctor");
                //Console.WriteLine("Database has been changed.");
            }

            return;
        }
        static void tableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }
        public JsonResult GetEvents(int id)
        {
            List<Events> eventList = doc.GetCalenderEvents(id);
            Events[] rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveAppointment(string name)
        {
            bool result = false;
            if (!name.Equals(""))
            {
                if (checkSession())
                {
                    int notificationID = Convert.ToInt32(name);
                    int docID = (int)Session["Person_ID"];

                    List<Notification> list = doc.GetNotificationList(docID);
                    Notification notification = list[notificationID];
                    doc.AddAppointment(notification, docID);
                    doc.delete_notification(notificationID);
                    result = true;
                }
            }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNotifications(string notification, string date)
        {
            List<string> ui_list = new List<string>();
            if (checkSession())
            {
                int docID = (int)Session["Person_ID"];
                List<DateTime> free_slots = doc.GetDoctorSlots(docID, date);
                foreach (DateTime item in free_slots)
                {
                    ui_list.Add("<option>" + item.ToString("hh:mm:ss tt") + "</option>");
                }
            }
            return this.Json(ui_list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditPrescription(int id)
        {
            Prescription prescription = null;
            if (checkSession())
            {
                prescription = doc.GetPrescription(id);
                if(prescription != null)
                {
                    return View(prescription);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditPrescription(Prescription p,int id)
        {

            if (checkSession())
            {
                doc.EditPrescription(p, id);

                return RedirectToAction("GetMyPatients", "Doctor");
            }
            return View("GetMyPatients","Doctor");
        }
        
        public ActionResult DeletePrescription(int id)
        {
            if (checkSession())
            {
                bool b = doc.DeletePrescription(id);

            }
            return RedirectToAction("GetMyPatients", "Doctor");
        }

        [HttpPost]
        public JsonResult SaveAlternateAppointment(string date, string time, string id)
        {
            //var date = Request.Form["date"];
            //var time = Request.Form["time"];
            bool result = false;
            if (checkSession())
            {
                int notificationID = Convert.ToInt32(id);
                int docID = (int)Session["Person_ID"];

                List<Notification> list = doc.GetNotificationList(docID);
                Notification notification = list[notificationID];
                doc.AddAppointment(notification, docID);
                doc.delete_notification(notificationID);
                result = true;
            }
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}