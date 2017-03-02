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
    public class NotificationsController : ApiController
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: api/Notifications
        public IQueryable<Notification> GetNotifications()
        {
            return db.Notifications;
        }

        // GET: api/Notifications/5
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> GetNotification(int id)
        {
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        // PUT: api/Notifications/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNotification(int id, Notification notification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notification.Notification_Id)
            {
                return BadRequest();
            }

            db.Entry(notification).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationExists(id))
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

        // POST: api/Notifications
        [ResponseType(typeof(KeyValuePair<string,string>))]
        public async Task<IHttpActionResult> PostNotification(Notification notification)
        {
            if(notification.Pat_Id == -1)
            {
                notification.Pat_Id = null;
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notifications.Add(notification);
            await db.SaveChangesAsync();

            return Ok( new KeyValuePair<string, string>("Responce","Request accepted at " + notification.StartTime.ToString("yyyy-MM-dd hh:mm:ss tt") ) );
        }

        // DELETE: api/Notifications/5
        [ResponseType(typeof(Notification))]
        public async Task<IHttpActionResult> DeleteNotification(int id)
        {
            Notification notification = await db.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            db.Notifications.Remove(notification);
            await db.SaveChangesAsync();

            return Ok(notification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NotificationExists(int id)
        {
            return db.Notifications.Count(e => e.Notification_Id == id) > 0;
        }
    }
}