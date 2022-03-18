using System;
using System.Collections.Generic;

#nullable disable

namespace Models.InstituteModel
{
    public partial class Role
    {
        public Role()
        {
            People = new HashSet<Person>();
        }

        public byte Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
