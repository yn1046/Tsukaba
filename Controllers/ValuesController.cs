using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Tsukaba.Models;
using Tsukaba.Models.DatabaseModels;
using Tsukaba.Services.DalServices;

namespace Tsukaba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private MongoUnitOfWork db;

        public ValuesController(IOptions<Settings> settings) {
            db = new MongoUnitOfWork(settings);
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var topic = new Topic {
                Id = 1,
                Title = "sup /b/",
                Message = "I'm your new namephug",
                Time = DateTime.Now,
                BoardId = 1
            };
            //await db.Topics.Create(topic);
            return Json(topic);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
