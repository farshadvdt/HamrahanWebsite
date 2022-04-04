using Hamrahan.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Hamrahan.Models
{
    public partial class EducationGrade
    {
        public EducationGrade()
        {
            CourseGroups = new HashSet<CourseGroup>();
            People = new HashSet<Person>();
        }

        public byte Code { get; set; }
        public string Grade { get; set; }

        public virtual ICollection<CourseGroup> CourseGroups { get; set; }
        public virtual ICollection<Person> People { get; set; }
    } 
}
