using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class StudentCourse
    {
        public int Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public bool? IsRegistered { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
