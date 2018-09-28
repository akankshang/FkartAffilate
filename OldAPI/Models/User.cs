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
        

    }

    public class LoginResponse : APIResponse
    {

        public User_Session UserSession { get; set; }
    }
}
