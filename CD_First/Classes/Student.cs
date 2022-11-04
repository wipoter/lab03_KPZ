using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First.Classes
{
    public class Student
    {
        [ForeignKey("Account")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> Age { get; set; }
        public string Institute { get; set; }
        public string Group { get; set; }
        public Nullable<int> Num { get; set; }

        public Nullable<int> AccountId { get; set; }
        public virtual Account Account { get; set; }

        public ICollection<Achievement> Achievement { get; set; }

        public Student() => Achievement = new HashSet<Achievement>();
    }
}
