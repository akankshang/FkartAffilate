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

    public class ConfigurationResponse : APIResponse
    {
        public List<Configuration> configurations { get; set; }
    }

    public class SubscriptionResponse : APIResponse
    {
        public List<Subscription> Subscription { get; set; }
    }

    public class SubscriptionRequest  
    {
        public List<Subscription> Subscription { get; set; }
    }

    [Table("Subscription")]
    public partial class Subscription
    {
        [Key]
        public int Subscription_ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Mobile_No { get; set; }

        public string Message { get; set; }

        public DateTime Subscription_Date { get; set; }

        public decimal Amount { get; set; }

        public bool Is_Completed { get; set; }

        public bool DTS { get; set; }
    }
}
