using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class SalaryPayment
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RemainAmount { get; set; }

        public virtual Person Employee { get; set; }
    }
}
