using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //Db _db = new Db();
        //public ValuesController(Db db)
        //{
        //    _db = db;
        //}

        [HttpGet]
        public IEnumerable<string> Get() => new[] { "value1", "value2" };

        ////GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id) => _db[id];

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value) => _db.Add(value);

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value) => _db[id] = value;

        //// DELETE api/values/5
        //[HttpDelete("{value}")]
        //public void Delete(string value) => _db.Remove(value);

    }
}
