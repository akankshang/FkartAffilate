namespace OldAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        public int User_ID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool? Is_Active { get; set; }

        public bool? Is_Subscribed { get; set; }

        [StringLength(256)]
        public string Device_ID { get; set; }

        public DateTime? Last_Login { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
    }

    public class APIResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }

    public class LoginRequest
    {
        public string Ip_Address { get; set; }
		public string Device_Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
    }

    public class LoginResponse : APIResponse
    {

        public User_Session UserSession { get; set; }
    }
	public class ForgotPassword
	{
		public string Email { get; set; }
	}
	public class ChangePassword : ForgotPassword
	{
        public string old_Password { get; set; }
		public string New_Password { get; set; }
		public string Confirm_Password { get; set; }
	}
	public class Register : ForgotPassword
	{
		public string Full_Name { get; set; }
		public string Password { get; set; }
	}
	
	public class GetConfigRequest
	{
		public string Lookup_type { get; set; }
		public string Login_key { get; set; }
	}
	public class Response : APIResponse
	{
		public string Lookup_Name { get; set; }
		public string Lookup_Value { get; set; }
	}
	public class PostConfig : Response
	{
		public string Token_key { get; set; }
		public string Lookup_Type { get; set; }
	}
}
