using MongoDB.Entities.Core;
using System;

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
        public int? superpower_code { get; set; }

        public Boolean HasPower(int power_index)
        {
            double a = Math.Pow(2, power_index + 1);
            double b = Math.Pow(2, power_index);
            if ((superpower_code % a) >= b)
                return true;
            else
                return false;

        }
    }
}