using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private AppDatabaseContext _ctx;
        public ValuesController(AppDatabaseContext ctx)
        {
            _ctx = ctx;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            var users = _ctx.Users.Select(u => u.Name).ToArray();
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _ctx.Users.Find(id).Name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            User user = new User();
            user.Name = value;
            user.Age = 20;
            
            _ctx.Users.Add(user); //Insert
            _ctx.SaveChanges();   //commint
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            User user = _ctx.Users.Find(id);
            user.Name = value;

            _ctx.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = _ctx.Users.Find(id);
            _ctx.Users.Remove(user);

            _ctx.SaveChanges();
        }
    }
}
