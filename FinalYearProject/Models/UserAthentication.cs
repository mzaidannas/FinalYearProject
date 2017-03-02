using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace FinalYearProject.Models
{
    public class UserAthentication : IUser
    {
        DatabaseEntities db;

        public UserAthentication()
        {
            db = new DatabaseEntities();
        }
        public bool Exists(Login user)
        {
            var query = from x in db.Logins
                        where user.Username == x.Username
                        select x;
            foreach(var u in query)
            {
                return true;
            }
            return false;
        }
        public bool Athenticate(Login user)
        {
            var query = from x in db.Logins
                        where user.Username == x.Username && user.Password == x.Password
                        select x;
            foreach (var v in query)
            {
                return true;
            }
            return false;
        }
        public bool isDoctor(Login user)
        {   
            var query = from login in db.Logins
                        join d in db.Doctors on login.Person_Id equals d.Person_Id
                        where login.Username == user.Username && login.Password == user.Password
                        select login;
            foreach (var v in query)
            {
                return true;
            }
            return false;
        }
        public void AddUser(Login user)
        {
            db.Logins.Add(user);
            db.SaveChanges();
        }
        public void RegisterDoctor(MainPerson mainPerson)
        {
            Login login = new Login();
            Person person = new Person();
            Doctor doctor = new Doctor();
            //Patient patient = new Patient();

            login.Password = mainPerson.Password;
            login.Email = mainPerson.Email;
            login.Username = mainPerson.UserName;

            person.Name = mainPerson.Name;
            person.CNIC = mainPerson.CNIC;
            person.Address = mainPerson.Address;
            person.DOB = mainPerson.DOB;
            person.Gender = mainPerson.Gender;
            person.Phone = mainPerson.Phone_no;

            //doctor.Average_Duration = int.Parse(mainPerson.Average_Duration);
            doctor.Average_Duration = TimeSpan.FromMinutes(mainPerson.Average_Duration);
            doctor.Career_Start = mainPerson.Career_Start;
            doctor.Type = mainPerson.Type;
            doctor.Rating = 0;

            //patient.Blood_Group = mainPerson.Blood_Group;
            //patient.Insurance_No = mainPerson.Insurance_No; 

            db.Logins.Add(login);
            db.Persons.Add(person);
            db.Doctors.Add(doctor);
            
            db.SaveChanges();
            
        }
        public void DeleteUser(Login user)
        {
            db.Logins.Remove(user);
            db.SaveChanges();
        }
        public List<Login> GetLogins()
        {
            List<Login> logins = new List<Login>();
            var query = from x in db.Logins
                        select x;
            foreach (var v in query)
            {
                logins.Add(v);
            }
            return logins;
        }
        public int GetPersonID(string userName, string password)
        {
            var query = from l in db.Logins
                        join p in db.Persons on l.Person_Id equals p.Person_Id
                        where (l.Username == userName && l.Password == password)
                        select new
                        {
                            Id = l.Person_Id
                        };
            int Id = 0;
            foreach (var v in query)
            {
                Id = v.Id;
                return v.Id;
            }
            return Id;
        }

        public string GetPassword(string email)
        {
            var query = from l in db.Logins
                        where l.Email == email
                        select l.Password;

            foreach (var v in query)
            {
                return v;
            }
            return "";
        }
    }
}