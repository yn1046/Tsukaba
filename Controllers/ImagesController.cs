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
    public class ImagesController : Controller
    {
        [HttpGet("{boardId}/{postId}")]
        public async Task<ActionResult> GetImages(int boardId, int postId)
        {
            List<Image> images;
            using (var db = new ApplicationDbContext())
            {
                if (!db.Posts.Any(p => p.BoardId == boardId
                    && p.NumberOnBoard == postId))
                    return NotFound();
                
                var post = await db.Posts
                    .FirstOrDefaultAsync(p => p.BoardId == boardId 
                        && p.NumberOnBoard == postId);
                if (!db.Images.Any(i => i.PostId == post.Id))
                    return NoContent();


                images = await db.Images
                    .Where(i => i.PostId == post.Id)
                    .ToListAsync();
            }

            return Json(images);
        }
    }
}