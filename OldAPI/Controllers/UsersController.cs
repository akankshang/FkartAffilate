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

    public class UsersController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        /// <summary>
        /// Login API
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginResponse Login(LoginRequest login)
        {
            LoginResponse objResponse = new LoginResponse();
            if (login != null)
            {
                var objuser = db.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
                if (objuser != null)
                {
                    User_Session objSession = new User_Session();
                    objSession.User_ID = objuser.User_ID;
                    objSession.Session_GUID = (new Guid()).ToString();
                    objSession.Token_Key = objSession.Session_GUID;
                    objSession.Issued_On = DateTime.Now;
                    objSession.IP_Address = login.Ip_Address;
                   // objSession.Expired = false;//Fix
                    objSession.Expired_On = DateTime.Now.AddDays(1);

                    db.User_Session.Attach(objSession);
                    db.Entry(objSession).State = EntityState.Added;
                    db.SaveChanges();

                    objResponse.UserSession = objSession;
                    objResponse.Message = "Success";
                    objResponse.Status = "1";

                }

            }
            else
            {
                objResponse.Message = "Login object is null. Please send valid API request.";
                objResponse.Status = "0";
            }
            return objResponse;
        }

    }
}