using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.Entity
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Title2 { get; set; }
        public string LargeBody { get; set; }
        public string Tarix { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public DateTime? PublishDate { get; set; }

        public virtual ICollection<BlogPostComment> Comments { get; set; }



    }
}
