using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamrahan.Models
{
    public class PostKeyWord
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int KeywordID { get; set; }
        public virtual Post Post{ get; set; }
        public virtual Keyword Keyword{ get; set; }


    }
}
