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
                else
                {
                    objResponse.Message = "Invalid login credentials.";
                    objResponse.Status = "0";
                }

            }
            else
            {
                objResponse.Message = "Object is null.";
                objResponse.Status = "0";
            }
            return objResponse;
        }

        [HttpPost]
        public APIResponse Register(Register register)
        {
            APIResponse objResponse = new APIResponse();
            if (register != null)
            {
                var objuser = db.Users.FirstOrDefault(x => x.Email == register.Email);
                if (objuser == null)
                {
                    objuser = new User();
                    objuser.Email = register.Email;
                    objuser.Is_Active = true;
                    objuser.Is_Subscribed = false;
                    objuser.Name = register.Full_Name;
                    objuser.Password = register.Password;
                    objuser.Device_ID = "";

                    db.Users.Attach(objuser);
                    db.Entry(objuser).State = EntityState.Added;
                    db.SaveChanges();
                    NotificationHelper.SendRegistrationEmail(objuser.Email, objuser.Name);
                    objResponse.Message = "Registration Successfull, Please login";
                    objResponse.Status = "1";

                }
                else
                {
                    objResponse.Message = "Email is already registered, Please login";
                    objResponse.Status = "0";
                }

            }
            else
            {
                objResponse.Message = "Object is null";
                objResponse.Status = "0";
            }
            return objResponse;
        }


        [HttpPost]
        public APIResponse ForgotPassword(ForgotPassword forgetPassword)
        {
            APIResponse objResponse = new APIResponse();
            if (forgetPassword != null)
            {
                var objuser = db.Users.FirstOrDefault(x => x.Email == forgetPassword.Email);
                if (objuser != null)
                {
                    objuser.Password = GNF.RandomPassword(4);
                    db.Users.Attach(objuser);
                    db.Entry(objuser).State = EntityState.Modified;
                    db.SaveChanges();

                    NotificationHelper.SendForgetPasswordEmail(objuser.Email, objuser.Password);

                    objResponse.Message = "Please check your email inbox for new login code. Thank you";
                    objResponse.Status = "1";

                }

            }
            else
            {
                objResponse.Message = "This email is not registered with us";
                objResponse.Status = "0";
            }
            return objResponse;
        }

        [HttpPost]
        public APIResponse ChangePassword(ChangePassword changePassword)
        {
            APIResponse objResponse = new APIResponse();
            if (changePassword != null)
            {
                if (changePassword.New_Password == changePassword.Confirm_Password)
                {
                    var objuser = db.Users.FirstOrDefault(x => x.Email == changePassword.Email && x.Password == changePassword.old_Password);
                    if (objuser != null)
                    {
                        objuser.Password = changePassword.New_Password;
                        db.Users.Attach(objuser);
                        db.Entry(objuser).State = EntityState.Modified;
                        db.SaveChanges();

                        EmailHelper.SendMail(objuser.Email, "Password Changed Successfully", "Your <b>Affilate URL Builder App</b> password updated successfully", true);

                        objResponse.Message = "Passowrd updated successfully. Thank you";
                        objResponse.Status = "1";

                    }
                    else
                    {
                        objResponse.Message = "Old password is not correct";
                        objResponse.Status = "0";
                    }
                }
                else
                {
                    objResponse.Message = "Confirm password is not matched";
                    objResponse.Status = "0";
                }

            }
            else
            {
                objResponse.Message = "Some values are null.";
                objResponse.Status = "0";
            }
            return objResponse;
        }



    }
}