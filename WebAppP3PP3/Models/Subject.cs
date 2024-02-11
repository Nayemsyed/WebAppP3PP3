using System;
using System.Collections.Generic;

namespace WebAppP3PP3.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Students = new HashSet<Student>();
        }

        public int SubId { get; set; }
        public string? SubName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
