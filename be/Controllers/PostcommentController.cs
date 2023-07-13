using be.Models;
using be.Services.PostcommentService;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class PostcommentController : Controller
    {
        private readonly IPostcommentService _postcommentService;
        private readonly IConfiguration _configuration;

        public PostcommentController(IConfiguration configuration, IPostcommentService postcommentService)
        {
            _postcommentService = postcommentService;
            _configuration = configuration;
        }

        [HttpPost("AddComment")]
        public async Task<ActionResult> AddPostcomment([FromBody]PostDTO addPost)
        {
            try
            {
                var postcomment = new Postcomment();
                postcomment.Content = postcomment.Content;
                postcomment.FileComment = postcomment.FileComment;
                postcomment.Status = "Đã đăng";
                postcomment.CommentDate = DateTime.Now;
                var result = _postcommentService.AddPostcomment(postcomment);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("ChangeStatusPostcomment")]
        public async Task<ActionResult> ChangeStatusPostcomment(int postcommentId, string status)
        {
            try
            {
                var result = _postcommentService.ChangeStatusPostcomment(postcommentId, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditPost")]
        public ActionResult Editcomment(Postcomment postcomment)
        {      
            _postcommentService.EditComment(postcomment);
            return Ok();
        }
    }
}
