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
            var result = DB.Find<Character>().Many(p => true);
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
                Stats = new int[] { character.SelectToken("stats.intelligence").ToObject<int>(), character.SelectToken("stats.strength").ToObject<int>(), character.SelectToken("stats.speed").ToObject<int>(), character.SelectToken("stats.durability").ToObject<int>(), character.SelectToken("stats.power").ToObject<int>(), character.SelectToken("stats.combat").ToObject<int>() },
                Superpowers= new int[] { character.SelectToken("superpowers.strength").ToObject<int>(), character.SelectToken("superpowers.healing").ToObject<int>(), character.SelectToken("superpowers.flight").ToObject<int>(), character.SelectToken("superpowers.superspeed").ToObject<int>(), character.SelectToken("superpowers.telepathy").ToObject<int>(), character.SelectToken("superpowers.others").ToObject<int>() }
            };

            pj.Save();
            return character.SelectToken("name").ToString();
        }

        /******ADVANCED SEARCH******/
        [HttpGet("advancedsearch")]
        public String AdvancedSearch(/*[FromBody]JObject body*/)
        {
            new DB("MyDatabase", "localhost", 27017);
            List<Character> result = DB.Find<Character>().Many(p => true);
            return result.ToJson();
        }

    }
}