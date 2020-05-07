using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComicBookAPI.Classes;
using MongoDB.Driver;
using MongoDB.Entities;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Xml.Schema;
using System.Collections.ObjectModel;
using System.Linq;
//using System.Web.Mvc;

namespace ComicBookAPI.Controllers
{
    [Route("api/personajes")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        /***********************************GET********************************/

        /******LIST ALL******/
        [HttpGet("all")]
        public String GetAll()
        {
            //new DB("MyDatabase", "localhost", 27017);          
            var result = DB.Find<Character>().Many(p => true);
            return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")",""); 
        }
        /******GET BY NAME******/
        [HttpGet("{name}")]
        public String GetByName(String name)
        {
            //new DB("MyDatabase", "localhost", 27017);
            var result = DB.Find<Character>().Many(p => p.name.ToLower() == name.ToLower());
            return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");
        }


        /***********************************POST********************************/

        /******CREATE******/
        [HttpPost]
        public String Create([FromBody]JObject character)
        {
            //return SelectJsonToken(character, "power_values")[0].ToString();
            
            
            Boolean found = false;
            try
            {
                String name = SelectStrToken(character,"name");
                if (GetByName(name) != "[]")
                {
                    found = true;
                }
            }
            catch
            {
                

            }
            if (!found)
            {
                int? intelligence=null;
                int? strength = null;
                int? speed = null;
                int? durability = null;
                int? power = null;
                int? combat = null;
                int? total = null; 
                int? year = null;

                List<PowerValues> power_values = new List<PowerValues>();
                foreach (JToken item in SelectJsonToken(character, "power_values"))
                {

                    //String power_name = item;
                    PowerValues powervalue = new PowerValues();
                    powervalue.power_name = SelectStrToken(JObject.Parse(item.ToString()), "power_name");

                    power_values.Add(powervalue);
                }

                intelligence = SelectIntToken(character, "stats.intelligence");
                strength = SelectIntToken(character, "stats.strength");
                speed = SelectIntToken(character, "stats.speed");
                durability = SelectIntToken(character, "stats.durability");
                power = SelectIntToken(character, "stats.power");
                combat = SelectIntToken(character, "stats.combat");
                total = SelectIntToken(character, "stats.total");
                year = SelectIntToken(character, "year");


                var pj = new Character
                {
                    name = SelectStrToken(character,"name"),
                    universe = SelectStrToken(character, "universe"),
                    alignment = SelectStrToken(character, "alignment"),
                    year = year,
                    status = SelectStrToken(character, "status"),
                    race = SelectStrToken(character,"race"),
                    gender = SelectStrToken(character, "gender"),
                    stats = new Stats
                    {
                        intelligence = intelligence,
                        strength = strength,
                        speed = speed,
                        durability = durability,
                        power = power,
                        combat = combat,
                        total = total,
                    },
                    power_values = power_values
                };
                 
                
               

                pj.Save();
                return character.SelectToken("name").ToString()+" creado!";
            }
            else if (found)
            {
                return "Ya existe un personaje con el nombre "+character.SelectToken("name").ToString();
            }
            
            return character.SelectToken("name").ToString();
        }

        /******ADVANCED SEARCH******/
        [HttpPost("advancedsearch")]
        public String AdvancedSearch([FromBody]JObject character)
        {

            String Name = "";
            String Universe = "";            
            String Alignment = "";
            String Race = "";
            String Gender = "";
            //String Stats = "";
            String Status = "";
            int? intelligence = -1;
            int? strength = -1;
            int? speed = -1;
            int? durability = -1;
            int? power = -1;
            int? combat = -1;
            int? total = -1;
            int? year = -1;
            
            List<PowerValues> power_values = new List<PowerValues>();
            Boolean nopowers = true;
            try { 
            
                foreach (JToken item in SelectJsonToken(character, "power_values"))
                {

                    //String power_name = item;
                    PowerValues powervalue = new PowerValues();
                    powervalue.power_name = SelectStrToken(JObject.Parse(item.ToString()), "power_name");

                    power_values.Add(powervalue);
                }
                nopowers = false;
            }
            catch {
                nopowers = true;
            }
                

            //try
            //{
            Name = SelectStrToken(character, "name");//character.SelectToken("name").ToString();
            Universe = SelectStrToken(character, "universe");
            Alignment = SelectStrToken(character, "alignment");
            Race = SelectStrToken(character, "race");
            Gender = SelectStrToken(character, "gender");
            //Stats = SelectStrToken(character, "stats");

            
            intelligence = SelectIntToken(character, "stats.intelligence");
            strength = SelectIntToken(character, "stats.strength");
            speed = SelectIntToken(character, "stats.speed");
            durability = SelectIntToken(character, "stats.durability");
            power = SelectIntToken(character, "stats.power");
            combat = SelectIntToken(character, "stats.combat");
            total = SelectIntToken(character, "stats.total");
            year = SelectIntToken(character, "year");

            /*foreach(PowerValues item in power_values)
            {
                if()
            }*/
            //return nopowers.ToString();
            //return power_values.ToJson().ToString();
            //var result = DB.Find<Character>().Many(p => p.power_values.Contains(power_values[0]));
            // return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");

            if (power_values.Count() == 2) {
                var result = DB.Find<Character>().Many(p => ((p.name.ToLower() == Name.ToLower() || Name == "") && (p.universe.ToLower() == Universe.ToLower() || Universe == "") && (p.alignment.ToLower() == Alignment.ToLower() || Alignment == "") && (p.race.ToLower() == Race.ToLower() || Race == "") && (p.gender.ToLower() == Gender.ToLower() || Gender == "")
                   && (p.stats.intelligence == intelligence || intelligence == null) && (p.stats.strength == strength || strength == null) && (p.stats.speed == speed || speed == null) && (p.stats.durability == durability || durability == null) && (p.stats.power == power || power == null) && (p.stats.combat == combat || combat == null) && (p.stats.total == total || total == null)
                   && (p.status.ToLower() == Status.ToLower() || Status == "") && (p.year == year || year == null)
                   && p.power_values.Contains(power_values[0]) && p.power_values.Contains(power_values[1])));

                return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");
            }
            else if (power_values.Count() == 1)
            {
                var result = DB.Find<Character>().Many(p => ((p.name.ToLower() == Name.ToLower() || Name == "") && (p.universe.ToLower() == Universe.ToLower() || Universe == "") && (p.alignment.ToLower() == Alignment.ToLower() || Alignment == "") && (p.race.ToLower() == Race.ToLower() || Race == "") && (p.gender.ToLower() == Gender.ToLower() || Gender == "")
                   && (p.stats.intelligence == intelligence || intelligence == null) && (p.stats.strength == strength || strength == null) && (p.stats.speed == speed || speed == null) && (p.stats.durability == durability || durability == null) && (p.stats.power == power || power == null) && (p.stats.combat == combat || combat == null) && (p.stats.total == total || total == null)
                   && (p.status.ToLower() == Status.ToLower() || Status == "") && (p.year == year || year == null)
                   && p.power_values.Contains(power_values[0]) ));

                return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");
            }
            else if (power_values.Count() == 0)
            {
                var result = DB.Find<Character>().Many(p => ((p.name.ToLower() == Name.ToLower() || Name == "") && (p.universe.ToLower() == Universe.ToLower() || Universe == "") && (p.alignment.ToLower() == Alignment.ToLower() || Alignment == "") && (p.race.ToLower() == Race.ToLower() || Race == "") && (p.gender.ToLower() == Gender.ToLower() || Gender == "")
                   && (p.stats.intelligence == intelligence || intelligence == null) && (p.stats.strength == strength || strength == null) && (p.stats.speed == speed || speed == null) && (p.stats.durability == durability || durability == null) && (p.stats.power == power || power == null) && (p.stats.combat == combat || combat == null) && (p.stats.total == total || total == null)
                   && (p.status.ToLower() == Status.ToLower() || Status == "") && (p.year == year || year == null) ));
                
                return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");

            }
            else 
            {
                return "Solo se permite filtrar por un máximo de 2 poderes.";
            
            }


            
        }

        /***************************PUT************************/
        /******UPDATE******/
        [HttpPut]
        public String Update([FromBody]JObject character)
        {
            Boolean found = false;
            String id = "";
            
            id = SelectStrToken(character, "_id");
            found = true;
           
            
            var result = DB.Find<Character>().One(id);
            if (!found)
            {
               return "No se ha encontrado el personaje.";
            }
            else if (found)
            {
                String Name = "";
                String Universe = "";
                String Alignment = "";
                String Race = "";
                String Gender = "";
                String Status = "";

                int? intelligence = -1;
                int? strength = -1;
                int? speed = -1;
                int? durability = -1;
                int? power = -1;
                int? combat = -1;
                int? total = -1;
                int? year = -1;

                List<PowerValues> power_values = new List<PowerValues>();
                Boolean nopowers = true;
                try
                {

                    foreach (JToken item in SelectJsonToken(character, "power_values"))
                    {

                        //String power_name = item;
                        PowerValues powervalue = new PowerValues();
                        powervalue.power_name = SelectStrToken(JObject.Parse(item.ToString()), "power_name");

                        power_values.Add(powervalue);
                    }
                    nopowers = false;
                }
                catch
                {
                    nopowers = true;
                }


                Name = SelectStrToken(character, "name").ToString();
                Universe = SelectStrToken(character, "universe").ToString();
                Alignment = SelectStrToken(character, "alignment").ToString();
                Race = SelectStrToken(character, "race").ToString();
                Gender = SelectStrToken(character, "gender").ToString();
                Status = SelectStrToken(character, "status").ToString();
                


                intelligence = SelectIntToken(character, "stats.intelligence");
                strength = SelectIntToken(character, "stats.strength");
                speed = SelectIntToken(character, "stats.speed");
                durability = SelectIntToken(character, "stats.durability");
                power = SelectIntToken(character, "stats.power");
                combat = SelectIntToken(character, "stats.combat");
                total = SelectIntToken(character, "stats.total");
                year = SelectIntToken(character, "year");

                

                var pj = result;


                if(Name!="")
                    pj.name = Name;
               if (Universe != "")
                    pj.universe = Universe;
                if (Alignment != "")
                    pj.alignment = Alignment;
                if (Race != "")
                    pj.race = Race;
                if (Gender != "")
                    pj.gender = Gender;
                if (Status != "")
                    pj.status = Status;
                if (!nopowers)
                {
                    pj.power_values = power_values;
                }
                if (intelligence != null && strength != null && total != null && speed != null && durability != null && power != null && combat != null)
                    pj.stats = new Stats
                    {
                        intelligence = intelligence,
                        strength = strength,
                        speed = speed,
                        durability = durability,
                        power = power,
                        combat = combat,
                        total = total,
                    };
                
                if (year != null)
                    pj.year = year;

                pj.Save();
                
            }

            return "personaje con id "+id+" actualizado!";
        }

        private String SelectStrToken(JObject character, String node)
        {
            try
            {
                return character.SelectToken(node).ToString();
            }
            catch
            {
                return "";
            }
        }
        private int? SelectIntToken(JObject character, String node)
        {
            try
            {
                return character.SelectToken(node).ToObject<int>();
            }
            catch
            {
                return null;
            }
        }
        private JToken SelectJsonToken(JObject character, String node)
        {
            try
            {
                return character.SelectToken(node);
            }
            catch
            {
                return null;
            }
        }
    }
}
