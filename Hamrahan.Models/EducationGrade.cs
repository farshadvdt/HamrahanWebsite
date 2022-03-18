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
            Lessons = new HashSet<Lesson>();
            People = new HashSet<Person>();
        }

        public byte Code { get; set; }
        public string Grade { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
