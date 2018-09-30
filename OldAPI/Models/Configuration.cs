namespace OldAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration")]
    public partial class Configuration
    {
        [Key]
        public int Lookup_ID { get; set; }

        [StringLength(50)]
        public string Lookup_Type { get; set; }

        [StringLength(50)]
        public string Lookup_Name { get; set; }

        [StringLength(500)]
        public string Lookup_Value { get; set; }

        public int User_Id { get; set; }
    }

    public class ConfigurationResponse :APIResponse
    {
        public List<Configuration> configurations { get; set; }
    }
}
