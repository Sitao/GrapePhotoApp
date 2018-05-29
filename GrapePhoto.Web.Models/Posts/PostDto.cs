﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrapePhoto.Web.Models
{
    public class PostDto
    {
        public string Id { get; set; }

        [JsonProperty("Note")]
        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string ImgThumbUrl { get; set; }

        public string UserId { get; set; }

        [JsonProperty("TimeStamp")]
        public DateTime PostDate { get; set; }

        public int LikeCount { get; set; }
    }
}
