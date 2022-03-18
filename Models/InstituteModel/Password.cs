using FromDbModels.Context;
using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Password
    {
        public long Id { get; set; }
        public string HashPassword { get; set; }
        public long AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
