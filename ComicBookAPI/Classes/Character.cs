using MongoDB.Entities.Core;
using System;

namespace ComicBookAPI.Classes
{
    class Character : Entity
    {
        public string Name { get; set; }
        public string Universe { get; set; }
        public int Marvel_id { get; set; }
        public string Alignment { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }

        public int[] Stats { get; set; }
        public int[] Superpowers { get; set; }
    }
}