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
    public class PrescriptionsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        public PrescriptionsController()
        {
            //db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Prescriptions
        public IQueryable<Prescription> GetPrescriptions()
        {
            return db.Prescriptions;
        }

        // GET: api/Prescriptions/5
        [ResponseType(typeof(List<Full_Perscription>))]
        public async Task<IHttpActionResult> GetPrescription(int id)
        {
            var query = from x in db.Prescriptions
                        where x.Pat_Id == id
                        select new Full_Perscription
                        {
                            Id = x.Prescription_Id,
                            Doctor_Name = x.Doctor.Person.Name,
                            Patient_Name = x.Patient.Person.Name,
                            Prescription_Name = x.Medicine_Name,
                            Type = x.Type,
                            Frequency = x.Frequency,
                            Amount = x.Amount 
                        };
            List<Full_Perscription> perscriptions = new List<Full_Perscription>();
            foreach(var full_Perscription in query)
            {
                perscriptions.Add(full_Perscription);
            }
            //Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (perscriptions.Count == 0)
            {
                return NotFound();
            }
            return Ok(perscriptions);
        }

        // PUT: api/Prescriptions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrescription(int id, Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prescription.Prescription_Id)
            {
                return BadRequest();
            }

            db.Entry(prescription).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrescriptionExists(id))
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

        // POST: api/Prescriptions
        [ResponseType(typeof(Prescription))]
        public async Task<IHttpActionResult> PostPrescription(Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Prescriptions.Add(prescription);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = prescription.Prescription_Id }, prescription);
        }

        // DELETE: api/Prescriptions/5
        [ResponseType(typeof(Prescription))]
        public async Task<IHttpActionResult> DeletePrescription(int id)
        {
            Prescription prescription = await db.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            db.Prescriptions.Remove(prescription);
            await db.SaveChangesAsync();

            return Ok(prescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrescriptionExists(int id)
        {
            return db.Prescriptions.Count(e => e.Prescription_Id == id) > 0;
        }
    }
}