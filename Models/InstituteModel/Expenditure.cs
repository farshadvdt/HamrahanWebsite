using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Expenditure
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
    }
}
