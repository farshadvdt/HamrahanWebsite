using Models.InstituteModel;
using System;
using System.Collections.Generic;

#nullable disable

namespace FromDbModels.Context
{
    public partial class Account
    {
        public Account()
        {
            Passwords = new HashSet<Password>();
        }

        public long Id { get; set; }
        public string Email { get; set; }
        public long PersonId { get; set; }
        public object PersonID { get; internal set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Password> Passwords { get; set; }
    }
}
