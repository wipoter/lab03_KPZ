using Code_First.Classes;
using System;
using System.Data.Entity;
using System.Linq;

namespace CD_First
{
    public class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Achievement> Achievement { get; set; }
        public virtual DbSet<Student> Student { get; set; }
    }
}