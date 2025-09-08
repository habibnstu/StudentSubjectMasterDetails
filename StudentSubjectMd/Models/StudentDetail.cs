using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentSubjectMd.Models
{
    public class StudentDetail
    {
        public int StudentDetailId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}