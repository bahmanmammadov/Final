﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models.Entity.Membership
{
    public class BarberUser : IdentityUser<int>
    {
        public string FullName { get; set; }
    }
}
