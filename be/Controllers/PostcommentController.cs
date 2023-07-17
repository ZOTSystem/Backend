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
        public async Task<ActionResult> AddPostcomment([FromBody]PostcommentDTO addPostcomment)
        {
            try
            {
                var postcomment = new Postcomment();
                postcomment.PostId = addPostcomment.PostId;
                postcomment.AccountId = addPostcomment.AccountId;
                postcomment.Content = addPostcomment.Content;
                postcomment.FileComment = addPostcomment.FileComment;
                postcomment.Status = "Đã đăng";
                postcomment.CommentDate = DateTime.Now;
                var result = _postcommentService.AddPostcomment(postcomment);
                
                return Ok(result);
            } 
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getCommentByPost")]
        public ActionResult GetCommentbyPost(int postId)
        {
            try
            {
                var data =  _postcommentService.GetCommentByPost(postId);
                return Ok(data);
            }
            catch
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
