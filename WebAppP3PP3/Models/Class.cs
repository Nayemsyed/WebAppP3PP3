using System;
using System.Collections.Generic;

namespace WebAppP3PP3.Models
{
    public partial class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        public int Cid { get; set; }
        public string? Cname { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
