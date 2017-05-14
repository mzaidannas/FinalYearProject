using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FinalYearProject.Models;

namespace FinalYearProject.Controllers
{
    public class DoctorsApiController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/DoctorsApi
        public List<Full_Doctor> GetDoctors()
        {
            List<Full_Doctor> full_doc = new List<Full_Doctor>();
            List<Full_Comment> full_com = new List<Full_Comment>();

            var query1 = db.Comments;
            foreach (var temp1 in query1)
            {
                Full_Comment comment = new Full_Comment();
                comment.Comment_Id = temp1.Comment_Id;
                comment.Doc_Id = temp1.Doc_Id;
                comment.Date = temp1.Date.Value;
                comment.Pat_Name = temp1.Patient.Person.Name;
                comment.Rating = temp1.Rating.Value;
                comment.Text = temp1.Text;
                full_com.Add(comment);
            }
            var query = from x in db.Doctors
                        select new Full_Doctor
                        {
                            Id = x.Person_Id,
                            Type = x.Type,
                            Career_Start = x.Career_Start.Value,
                            Average_Duration = x.Average_Duration.Value,
                            Rating = x.Rating.Value,
                            Name = x.Person.Name,
                            Phone = x.Person.Phone,
                            Gender = x.Person.Gender,
                            Address = x.Person.Address,
                            Image_Path = x.Person.Image_Path,
                            //Don't try to include list in list in LINQ queries again
                        };

            foreach (var item in query)
            {
                item.Comments = full_com.Where(x => x.Doc_Id == item.Id).ToList();
                full_doc.Add(item);

            }
            return full_doc;
        }

        // GET: api/DoctorsApi/5
        [ResponseType(typeof(Doctor))]
        public async Task<IHttpActionResult> GetDoctor(int id)
        {
            Doctor doc = await db.Doctors.FindAsync(id);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }

        // PUT: api/DoctorsApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDoctor(int id, Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctor.Person_Id)
            {
                return BadRequest();
            }

            db.Entry(doctor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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
        // POST: api/DoctorsApi
        [ResponseType(typeof(List<DateTime>))]
        public async Task<IHttpActionResult> PostDoctor(Notification notification)
        {
            IDoctor doc = new DoctorRepository();
            List<DateTime> slots = doc.GetDoctorSlots(notification.Doc_Id, notification.StartTime.ToString("yyyy-MM-dd"));
            if (slots == null)
            {
                return NotFound();
            }

            return Ok(slots);
        }

    //// POST: api/DoctorsApi
    //[ResponseType(typeof(Doctor))]
    //public async Task<IHttpActionResult> PostDoctor(Doctor doctor)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }

    //    db.Doctors.Add(doctor);

    //    try
    //    {
    //        await db.SaveChangesAsync();
    //    }
    //    catch (DbUpdateException)
    //    {
    //        if (DoctorExists(doctor.Person_Id))
    //        {
    //            return Conflict();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return CreatedAtRoute("DefaultApi", new { id = doctor.Person_Id }, doctor);
    //}

    // DELETE: api/DoctorsApi/5
    [ResponseType(typeof(Doctor))]
        public async Task<IHttpActionResult> DeleteDoctor(int id)
        {
            Doctor doctor = await db.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync();

            return Ok(doctor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoctorExists(int id)
        {
            return db.Doctors.Count(e => e.Person_Id == id) > 0;
        }
    }
}