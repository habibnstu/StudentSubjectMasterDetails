using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentSubjectMd.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required, StringLength(50), Display(Name ="Student Name")]
        public string StudentName { get; set; }
        [Required, StringLength(50), Display(Name = "Student Roll")]
        public string StudentRoll { get; set; }
        public string Group { get; set; }
        [Required, StringLength(50), Display(Name = "Student Class")]
        public string StudentClass  { get; set; }
        public bool IsRegural { get; set; }
        [Display(Name = "Student Phone")]
        public string StudentPhone { get; set; }
        [Display(Name = "Guardian Phone")]
        public string GuardianPhone { get; set; }
        [Display(Name = "Student Address")]
        public string StudentAddress { get; set; }
        public string Image { get; set; }    
        [Display(Name = "Admission Fee")]
        public decimal AdmissionFee { get; set; }
        [Column(TypeName ="date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true) ,Display(Name = "Admission Date")]
        public DateTime AdmissionDate { get; set; }
        public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();

    }
}