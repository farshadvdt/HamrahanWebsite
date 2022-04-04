using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamrahan.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string ImagesLink { get; set; }
        public string PersonID { get; set; }
        public string Topic { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public bool IsDeleted { get; set; }


        public DateTime Updated { get; set; } 
        public DateTime Published { get; set; }


        public virtual Person Person { get; set; }
       
        public virtual ICollection<PostKeyWord> PostKeyWords{ get; set; }
       

    }
}
