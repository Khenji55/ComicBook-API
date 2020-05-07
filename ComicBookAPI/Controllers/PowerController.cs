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

namespace ComicBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerController : ControllerBase
    {

        [HttpGet("all")]
        public String GetAll()
        {
            //new DB("MyDatabase", "localhost", 27017);          
            var result = DB.Find<Power>().Many(p => true);
            String count = result.Count().ToString();
            Console.WriteLine("SI");
            return result.ToJson().Replace("ObjectId(", "").Replace("ISODate(", "").Replace(")", "");
        }
        /*[HttpPost]
        public String Create([FromBody]JObject power)
        {
            var pow = new Power
            {
                power_index = power.SelectToken("power_index").ToObject<int>(),
                power_name = power.SelectToken("power_name").ToString()
            };

            pow.Save();
            return power.ToString();
        }
        [HttpGet]
        public String test([FromBody]JObject power)
        {
            var pow = new Power
            {
                power_index = 0,
                power_name = "TEST"
            };

            
            return pow.IsIncluded(8,2).ToString();
        }*/

    }
}