namespace OldAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Session
    {
        [Key]
        public long Token_ID { get; set; }

        public int? User_ID { get; set; }

        [StringLength(40)]
        public string IP_Address { get; set; }

        [StringLength(256)]
        public string Token_Key { get; set; }

        [StringLength(36)]
        public string Session_GUID { get; set; }

        public DateTime? Issued_On { get; set; }

        public DateTime? Expired { get; set; }

        public DateTime? Expired_On { get; set; }
    }
}
