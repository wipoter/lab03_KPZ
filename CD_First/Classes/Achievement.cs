using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_First.Classes
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Describtion { get; set; }

        public Nullable<int> StudentId { get; set; }
        public Student Student { get; set; }
    }
}
