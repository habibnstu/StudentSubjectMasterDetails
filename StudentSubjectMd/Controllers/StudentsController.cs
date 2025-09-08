using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using StudentSubjectMd.Models;
using StudentSubjectMd.Models.ViewModels;

namespace StudentSubjectMd.Controllers
{
  
    public class StudentsController : Controller
    {
        StudentDbContext db = new StudentDbContext();
        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students
                            .Include(sd => sd.StudentDetails.Select(sub => sub.Subject))
                            .OrderByDescending(s => s.StudentId)
                            .ToList();
            return View(students);
        }

        public ActionResult Create()
        {
            return PartialView("_create");
        }

        public ActionResult AddSubject(int? id)
        {
            ViewBag.subject = new SelectList(db.Subjects.ToList(), "SubjectId", "SubjectName", id ?? 0);
            return PartialView("_addSubject");
        }

        [HttpPost]
        public ActionResult Create(StudentVM studentVM, int[] subjectid)
        {
      
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    StudentName=studentVM.StudentName,
                    StudentRoll=studentVM.StudentRoll,
                    Group=studentVM.Group,
                    IsRegural=studentVM.IsRegural,
                    StudentClass=studentVM.StudentClass,
                    StudentPhone=studentVM.StudentPhone,
                    GuardianPhone=studentVM.GuardianPhone,
                    StudentAddress=studentVM.StudentAddress,
                    AdmissionDate=studentVM.AdmissionDate,
                    AdmissionFee=studentVM.AdmissionFee
                };
                //image

                HttpPostedFileBase file = studentVM.ImageFile;
                if (file != null)
                {
                    var ext = Path.GetExtension(file.FileName);
                    string fileName = Path.Combine("/Images/" + DateTime.Now.Ticks.ToString() + ext);
                    file.SaveAs(Server.MapPath(fileName));
                    student.Image = fileName;
                }
                db.Students.Add(student);
                db.SaveChanges();
                //Subject

                foreach (var sId in subjectid)
                {
                    var sDetail = new StudentDetail()
                    {
                        StudentId = student.StudentId,
                        Student = student,
                        SubjectId = sId
                    };
                    db.StudentDetails.Add(sDetail);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentVM);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            var details = db.StudentDetails
                           .Include(s => s.Subject)
                           .Where(x => x.StudentId == students.StudentId)
                           .ToList();
            var sObj = new StudentVM
            {
                StudentId = students.StudentId,
                StudentName = students.StudentName,
                StudentRoll = students.StudentRoll,
                Group = students.Group,
                IsRegural = students.IsRegural,
                StudentClass = students.StudentClass,
                StudentPhone = students.StudentPhone,
                GuardianPhone = students.GuardianPhone,
                StudentAddress = students.StudentAddress,
                AdmissionDate = students.AdmissionDate,
                AdmissionFee = students.AdmissionFee,
                Image = students.Image,
                StudentDetails = details
            };

            return PartialView("_edit", sObj);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM studentVM, int[] subjectid)
        {
            if (ModelState.IsValid)
            {
          
            var students = db.Students.Find(studentVM.StudentId);
            if (students == null)
            {
                return HttpNotFound();
            }
            var details = db.StudentDetails
                           .Include(s => s.Subject)
                           .Where(x => x.StudentId == students.StudentId)
                           .ToList();


            students.StudentName = studentVM.StudentName;
            students.StudentRoll = studentVM.StudentRoll;
            students.Group = studentVM.Group;
            students.IsRegural = studentVM.IsRegural;
            students.StudentClass = studentVM.StudentClass;
            students.StudentPhone = studentVM.StudentPhone;
            students.GuardianPhone = studentVM.GuardianPhone;
            students.StudentAddress = studentVM.StudentAddress;
            students.AdmissionDate = studentVM.AdmissionDate;
            students.AdmissionFee = studentVM.AdmissionFee;
            //image

            HttpPostedFileBase file = studentVM.ImageFile;
            var oldImage = studentVM.Image;
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                string fileName = Path.Combine("/Images/" + DateTime.Now.Ticks.ToString() + ext);
                file.SaveAs(Server.MapPath(fileName));
                students.Image = fileName;
            }
            else
            {
                students.Image = oldImage;
            }
            //db.Students.Add(students);
            //db.SaveChanges();

            //Subject
            db.StudentDetails.RemoveRange(details);
            foreach (var sId in subjectid)
            {
                var sDetail = new StudentDetail()
                {
                    StudentId = students.StudentId,
                    Student = students,
                    SubjectId = sId
                };
                db.StudentDetails.Add(sDetail);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
               
            }
            return View(studentVM);
        }

        public  ActionResult Delete (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            var details = db.StudentDetails
                           .Include(s => s.Subject)
                           .Where(x => x.StudentId == students.StudentId)
                           .ToList();

            if (details.Any())
            {
                db.StudentDetails.RemoveRange(details);
            }
            db.Students.Remove(students);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}