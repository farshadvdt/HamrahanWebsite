using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Class
    {
        public Class()
        {
            Courses = new HashSet<Course>();
        }

        public byte Code { get; set; }
        public string Title { get; set; }
        public string Describtion { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
