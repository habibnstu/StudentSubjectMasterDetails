namespace StudentSubjectMd.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StudentSubjectMd.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSubjectMd.Models.StudentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentSubjectMd.Models.StudentDbContext context)
        {
            context.Subjects.AddOrUpdate(
                s=>s.SubjectName,
                new Subject { SubjectName = "Bangla"},
                new Subject { SubjectName = "English"},
                new Subject { SubjectName = "General Math" },
                new Subject { SubjectName = "General Science" },
                new Subject { SubjectName = "Chemistry" },
                new Subject { SubjectName = "Physics" },
                new Subject { SubjectName = "Higher Math" },
                new Subject { SubjectName = "Biology" },
                new Subject { SubjectName = "Agriculture" },
                new Subject { SubjectName = "Social Science" },
                new Subject { SubjectName = "ICT" }
                );
        }
    }
}
