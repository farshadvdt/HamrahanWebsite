

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamrahan.Models
{
    public partial class Person:IdentityUser
    {

        public Person()
        {
            Courses = new HashSet<Course>();
            SalaryPayments = new HashSet<SalaryPayment>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentPayments = new HashSet<StudentPayment>();
            PersonPosts = new HashSet<Post>();
        

        }

            //public string ID { get; set; }
            public string FirstName { get; set; }
            public string Lastname { get; set; }
            public string NationalCode { get; set; }
            public DateTime Birthdate { get; set; }
            //public string Telephone { get; set; }
            public string TelePhone { get; set; }
            public string Address { get; set; }
            public string FullName { get; set; }
            public byte EducationGradeCode { get; set; }
            public int? Age { get; set; }
            public bool Gender { get; set; }

            //public byte RoleCode { get; set; }

            //public virtual Role RoleCodeNavigation { get; set; }

        [ForeignKey("EducationGradeCode")]
        public virtual EducationGrade EducationGradeNavigation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SalaryPayment> SalaryPayments { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
        public virtual ICollection<Post> PersonPosts{ get; set; }

     
    }
}
