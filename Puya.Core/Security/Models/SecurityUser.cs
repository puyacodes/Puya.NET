﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puya.Security.Models
{
    public class SecurityUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
