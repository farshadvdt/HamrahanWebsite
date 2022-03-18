using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamrahanTemplate.Application.DTOs
{
   public class UserDTO
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string NationalCode { get; set; }
        public DateTime Birthdate { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public byte EducationGradecode { get; set; }
        public bool Gender { get; set; }
    }
}
