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
            new DB("MyDatabase", "localhost", 27017);
            var result = DB.Find<Character>().Many(p => p.Name.Contains(""));
            return result.ToJson() ; 
        }
        /******GET BY NAME******/
        [HttpGet("{name}")]
        public String GetByName(String name)
        {
            new DB("MyDatabase", "localhost", 27017);
            var result = DB.Find<Character>().Many(p => p.Name == name);
            return result.ToJson();
        }


        /***********************************POST********************************/

        /******CREATE******/
        [HttpPost]
        public String Create([FromBody]JObject character)
        {
            
            
            var pj = new Character
            {
                Name = character.SelectToken("name").ToString(),
                Universe = character.SelectToken("universe").ToString(),
                Marvel_id = character.SelectToken("marvel_id").ToObject<int>(),
                Alignment = character.SelectToken("alignment").ToString(),
                Race = character.SelectToken("race").ToString(),
                Gender = character.SelectToken("gender").ToString(),
                Stats = new int[] { character.SelectToken("stats.intelligence").ToObject<int>(), character.SelectToken("stats.strength").ToObject<int>(), character.SelectToken("stats.speed").ToObject<int>(), character.SelectToken("stats.durability").ToObject<int>(), character.SelectToken("stats.power").ToObject<int>(), character.SelectToken("stats.combat").ToObject<int>(), character.SelectToken("stats.total").ToObject<int>() },
                Superpowers= new int[] { character.SelectToken("superpowers.strength").ToObject<int>(), character.SelectToken("superpowers.healing").ToObject<int>(), character.SelectToken("superpowers.flight").ToObject<int>(), character.SelectToken("superpowers.superspeed").ToObject<int>(), character.SelectToken("superpowers.telepathy").ToObject<int>(), character.SelectToken("superpowers.others").ToObject<int>() }
            };

            pj.Save();
            return character.SelectToken("name").ToString();
        }

        /******ADVANCED SEARCH******/
        [HttpPost("advancedsearch")]
        public String AdvancedSearch([FromBody]JObject character)
        {

            String Name = "";
            String Universe = "";
            int Marvel_id = -1;
            String Alignment = "";
            String Race = "";
            String Gender = "";
            int[] Stats = { 0 };
            int intelligence = -1;
            int strength = -1;
            int speed = -1;
            int durability = -1;
            int power = -1;
            int combat = -1;
            int total = -1;
            int[] Superpowers = { 0 };
            int superstrength = -1;
            int healing = -1;
            int flight = -1;
            int superspeed = -1;
            int telepathy = -1;
            int others = -1;
            try
            {
                Name= character.SelectToken("name").ToString();
            }
            catch { }
            try
            {
                Universe=character.SelectToken("universe").ToString();
            }
            catch { }
            try
            {
                Marvel_id = character.SelectToken("marvel_id").ToObject<int>();

            }
            catch { }
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
            /*try
            {
                Stats = new int[] { character.SelectToken("stats.intelligence").ToObject<int>(), character.SelectToken("stats.strength").ToObject<int>(), character.SelectToken("stats.speed").ToObject<int>(), character.SelectToken("stats.durability").ToObject<int>(), character.SelectToken("stats.power").ToObject<int>(), character.SelectToken("stats.combat").ToObject<int>() };

            }
            catch { }
            
            try
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
                superstrength = character.SelectToken("superpowers.strength").ToObject<int>();

            }
            catch { }
            try
            {
                healing = character.SelectToken("superpowers.healing").ToObject<int>();

            }
            catch { }
            try
            {
                flight = character.SelectToken("superpowers.flight").ToObject<int>();

            }
            catch { }
            try
            {
                superspeed = character.SelectToken("superpowers.superspeed").ToObject<int>();

            }
            catch { }
            try
            {
                telepathy = character.SelectToken("superpowers.telepathy").ToObject<int>();

            }
            catch { }
            try
            {
                others = character.SelectToken("superpowers.others").ToObject<int>();

            }
            catch { }
            var result = DB.Find<Character>().Many(p => ((p.Name==Name || Name=="")&&(p.Universe==Universe || Universe=="") && (p.Alignment==Alignment || Alignment=="") && (p.Race==Race || Race=="") && (p.Gender==Gender || Gender=="")
                   && (p.Marvel_id == Marvel_id || Marvel_id == -1)
                   && (p.Stats[0] == intelligence || intelligence == -1) && (p.Stats[1] == strength || strength == -1) && (p.Stats[2] == speed || speed == -1) && (p.Stats[3] == durability || durability == -1) && (p.Stats[4] == power || power == -1) && (p.Stats[5] == combat || combat == -1) && (p.Stats[6] == total || total == -1)
                   && (p.Superpowers[0] == superstrength || superstrength == -1) && (p.Superpowers[1] == healing || healing == -1) && (p.Superpowers[2] == flight || flight == -1) && (p.Superpowers[3] == superspeed || superspeed == -1) && (p.Superpowers[4] == telepathy || telepathy == -1) && (p.Superpowers[5] == others || others == -1) ));



            //pj.Save();
            return result.ToJson();
        }

    }
}
