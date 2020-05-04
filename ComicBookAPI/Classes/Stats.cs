using MongoDB.Entities.Core;
using System;

namespace ComicBookAPI.Classes
{
    class Stats 
    {
        //public string _id { get; set; }
        public int? intelligence { get; set; }
        public int? strength { get; set; }
        public int? speed { get; set; }
        public int? durability { get; set; }
        public int? power { get; set; }
        public int? combat { get; set; }
        public int? total { get; set; }
    }
}