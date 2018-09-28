using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OldAPI.Models;

namespace OldAPI.Controllers
{
    public class User_SessionController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: api/User_Session
        public IQueryable<User_Session> GetUser_Session()
        {
            return db.User_Session;
        }

        // GET: api/User_Session/5
        [ResponseType(typeof(User_Session))]
        public IHttpActionResult GetUser_Session(long id)
        {
            User_Session user_Session = db.User_Session.Find(id);
            if (user_Session == null)
            {
                return NotFound();
            }

            return Ok(user_Session);
        }

        // PUT: api/User_Session/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser_Session(long id, User_Session user_Session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user_Session.Token_ID)
            {
                return BadRequest();
            }

            db.Entry(user_Session).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_SessionExists(id))
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

        // POST: api/User_Session
        [ResponseType(typeof(User_Session))]
        public IHttpActionResult PostUser_Session(User_Session user_Session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User_Session.Add(user_Session);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user_Session.Token_ID }, user_Session);
        }

        // DELETE: api/User_Session/5
        [ResponseType(typeof(User_Session))]
        public IHttpActionResult DeleteUser_Session(long id)
        {
            User_Session user_Session = db.User_Session.Find(id);
            if (user_Session == null)
            {
                return NotFound();
            }

            db.User_Session.Remove(user_Session);
            db.SaveChanges();

            return Ok(user_Session);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool User_SessionExists(long id)
        {
            return db.User_Session.Count(e => e.Token_ID == id) > 0;
        }
    }
}