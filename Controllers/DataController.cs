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
            Post topic;
            using (var db = new ApplicationDbContext()) {
                topic = await db.Posts.SingleAsync();
            }
            return Json(topic);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<string>>> Get(int id)
        {
            List<Post> topics;
            using (var db = new ApplicationDbContext()) {
                topics = await db.Posts.Where(t => t.BoardId == id).ToListAsync();
            }
            return Json(topics);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] TopicTransfer fetchedTopic)
        {
            using (var db = new ApplicationDbContext()) {
                var topic = new Post {
                    NumberOnBoard = db.Posts.Where(p => 
                        p.BoardId == fetchedTopic.BoardId).Count() + 1,
                    Title = fetchedTopic.Title,
                    Message = fetchedTopic.Message,
                    BoardId = fetchedTopic.BoardId,
                    Time = DateTime.Now,
                    LastTimeBumped = DateTime.Now
                };
                await db.Posts.AddAsync(topic);
            }
            return Ok(fetchedTopic.Message + "\nFiles: "+ fetchedTopic.Images.Count);
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
