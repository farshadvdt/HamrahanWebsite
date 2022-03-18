using Hamrahan.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Hamrahan.Models
{
    public partial class StudentCourse
    {
        public int ID { get; set; }
        public string StudentID { get; set; }
        public long CourseID { get; set; }
        public bool? IsRegistered { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
