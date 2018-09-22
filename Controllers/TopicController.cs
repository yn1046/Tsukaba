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
    public class TopicController : Controller
    {
        [HttpGet("{boardId}/{topicId}")]
        public async Task<ActionResult> GetTopic(int boardId, int topicId)
        {
            Post topic;
            using (var db = new ApplicationDbContext())
            {
                if (!db.Posts.Any(p => p.BoardId == boardId
                    && p.NumberOnBoard == topicId
                    && p.ParentId == 0))
                    return NotFound();

                topic = await db.Posts
                    .FirstOrDefaultAsync(p => p.BoardId == boardId
                        && p.NumberOnBoard == topicId
                        && p.ParentId == 0);
            }
            return Json(topic);
        }
    }
}