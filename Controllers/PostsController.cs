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
    }
}