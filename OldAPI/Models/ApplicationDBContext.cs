namespace OldAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
            : base("name=ApplicationDBContext")
        {
        }

        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<User_Session> User_Session { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
