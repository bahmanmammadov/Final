using Final_Project.Models.Entity.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.Entity
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual BarberUser CreatedByUser { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public virtual BarberUser DeleteByUser { get; set; }

        public int? DeleteByUserId { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
