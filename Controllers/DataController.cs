using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tsukaba.Models;
using Tsukaba.Models.DatabaseModels;
using Tsukaba.Models.TransferModels;

namespace Tsukaba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            Topic topic;
            using (var db = new ApplicationDbContext()) {
                topic = await db.Topics.SingleAsync();
            }
            return Json(topic);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(int id)
        {
            List<Topic> topics;
            using (var db = new ApplicationDbContext()) {
                topics = await db.Topics.Where(t => t.BoardId == id).ToListAsync();
            }
            return Json(topics);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TopicTransfer topic)
        {
            return Ok(topic);
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
