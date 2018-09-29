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
using System.Web.Mvc;

namespace OldAPI.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        public LoginResponse Login(LoginRequest login)
        {
            LoginResponse objResponse = new LoginResponse();
            if(login != null)
            {
				var objuser = db.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
				if (objuser != null)
				{
					LoginRequest objLogin = new LoginRequest();
					objLogin = db.LoginRequest.FirstOrDefault(x => x.Device_Id == objuser.Device_Id);

				if (objLogin == null)
				{
						objLogin = new Device_Id();
						objLogin.Ip_Address = objuser.Ip_Address;
						objLogin.Device_Id = objuser.Device_Id;
						objLogin.Email = objuser.Email;
						objLogin.Password = objuser.Password;
					db.LoginRequest.Attach(objLogin);
					db.Entry(objLogin).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
				}
			}
					
			}
            else
            {
                objResponse.Message = "Login object is null. Please send valid API request.";
                objResponse.Status = "0";
            }
            return objResponse;
        }
		public ActionResult Register()
		{
			return View();

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Users(Register Register)
		{

			if (ModelState.IsValid)
			{
				var objRes = db.Users.FirstOrDefault(x => x.Email == Register.Email);
				if (objRes == null)
				{


					objRes = new objRes { Email = Register.Email, Password = Register.Password };

					db.Users.Add(objRes);
					db.SaveChanges();

					objRe = db.Users.FirstOrDefault(x => x.Email == objRes.Email);
					int? HRM_UsersId = objRes.Entity_Id;
					if (HRM_UsersId > 0)
					{
						HRM_Entity objhrm_Entity = new HRM_Entity();
						objhrm_Entity.First_Name = model.First_Name;
						objhrm_Entity.Last_Name = model.Last_Name;
						objhrm_Entity.Email = model.Email;
						objhrm_Entity.Mobile_No = model.Mobile_No;
						objhrm_Entity.Entity_Id = Convert.ToInt32(HRM_UsersId);

						//objhrm_entity.entity_type = eentitytype.user.tostring();

						//objhrm_Entity.Is_deleted = false;

						db.HRM_Entity.Add(objhrm_Entity);
						db.SaveChanges();

						return RedirectToAction("Login");
					}
				}
				else
				{
					ModelState.AddModelError("Email", "This Email is already Registered with us");
				}

			}

			// If we got this far, something failed, redisplay form
			return View(model);

		}


		//// GET: api/Users
		//public IQueryable<User> GetUsers()
		//{
		//    return db.Users;
		//}

		//// GET: api/Users/5
		//[ResponseType(typeof(User))]
		//public IHttpActionResult GetUser(int id)
		//{
		//    User user = db.Users.Find(id);
		//    if (user == null)
		//    {
		//        return NotFound();
		//    }

		//    return Ok(user);
		//}

		//// PUT: api/Users/5
		//[ResponseType(typeof(void))]
		//public IHttpActionResult PutUser(int id, User user)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    if (id != user.User_ID)
		//    {
		//        return BadRequest();
		//    }

		//    db.Entry(user).State = EntityState.Modified;

		//    try
		//    {
		//        db.SaveChanges();
		//    }
		//    catch (DbUpdateConcurrencyException)
		//    {
		//        if (!UserExists(id))
		//        {
		//            return NotFound();
		//        }
		//        else
		//        {
		//            throw;
		//        }
		//    }

		//    return StatusCode(HttpStatusCode.NoContent);
		//}

		//// POST: api/Users
		//[ResponseType(typeof(User))]
		//public IHttpActionResult PostUser(User user)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return BadRequest(ModelState);
		//    }

		//    db.Users.Add(user);
		//    db.SaveChanges();

		//    return CreatedAtRoute("DefaultApi", new { id = user.User_ID }, user);
		//}

		//// DELETE: api/Users/5
		//[ResponseType(typeof(User))]
		//public IHttpActionResult DeleteUser(int id)
		//{
		//    User user = db.Users.Find(id);
		//    if (user == null)
		//    {
		//        return NotFound();
		//    }

		//    db.Users.Remove(user);
		//    db.SaveChanges();

		//    return Ok(user);
		//}

		//protected override void Dispose(bool disposing)
		//{
		//    if (disposing)
		//    {
		//        db.Dispose();
		//    }
		//    base.Dispose(disposing);
		//}

		//private bool UserExists(int id)
		//{
		//    return db.Users.Count(e => e.User_ID == id) > 0;
		//}
	}
}