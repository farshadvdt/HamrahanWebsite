﻿using Hamrahan.Models;
using System;
using System.Collections.Generic;

#nullable disable
namespace Hamrahan.Models
{
    public partial class CourseGroup
    {
        public CourseGroup()
        {
            Courses = new HashSet<Course>();
        }

        public int Code { get; set; }
        public byte EducationGradeCode { get; set; }
        public string Title { get; set; }

        public virtual EducationGrade EducationGradeCodeNavigation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
