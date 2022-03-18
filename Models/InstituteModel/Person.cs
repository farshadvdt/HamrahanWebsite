using FromDbModels.Context;
using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Person
    {
        public Person()
        {
            Courses = new HashSet<Course>();
            SalaryPayments = new HashSet<SalaryPayment>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentPayments = new HashSet<StudentPayment>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string NationalCode { get; set; }
        public DateTime Birthdate { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public byte RoleCode { get; set; }
        public string FullName { get; set; }
        public byte EducationGradecode { get; set; }
        public int? Age { get; set; }
        public bool Gender { get; set; }

        public virtual EducationGrade EducationGradecodeNavigation { get; set; }
        public virtual Role RoleCodeNavigation { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SalaryPayment> SalaryPayments { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
    }
}
