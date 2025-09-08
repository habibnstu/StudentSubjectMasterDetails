namespace StudentSubjectMd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentDetails",
                c => new
                    {
                        StudentDetailId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentDetailId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        StudentRoll = c.String(nullable: false, maxLength: 50),
                        Group = c.String(),
                        StudentClass = c.String(nullable: false, maxLength: 50),
                        IsRegural = c.Boolean(nullable: false),
                        StudentPhone = c.String(),
                        GuardianPhone = c.String(),
                        StudentAddress = c.String(),
                        Image = c.String(),
                        AdmissionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdmissionDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                    })
                .PrimaryKey(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDetails", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentDetails", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentDetails", new[] { "SubjectId" });
            DropIndex("dbo.StudentDetails", new[] { "StudentId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Students");
            DropTable("dbo.StudentDetails");
        }
    }
}
