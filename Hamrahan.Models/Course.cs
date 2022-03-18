using System;
using System.Collections.Generic;

namespace Hamrahan.Models
{
    public partial class Course
    {
        public long ID { get; set; }
        public int LessonCode { get; set; }
        public string TeacherID { get; set; }
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
