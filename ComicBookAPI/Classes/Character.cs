using MongoDB.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComicBookAPI.Classes
{
    class Character : Entity
    {
        //public string _id { get; set; }
        public string name { get; set; }
        public string universe { get; set; }
        //public int Marvel_id { get; set; }
        public string alignment { get; set; }
        public int? year { get; set; }
        public string race { get; set; }
        public string status { get; set; }
        public string gender { get; set; }
        //public int[] Stats { get; set; }
        public Stats stats { get; set; }
        //public int? superpower_code { get; set; }
        public List<PowerValues> power_values { get; set; }
        
        public Boolean HasPowers(List<PowerValues> powers)
        {
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            return power_values.Intersect(powers).Count() == power_values.Count();
        }
        public Boolean HasPower(PowerValues powers)
        {
            
            return power_values.Contains(powers);
        }
    }
}