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
    public class ConfigurationsController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: api/Configurations
        public IQueryable<Configuration> GetConfigurations()
        {
            return db.Configurations;
        }

        // GET: api/Configurations/5
        [ResponseType(typeof(Configuration))]
        public IHttpActionResult GetConfiguration(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            return Ok(configuration);
        }

        // PUT: api/Configurations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConfiguration(int id, Configuration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configuration.Lookup_ID)
            {
                return BadRequest();
            }

            db.Entry(configuration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigurationExists(id))
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

        // POST: api/Configurations
        [ResponseType(typeof(Configuration))]
        public IHttpActionResult PostConfiguration(Configuration configuration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Configurations.Add(configuration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = configuration.Lookup_ID }, configuration);
        }

        // DELETE: api/Configurations/5
        [ResponseType(typeof(Configuration))]
        public IHttpActionResult DeleteConfiguration(int id)
        {
            Configuration configuration = db.Configurations.Find(id);
            if (configuration == null)
            {
                return NotFound();
            }

            db.Configurations.Remove(configuration);
            db.SaveChanges();

            return Ok(configuration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConfigurationExists(int id)
        {
            return db.Configurations.Count(e => e.Lookup_ID == id) > 0;
        }
    }
}