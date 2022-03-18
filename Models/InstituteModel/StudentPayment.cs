using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class StudentPayment
    {
        public long Id { get; set; }
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
