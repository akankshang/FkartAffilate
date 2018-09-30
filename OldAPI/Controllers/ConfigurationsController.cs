using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using OldAPI.Models;

namespace OldAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ConfigurationsController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

      
       /// <summary>
       /// 
       /// </summary>
       /// <param name="LookupType"></param>
       /// <param name="Login_Key"></param>
       /// <param name="UserId"></param>
       /// <returns></returns>
        [ResponseType(typeof(Configuration))]
        public ConfigurationResponse GetConfiguration(string LookupType, string Login_Key, int UserId)
        {
            ConfigurationResponse objResponse = new ConfigurationResponse();
            if (!string.IsNullOrEmpty(Login_Key))
            {

                objResponse.configurations = db.Configurations.Where(x => x.Lookup_Type == LookupType && x.User_Id == UserId).ToList();
                objResponse.Message = objResponse.configurations.Count + " Configuration setting found";
                objResponse.Status = "1";
            }
            else
            {
                objResponse.configurations = null;
                objResponse.Message = "Invalid login key";
                objResponse.Status = "0";
            }
            return objResponse;
        }

    

       
        public APIResponse PostConfiguration(List<Configuration> configuration, string Login_Key, int UserId)
        {
            APIResponse objResponse = new APIResponse();
            if (!string.IsNullOrEmpty(Login_Key))
            {
                if (configuration != null)
                {
                    foreach (Configuration obj in configuration)
                    {

                        db.Configurations.Attach(obj);
                        db.Entry(obj).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                   

                }
                else
                {
                    objResponse.Message = "Configuration list is null.";
                    objResponse.Status = "0";
                }
            }
            else
            {
                objResponse.Message = "Invalid login key";
                objResponse.Status = "0";
            }
            return objResponse;
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