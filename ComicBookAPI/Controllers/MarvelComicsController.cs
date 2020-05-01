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

namespace ComicBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarvelComicsController : ControllerBase
    {

        [HttpGet]
        public String Get()
        {
            
            return "ok" ; 
        }
        [HttpPost]
        public String Post([FromBody]JObject comic)
        {
            //Personaje pj = new Personaje(character, "marvel", "good", "mutant", "M", new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 0, 0, 0, 0, 1 });
            /*JToken chars = comic.SelectToken("characters");
            List<int> characters=new List<int>();
            foreach (JToken ch in chars)
            {
                characters.Add((int)(ch.ToString()));//characters[i] = comic.SelectToken("characters[0].marvel_id").ToObject<int>()
            }*/
            new DB("MyDatabase", "localhost", 27017);
            var mc = new MarvelComic
            {
                Title = comic.SelectToken("title").ToString(),
                Issue = comic.SelectToken("issue").ToObject<int>(),
                Characters = comic.SelectToken("characters").ToObject<int[]>(),//comic.SelectToken("characters").ToObject<int[]>()//.ToString().Trim().Replace("\r\n", string.Empty).Replace("\\", string.Empty)
            };

            mc.Save();
            return comic.SelectToken("characters").ToObject<int[]>()[0].ToString();
        }
    }
}