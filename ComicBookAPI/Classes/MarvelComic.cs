using MongoDB.Entities.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ComicBookAPI.Classes
{
    class MarvelComic : Entity
    {
        public string Title { get; set; }
        public int Issue { get; set; }
        public int[]Characters { get; set; }
        
    }
}