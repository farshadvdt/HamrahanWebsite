using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
            StudentPayments = new HashSet<StudentPayment>();
        }

        public long Id { get; set; }
        public int LessonCode { get; set; }
        public long TeacherId { get; set; }
        public byte ClassCode { get; set; }
        public string Title { get; set; }
        public string TimeInWeek { get; set; }
        public DateTime? StartingDay { get; set; }
        public decimal Fee { get; set; }

        public virtual Class ClassCodeNavigation { get; set; }
        public virtual Lesson LessonCodeNavigation { get; set; }
        public virtual Person Teacher { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
    }
}
