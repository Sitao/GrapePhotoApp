﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapePhoto.Models
{
    public class Message
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Followers { get; set; }
    }
}
