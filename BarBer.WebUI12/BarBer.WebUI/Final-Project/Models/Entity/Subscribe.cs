using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.Entity
{
    public class Subscribe : BaseEntity
    {
        public string EMail { get; set; }
        public bool? EMailConfirm { get; set; }

        public DateTime? EMailConfirmDate { get; set; }

    }
}
