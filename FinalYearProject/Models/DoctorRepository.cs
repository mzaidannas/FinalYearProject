using FinalYearProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class DoctorRepository : IDoctor
    {
        private DatabaseEntities db;
        public DoctorRepository()
        {
            db = new DatabaseEntities();
        }
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctor = new List<Doctor>();
            var query = from x in db.Doctors
                        select x;
            foreach (var v in query)
            {
                doctor.Add(v);
            }
            return doctor;
        }
        public List<Full_Patient> GetAllPatients(int id)
        {
            List<Full_Patient> patients = new List<Full_Patient>();
            var query = from d in db.Doctors
                        join pd in db.PatientDoctors on d.Person_Id equals pd.Doc_Id
                        join p in db.Persons on pd.Pat_Id equals p.Person_Id
                        where d.Person_Id == id
                        select new Full_Patient
                        {
                            Id = p.Person_Id,
                            Username = p.Login.Username,
                            Password = p.Login.Password,
                            Email = p.Login.Email,
                            Name = p.Name,
                            CNIC = p.CNIC,
                            DOB = p.DOB,
                            Gender = p.Gender,
                            Phone = p.Phone,
                            Address = p.Address,
                            Image_Path = p.Image_Path,
                            Blood_Group = p.Patient.Blood_Group,
                            Insurance_No = p.Patient.Insurance_No
                        };
            foreach (var v in query)
            {
                patients.Add(v);
            }
            return patients;
        }
        public bool AddPatient(Full_Patient full_patient, int DoctorID)
        {
            var query = from x in db.Persons
                        where x.CNIC.Equals(full_patient.CNIC) && x.DOB == null
                        select x;
            int pat_id = 0;
            if (query.Count() == 0)
            {
                Person person = new Person();
                Patient patient = new Patient();
                Login login = new Login();

                //Extract Login's Attribute
                login.Username = full_patient.Username;
                login.Email = full_patient.Email;
                string email = login.Email;
                string[] userName = email.Split('@');
                login.Username = userName[0];
                Random rnd = new Random();
                string password = login.Username + rnd.Next(9999999);
                login.Password = password;


                //Extract Person's Attribute
                person.Phone = full_patient.Phone;
                person.CNIC = full_patient.CNIC;
                person.Name = full_patient.Name;
                person.Address = full_patient.Address;
                person.DOB = full_patient.DOB;
                person.Gender = full_patient.Gender;

                //Extract Patient's Attribute
                patient.Blood_Group = full_patient.Blood_Group;
                patient.Insurance_No = full_patient.Insurance_No;

                //Save to Database
                db.Persons.Add(person);
                db.Patients.Add(patient);
                db.Logins.Add(login);

                //Commit Changes
                db.SaveChanges();

                //Get Maximum ID
                pat_id= db.Patients.Max(x => x.Person_Id);

                PatientDoctors patientDoctor = new PatientDoctors();
                patientDoctor.Doc_Id = DoctorID;
                patientDoctor.Pat_Id = pat_id;
                patientDoctor.Pat_Confidential = false;
                patientDoctor.Recovery_Status = 0;

                db.PatientDoctors.Add(patientDoctor);
                db.SaveChanges();
            }
            else
            {
                foreach (var item in query)
                {
                    pat_id = item.Person_Id;
                }

                var query1 = from x in db.Logins
                             where x.Person_Id == pat_id
                             select x;
                if (query1.Count() == 0) { 
                    Login login = new Login();
                    login.Username = full_patient.Username;
                    login.Email = full_patient.Email;
                    string email = login.Email;
                    string[] userName = email.Split('@');
                    login.Username = userName[0];
                    Random rnd = new Random();
                    string password = login.Username + rnd.Next(9999999);
                    login.Password = password;
                    login.Person_Id = pat_id;
                    db.Logins.Add(login);
                }

                PatientDoctors patientndoctor = new PatientDoctors();
                patientndoctor.Pat_Id = pat_id;
                patientndoctor.Doc_Id = DoctorID;
                patientndoctor.Pat_Confidential = false;
                patientndoctor.Recovery_Status = 0;

                db.PatientDoctors.Add(patientndoctor);

                db.SaveChanges();
            }


            //patient's folder

            var dir = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\normal\";
            var dir_confidential = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\confidential\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!Directory.Exists(dir_confidential))
                Directory.CreateDirectory(dir_confidential);
            Patient_History pat_his = new Patient_History();
            pat_his.Confidential = dir_confidential;
            pat_his.Public = dir;
            pat_his.Pat_Id = pat_id;

            db.Patient_History.Add(pat_his);
            db.SaveChanges();
            return true;
        }

        public Full_Patient PatientDetails(int id)
        {
            var query = from d in db.Patients
                        join p in db.Persons on d.Person_Id equals p.Person_Id
                        where d.Person_Id == id
                        select new Full_Patient
                        {
                            Id = p.Person_Id,
                            Username = p.Login.Username,
                            Password = p.Login.Password,
                            Email = p.Login.Email,
                            Name = p.Name,
                            CNIC = p.CNIC,
                            DOB = p.DOB,
                            Gender = p.Gender,
                            Phone = p.Phone,
                            Address = p.Address,
                            Image_Path = p.Image_Path,
                            Blood_Group = p.Patient.Blood_Group,
                            Insurance_No = p.Patient.Insurance_No
                        };
            foreach (var patient in query)
            {
                return patient;
            }
            return null;
        }

        public void AddDoctor(Doctor doc)
        {
            db.Doctors.Add(doc);
            db.SaveChanges();
        }

        public void SaveEditedPatient(Full_Patient full_patient)
        {
            Person person = new Person();
            Patient patient = new Patient();
            
            person = db.Persons.Find(full_patient.Id);
            patient = db.Patients.Find(full_patient.Id);

            if (person.Person_Id != full_patient.Id)
            {
                person.Person_Id = full_patient.Id;
            }
            if(person.Address != full_patient.Address)
            {
                person.Address = full_patient.Address;
            }
            if(person.CNIC != full_patient.CNIC)
            {
                person.CNIC = full_patient.CNIC;
            }
            if(person.DOB != full_patient.DOB)
            {
                person.DOB = full_patient.DOB;
            }
            if(person.Gender != full_patient.Gender)
            {
                person.Gender = full_patient.Gender;
            }
            if(person.Image_Path != full_patient.Image_Path)
            {
                person.Image_Path = full_patient.Image_Path;
            }
            if(person.Name != full_patient.Name)
            {
                person.Name = full_patient.Name;
            }
            if(person.Phone != full_patient.Phone)
            {
                person.Phone = full_patient.Phone;
            }


            if(patient.Blood_Group != full_patient.Blood_Group)
            {
                patient.Blood_Group = full_patient.Blood_Group;
            }
            if(patient.Insurance_No != full_patient.Insurance_No)
            {
                patient.Insurance_No = full_patient.Insurance_No;
            }

            db.Entry(person).State = EntityState.Modified;
            db.Entry(patient).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveDoctor(Doctor doc)
        {
            db.Entry(doc).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool DeletePatient(int pat_id,int doc_id)
        {

            var deleteFromDocTable = from x in db.PatientDoctors
                                     where x.Pat_Id == pat_id
                                     select x;

            List<PatientDoctors> pds=new List<PatientDoctors>();
            foreach(var item in deleteFromDocTable)
            {
                pds.Add(item);
            }

            //check if there is a record with this id
            if (deleteFromDocTable.Count() == 1)
            {
                var deletefromlogin = from x in db.Logins
                                      where x.Person_Id == pat_id
                                      select x;

                var pd=pds.Where(x => x.Doc_Id == doc_id).First();
                Login log = deletefromlogin.First();

                db.PatientDoctors.Remove(pd);
                db.Logins.Remove(log);
                db.SaveChanges();

                return true;
            }
            else if (deleteFromDocTable.Count() > 0)
            {
                var pd = pds.Where(x => x.Doc_Id == doc_id).First();
                db.PatientDoctors.Remove(pd);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public Doctor Details(int id)
        {
            var query = from x in db.Doctors
                        where x.Person_Id == id
                        select x;
            foreach (Doctor s in query)
            {
                return s;
            }
            return null;
        }

        public List<Prescription> GetAllPrescriptions(int id, int doc_id)
        {
            List<Prescription> lp = new List<Prescription>();

            var query = from x in db.Prescriptions
                        where x.Pat_Id == id && x.Doc_Id == doc_id
                        select x;

            foreach (var item in query)
            {
                lp.Add(item);
            }

            return lp;
        }

        public bool DeletePrescription(int id)
        {
            var query = from x in db.Prescriptions
                        where x.Prescription_Id == id
                        select x;
            if (query.Count() > 0) {
                foreach (var item in query)
                {
                    db.Prescriptions.Remove(item);

                }
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public Doctor GetDoctor(int id)
        {
            return db.Doctors.Find(id);
        }
        public bool DeleteDoctor(int id)
        {
            DatabaseEntities db = new DatabaseEntities();
            var result = from x in db.Doctors
                         where x.Person_Id == id
                         select x;
            //check if there is a record with this id

            if (result.Count() > 0)
            {
                Doctor doc = result.First();
                db.Doctors.Remove(doc);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public IQueryable<Doctor> SearchIndex(string searchString)
        {
            var doctors = from x in db.Doctors
                           select x;
            doctors = doctors.Where(s => s.Person.Name.Contains(searchString));
            if (doctors != null)
            {
                return doctors;
            }
            return null;
        }
        public List<Appointment> GetAppointmentList(int id)
        {
            List<Appointment> list = new List<Appointment>();
            //list = db.Appointments.ToList();
            var query = from x in db.Appointments
                        where x.Doc_Id == id
                        select new
                        {
                            title = "Appointment",
                            //start = x.Date.GetDateTimeFormats().ElementAt(5) + "T" + x.Start.ToString(),
                            //end = x.Date.GetDateTimeFormats().ElementAt(5) + "T" + x.End
                            date = x.Date,
                            start = x.Start,
                            end = x.End
                        };
            foreach (var v in query)
            {
                Appointment obj = new Appointment();
                obj.Start = v.start;
                obj.Date = v.date;
                obj.End = v.end;
                list.Add(obj);
            }
            return list;
        }

        public List<Notification> GetNotificationList(int id)
        {
            List<Notification> notifications = new List<Notification>();

            //Just to show Patient name in Views
            var query = from x in db.Notifications
                        where x.Doc_Id == id
                        select x;
            foreach (var notification in query)
            {
                notifications.Add(notification);
            }

            return notifications;
        }

        public List<Events> GetCalenderEvents(int id)
        {
            List<Events> events = new List<Events>();
            List<Appointment> appointments = GetAppointmentList(id);

            foreach (Appointment temp in appointments)
            {
                Events temp_event = new Events();
                temp_event.id = id.ToString();
                temp_event.title = "Event";
                temp_event.start = temp.Date.ToString("yyyy-MM-dd") + "T" + temp.Start.ToString();
                temp_event.end = temp.Date.ToString("yyyy-MM-dd") + "T" + temp.End.ToString();
                temp_event.allDay = false;
                events.Add(temp_event);
            }

            return events;
        }
        public void AddAppointment(Notification notification, int docID)
        {
            Appointment appointment = new Appointment();
            appointment.Doc_Id = docID;
            appointment.Pat_Id = (int)notification.Pat_Id;
            appointment.Date = notification.StartTime;

            db.Appointments.Add(appointment);
            db.SaveChanges();
        }
        public Full_Doctor GetFullDoctorForProfile(int? id)
        {
            Full_Doctor obj = new Full_Doctor();
            if (id != 0)
            {
                Person p = db.Persons.Find(id);
                Login l = db.Logins.Find(id);
                Doctor d = db.Doctors.Find(id);
                List<Comment> comments = db.Comments.Where(x => x.Doc_Id == id).ToList();
                List<Full_Comment> full_Comments = new List<Full_Comment>();

                obj.Name = p.Name;
                obj.Career_Start = (DateTime)d.Career_Start;
                obj.Type = d.Type;
                obj.Email = l.Email;
                obj.Phone = p.Phone;
                foreach (Comment c in comments)
                {
                    Full_Comment full_Comment = new Full_Comment();
                    full_Comment.Pat_Name = c.Patient.Person.Name;
                    full_Comment.Text = c.Text;
                    full_Comments.Add(full_Comment);
                }
                obj.Comments = full_Comments;
                return obj;
            }

            return null;
        }
        public List<Comment> GetCommentOfDoctor(int docID)
        {
            List<Comment> list = db.Comments.Where(x => x.Doc_Id == docID).ToList();
            return list;
        }
        public bool GetConfidentialStatus(int pat_id,int doc_id)
        {
            var query = from x in db.PatientDoctors
                        where x.Pat_Id == pat_id && x.Doc_Id == doc_id
                        select x;

            foreach(var item in query)
            {
                return item.Pat_Confidential;
            }
            return false;
        }

        public bool AddPrescription(Prescription Prescription, int doc_id)
        {
            Prescription p = new Prescription();
            p.Amount = Prescription.Amount;
            p.Doc_Id = doc_id;
            p.Frequency = Prescription.Frequency;
            p.Medicine_Name = Prescription.Medicine_Name;
            p.Pat_Id = Prescription.Pat_Id;
            p.Purpose = Prescription.Purpose;
            p.Type = Prescription.Type;

            db.Prescriptions.Add(p);
            db.SaveChanges();

            var path = "nothing";
            if (!GetConfidentialStatus(p.Pat_Id,doc_id))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + p.Pat_Id + @"\normal\" + "Prescriptions.txt";
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + p.Pat_Id + @"\Confidential\" + "Prescriptions.txt";
            }

            var dir = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + p.Pat_Id + @"\normal\";
            var dir_confidential = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + p.Pat_Id + @"\confidential\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!Directory.Exists(dir_confidential))
                Directory.CreateDirectory(dir_confidential);

            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sout = new StreamWriter(fs);
            try
            {
                sout.Write("Dr." + db.Doctors.Where(x => x.Person_Id == doc_id).First().Person.Name + " on " + DateTime.Now + " : " + p.Medicine_Name + " for " + p.Amount + " ," + p.Frequency + " times\n");
            }
            catch (IOException ex)
            {
                Console.Write(ex);
            }
            sout.Close();
            fs.Close();
            return true;
        }

        public List<string> GetPatientHistory(int pat_id, int doc_id)
        {
            string path = "nothing";
            List<string> history = new List<string>();
            if (!GetConfidentialStatus(pat_id, doc_id))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\normal\" + "Prescriptions.txt";
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\Confidential\" + "Prescriptions.txt";
            }

            string dir = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\normal\";
            string dir_confidential = AppDomain.CurrentDomain.BaseDirectory + @"\Patients\" + pat_id + @"\confidential\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!Directory.Exists(dir_confidential))
                Directory.CreateDirectory(dir_confidential);

            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamReader sout = new StreamReader(fs);
            try
            {
                history.Add( sout.ReadLine() );
            }
            catch (IOException ex)
            {
                Console.Write(ex);
            }
            sout.Close();
            fs.Close();

            return history;
        }

        public Prescription GetPrescription(int id)
        {
            Prescription prescription = db.Prescriptions.Find(id);
            if(prescription != null)
            {
                return prescription;
            }
            return null;
        }

        public void EditPrescription(Prescription p, int id)
        {
            var query = from x in db.Prescriptions
                        where x.Prescription_Id == id
                        select x;
            foreach(var item in query)
            {
                item.Amount = p.Amount;
                item.Frequency = p.Frequency;
                item.Medicine_Name = p.Medicine_Name;
                item.Purpose = p.Purpose;
                item.Type = p.Type;
            }

            db.SaveChanges();
        }

        public List<DateTime> GetDoctorSlots(int doc_id, string date)
        {
            List<string> ui_list = new List<string>();

            List<Appointment> a_list = GetAppointmentList(doc_id);

            //Get timespans
            TimeSpan avgtime = GetDoctor(doc_id).Average_Duration.Value;

            List<DateTime> free_slots = new List<DateTime>();
            DateTime end_time = DateTime.Parse("17:00");
            for (DateTime tmp = DateTime.Parse("9:00"); DateTime.Compare(tmp, end_time) < 0; tmp = tmp.Add(avgtime))
            {
                free_slots.Add(tmp);
            }

            DateTime temp = DateTime.Parse(date);
            var selected = a_list.Where(x => DateTime.Compare(x.Date.Date, temp.Date) == 0).ToList(); // never try to compare dates in LINQ, untill you visit their office

            foreach (var item in selected)
            {
                for (int i = 0; i < free_slots.Count(); i++)
                {
                    if (item.Date.Hour == free_slots[i].Hour)
                        free_slots.RemoveAt(i);
                }
            }
            return free_slots;
        }
        public void delete_notification(int id)
        {
            var not = db.Notifications.Where(x => x.Notification_Id == id).First();
            db.Notifications.Remove(not);
        }
    }
}