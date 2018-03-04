using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        Database _db = new Database();
        public ValuesController(Database db)
        {
            _db = db;
        }

        //GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) => _db[id];

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value) => _db.Add(value);

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) => _db[id] = value;

        // DELETE api/values/5
        [HttpDelete("{value}")]
        public void Delete(string value) => _db.Remove(value);

    }
}
