using System;
using System.Collections.Generic;

namespace WebAppP3PP3.Models
{
    public partial class Student
    {
        public string? StuName { get; set; }
        public int StuRollNo { get; set; }
        public int? Cid { get; set; }
        public int? SubjId { get; set; }

        public virtual Class? CidNavigation { get; set; }
        public virtual Subject? Subj { get; set; }
    }
}
