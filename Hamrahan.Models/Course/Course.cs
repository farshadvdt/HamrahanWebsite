using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hamrahan.Models
{
    public partial class Course
    {
        public long ID { get; set; }
        public int CourseGroupCode { get; set; }
        public string TeacherID { get; set; }
        public byte ClassCode { get; set; }
        public string Title { get; set; }
        public string CourseDescription { get; set; }
        public string TimeInWeek { get; set; }
        public DateTime? StartingDay { get; set; }
        public int CoursePrice { get; set; }
        public string Keyword{ get; set; }
        public string CourseImageName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Class ClassCodeNavigation { get; set; }
        [ForeignKey("CourseGroupCode")]
        public virtual CourseGroup CourseGroupCodeNavigation { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Person Teacher { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
        public virtual ICollection<StudentPayment> StudentPayments { get; set; }
        public ICollection<CourseEpisode> CourseEpisodes { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Keyword> Keywords { get; set; }
        

    }
}
