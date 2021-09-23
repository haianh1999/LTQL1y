using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LTQL11.Models
{
    public partial class LaptrinhquanlyDBcontext : DbContext
    {
        public LaptrinhquanlyDBcontext()
            : base("name=LaptrinhquanlyDBcontext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
              .Property(e => e.Usename)
              .IsUnicode(false);
        }
    }
}
