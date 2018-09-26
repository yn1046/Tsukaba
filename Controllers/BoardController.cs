using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
            using (var db = new ApplicationDbContext())
            {
                topics = await db.Posts
                    .Where(t => t.BoardId == id && t.ParentId == 0)
                    .OrderByDescending(t => t.LastTimeBumped)
                    .ToListAsync();
            }
            return Json(topics);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PostTransfer fetchedTopic)
        {
            using (var db = new ApplicationDbContext())
            {
                var topic = new Post
                {
                    Id = (db.Posts.Count() + 1),
                    NumberOnBoard = (db.Posts.Where(p =>
                        p.BoardId == fetchedTopic.BoardId).Count() + 1),
                    Title = fetchedTopic.Title,
                    Message = fetchedTopic.Message,
                    BoardId = fetchedTopic.BoardId,
                    Time = DateTime.Now,
                    LastTimeBumped = DateTime.Now
                };
                await db.Posts.AddAsync(topic);

                if (fetchedTopic.Images != null)
                {
                    foreach (var image in fetchedTopic.Images)
                    {
                        var timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        var path = "/Files/" + timestamp
                            + image.FileName.Substring(image.FileName.LastIndexOf('.'));
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(Directory.GetCurrentDirectory() + path, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }
                        await db.Images.AddAsync(new Image {
                            Id = (db.Images.Count() + 1),
                            ImageUrl = path,
                            PostId = topic.Id
                        });
                    }
                }

                await db.SaveChangesAsync();
            }
            return Ok(fetchedTopic.Message + "\nFiles: " + fetchedTopic.Images.Count);
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
