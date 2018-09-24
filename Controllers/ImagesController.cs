using System;
using System.Collections.Generic;
using System.IO;
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


                var imagePaths = await db.Images
                    .Where(i => i.PostId == post.Id)
                    .Select(i => i.ImageUrl)
                    .ToListAsync();

                var result = new List<string>();
                foreach (var path in imagePaths) {
                    var file = System.IO.File.ReadAllBytes(path.Substring(1));
                    result.Add("data:image/*;base64,"+Convert.ToBase64String(file));
                }

                return Json(result);
            }
        }
    }
}