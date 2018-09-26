using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    public class PostsController : Controller
    {
        [HttpGet("{boardId}/{topicId}")]
        public async Task<ActionResult> GetPosts(int boardId, int topicId)
        {
            List<Post> posts;
            using (var db = new ApplicationDbContext())
            {
                if (!db.Posts.Any(p => p.BoardId == boardId
                    && p.NumberOnBoard == topicId))
                    return NotFound();
                
                var topic = await db.Posts
                    .FirstOrDefaultAsync(p => p.BoardId == boardId 
                        && p.NumberOnBoard == topicId);
                if (!db.Posts.Any(p => p.BoardId == boardId
                    && p.ParentId == topic.Id))
                    return NoContent();


                posts = await db.Posts
                    .Where(p => p.ParentId == topic.Id)
                    .ToListAsync();
            }

            return Json(posts);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PostTransfer fetchedPost)
        {
            using (var db = new ApplicationDbContext())
            {
                var post = new Post
                {
                    Id = (db.Posts.Count() + 1),
                    NumberOnBoard = (db.Posts.Where(p =>
                        p.BoardId == fetchedPost.BoardId).Count() + 1),
                    Title = fetchedPost.Title,
                    Message = fetchedPost.Message,
                    BoardId = fetchedPost.BoardId,
                    ParentId = fetchedPost.ParentId,
                    Time = DateTime.Now,
                    LastTimeBumped = DateTime.Now
                };
                await db.Posts.AddAsync(post);

                if (fetchedPost.Images != null)
                {
                    foreach (var image in fetchedPost.Images)
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
                            PostId = post.Id
                        });
                    }
                }

                await db.SaveChangesAsync();
            }
            return Ok("Parent" + fetchedPost.ParentId);
        }

    }
}