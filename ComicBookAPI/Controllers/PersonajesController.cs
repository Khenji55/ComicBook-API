using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComicBookAPI.Classes;
using MongoDB.Driver;
using MongoDB.Entities;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using System.Xml.Schema;
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
            String count = result.Count().ToString();
            Console.WriteLine("SI");
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
            Boolean found = false;
            try
            {
                String name = character.SelectToken("name").ToString();
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
                int? superpower_code = null;
                int? year = null;
                try
                {
                    intelligence = character.SelectToken("stats.intelligence").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    strength = character.SelectToken("stats.strength").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    speed = character.SelectToken("stats.speed").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    durability = character.SelectToken("stats.durability").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    power = character.SelectToken("stats.power").ToObject<int>();
                }
                catch
                {
                    
                }
                try
                {
                    combat = character.SelectToken("stats.combat").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    total = character.SelectToken("stats.total").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    superpower_code = character.SelectToken("superpower_code").ToObject<int>();
                }
                catch
                {

                }
                try
                {
                    year = character.SelectToken("year").ToObject<int>();
                }
                catch
                {

                }
                var pj = new Character
                    {
                        name = character.SelectToken("name").ToString(),
                        universe = character.SelectToken("universe").ToString(),
                        alignment = character.SelectToken("alignment").ToString(),
                        year=year,
                        status= character.SelectToken("status").ToString(),
                        race = character.SelectToken("race").ToString(),
                        gender = character.SelectToken("gender").ToString(),
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
                        superpower_code = superpower_code
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
            String Stats = "";
            String Status = "";
            int intelligence = -1;
            int strength = -1;
            int speed = -1;
            int durability = -1;
            int power = -1;
            int combat = -1;
            int total = -1;
            int year = -1;
            
            try
            {
                Name= character.SelectToken("name").ToString();
            }
            catch { }
            try
            {
                Universe = character.SelectToken("universe").ToString();
            }
            catch {
                
            }
            try
            {
                Alignment = character.SelectToken("alignment").ToString();
            }
            catch { }
            try
            {
                Race = character.SelectToken("race").ToString();
            }
            catch { }
            try
            {
                Gender = character.SelectToken("gender").ToString();
            }
            catch { }

            try{
                Stats = character.SelectToken("stats").ToString();

            }
            catch { }
            
            /*try
            {
                Superpowers = new int[] { character.SelectToken("superpowers.strength").ToObject<int>(), character.SelectToken("superpowers.healing").ToObject<int>(), character.SelectToken("superpowers.flight").ToObject<int>(), character.SelectToken("superpowers.superspeed").ToObject<int>(), character.SelectToken("superpowers.telepathy").ToObject<int>(), character.SelectToken("superpowers.others").ToObject<int>() };

            }
            catch { }*/



            try
            {
                intelligence = character.SelectToken("stats.intelligence").ToObject<int>();

            }
            catch { }
            try
            {
                strength = character.SelectToken("stats.strength").ToObject<int>();

            }
            catch { }
            try
            {
                speed = character.SelectToken("stats.speed").ToObject<int>();

            }
            catch { }
            try
            {
                durability = character.SelectToken("stats.durability").ToObject<int>();

            }
            catch { }
            try
            {
                power = character.SelectToken("stats.power").ToObject<int>();

            }
            catch { }
            try
            {
                combat = character.SelectToken("stats.combat").ToObject<int>();

            }
            catch { }
            try
            {
                total = character.SelectToken("stats.total").ToObject<int>();

            }
            catch { }
            try
            {
                year = character.SelectToken("year").ToObject<int>();

            }
            catch { }


            var result = DB.Find<Character>().Many(p => ((p.name.ToLower()==Name.ToLower() || Name=="")&&(p.universe.ToLower() == Universe.ToLower() || Universe=="") && (p.alignment.ToLower() == Alignment.ToLower() || Alignment=="") && (p.race.ToLower() == Race.ToLower() || Race=="") && (p.gender.ToLower() == Gender.ToLower() || Gender=="")
                   && (p.stats.intelligence == intelligence || intelligence == -1) && (p.stats.strength == strength || strength == -1) && (p.stats.speed == speed || speed == -1) && (p.stats.durability == durability || durability == -1) && (p.stats.power == power || power == -1) && (p.stats.combat == combat || combat == -1) && (p.stats.total == total || total == -1)
                   && (p.status.ToLower() == Status.ToLower() || Status == "") && (p.year == year || year == -1)));


            return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");
        }

        /***************************PUT************************/
        /******UPDATE******/
        [HttpPut]
        public String Update([FromBody]JObject character)
        {
            Boolean found = false;
            String id = "";
            
            try
            {
                id = character.SelectToken("_id").ToString();
                found = true;
                
            }
            catch
            {


            }
            
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

                int intelligence = -1;
                int strength = -1;
                int speed = -1;
                int durability = -1;
                int power = -1;
                int combat = -1;
                int total = -1;
                int Superpower_Code = -1;
                int year = -1;

                try
                {
                    Name = character.SelectToken("name").ToString();
                }
                catch { }
                try
                {
                    Universe = character.SelectToken("universe").ToString();
                }
                catch
                {

                }
                try
                {
                    Alignment = character.SelectToken("alignment").ToString();
                }
                catch { }
                try
                {
                    Race = character.SelectToken("race").ToString();
                }
                catch { }
                try
                {
                    Gender = character.SelectToken("gender").ToString();
                }
                catch { }
                try
                {
                    Status = character.SelectToken("status").ToString();
                }
                catch { }


                /*try
                {
                    Superpowers = new int[] { character.SelectToken("superpowers.strength").ToObject<int>(), character.SelectToken("superpowers.healing").ToObject<int>(), character.SelectToken("superpowers.flight").ToObject<int>(), character.SelectToken("superpowers.superspeed").ToObject<int>(), character.SelectToken("superpowers.telepathy").ToObject<int>(), character.SelectToken("superpowers.others").ToObject<int>() };

                }
                catch { }*/



                try
                {
                    intelligence = character.SelectToken("stats.intelligence").ToObject<int>();

                }
                catch { }
                try
                {
                    strength = character.SelectToken("stats.strength").ToObject<int>();

                }
                catch { }
                try
                {
                    speed = character.SelectToken("stats.speed").ToObject<int>();

                }
                catch { }
                try
                {
                    durability = character.SelectToken("stats.durability").ToObject<int>();

                }
                catch { }
                try
                {
                    power = character.SelectToken("stats.power").ToObject<int>();

                }
                catch { }
                try
                {
                    combat = character.SelectToken("stats.combat").ToObject<int>();

                }
                catch { }
                try
                {
                    total = character.SelectToken("stats.total").ToObject<int>();

                }
                catch { }
                try
                {
                    year = character.SelectToken("year").ToObject<int>();

                }
                catch { }

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
                if (intelligence != -1 && strength != -1 && total != -1 && speed != -1 && durability != -1 && power != -1 && combat != -1 )
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
                if (Superpower_Code != -1)
                    pj.superpower_code = Superpower_Code;
                if (year != -1)
                    pj.year = year;

                pj.Save();
                
            }

            return "personaje con id "+id+" actualizado!";
        }
    }
}
