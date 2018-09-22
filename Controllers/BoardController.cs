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
    public class BoardController : Controller
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            List<Post> topics;
            using (var db = new ApplicationDbContext()) {
                topics = await db.Posts
                    .Where(t => t.BoardId == id && t.ParentId == 0)
                    .OrderByDescending(t => t.LastTimeBumped)
                    .ToListAsync();
            }
            return Json(topics);
        }
        
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

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
