using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSubjectMd.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();
    }
}