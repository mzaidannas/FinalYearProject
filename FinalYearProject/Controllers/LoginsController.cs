using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FinalYearProject.Models;

namespace FinalYearProject.Controllers
{
    public class LoginsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        public LoginsController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Logins
        public IQueryable<Login> GetLogins()
        {
            return db.Logins;
        }

        // GET: api/Logins/5
        [ResponseType(typeof(Login))]
        public async Task<IHttpActionResult> GetLogin(int id)
        {
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/Logins/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLogin(int id, Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.Person_Id)
            {
                return BadRequest();
            }

            db.Entry(login).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Logins
        //[ResponseType(typeof(Login))]
        //public async Task<IHttpActionResult> PostLogin(Login login)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Logins.Add(login);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (LoginExists(login.Person_Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = login.Person_Id }, login);
        //}

        // POST: api/Logins
        [ResponseType(typeof(Full_Patient))]
        public async Task<IHttpActionResult> PostLogin(Login login)
        {
            Person patient = await db.Persons.SingleAsync(e => e.Login.Username == login.Username && e.Login.Password == login.Password);
            Full_Patient temp = new Full_Patient();
            temp.Id = patient.Person_Id;
            temp.Username = patient.Login.Username;
            temp.Password = patient.Login.Password;
            temp.Email = patient.Login.Email;
            temp.Name = patient.Name;
            temp.CNIC = patient.CNIC;
            temp.DOB = patient.DOB;
            temp.Gender = patient.Gender;
            temp.Phone = patient.Phone;
            temp.Address = patient.Address;
            temp.Image_Path = patient.Image_Path;
            temp.Blood_Group = patient.Patient.Blood_Group;
            temp.Insurance_No = patient.Patient.Insurance_No;

            return CreatedAtRoute("DefaultApi", new { id = login.Person_Id }, temp);
        }

        // DELETE: api/Logins/5
        [ResponseType(typeof(Login))]
        public async Task<IHttpActionResult> DeleteLogin(int id)
        {
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            db.Logins.Remove(login);
            await db.SaveChangesAsync();

            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginExists(int id)
        {
            return db.Logins.Count(e => e.Person_Id == id) > 0;
        }
    }
}