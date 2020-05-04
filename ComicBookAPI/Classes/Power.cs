using MongoDB.Entities.Core;
using System;

namespace ComicBookAPI.Classes
{
    class Power : Entity
    {
        //public string _id { get; set; }
        public int power_index { get; set; }
        public String power_name { get; set; }

        public Boolean IsIncluded(int power_code)
        {
            double a = Math.Pow(2, power_index + 1);
            double b = Math.Pow(2, power_index);
            if ((power_code % a) >= b) 
               return true;
            else 
                return false;
            
        }
    }
}