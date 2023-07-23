﻿using be.Models;
using be.Services.PostService;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _configuration;

        public PostController(IConfiguration configuration, IPostService postService)
        {
            _postService = postService;
            _configuration = configuration;
        }

        [HttpGet("getAllPost")]
        public async Task<ActionResult> GetAllPost()
        {
            try
            {
                var data = await _postService.GetAllPost();
                return Ok(data);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getPostDetail")]
        public async Task<ActionResult> GetPostById(int postId)
        {
            try
            {
                var result = _postService.GetPostById(postId);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("AddPost")]
        public async Task<ActionResult> AddPost([FromBody]PostDTO addPost)
        {
            try
            {
                var post = new Post();
                post.SubjectId = addPost.SubjectId;
                post.AccountId = addPost.AccountId;
                post.PostText = addPost.PostText;
                post.PostFile = addPost.PostFile;
                post.Status = "Pending";
                post.CreateDate = DateTime.Now;
                var result = _postService.AddPost(post);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("ChangeStatusPost")]
        public async Task<ActionResult> ChangeStatusPost(int postId, string status)
        {
            try
            {
                var result = _postService.ChangeStatusPost(postId, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("EditPost")]
        public ActionResult EditPost(Post post)
        {      
            _postService.EditPost(post);
            return Ok();
        }

        [HttpGet("FilterPostBySubjectAndStatus")]
        public ActionResult GetPostBySubjectAndStatus(int subjectId, string status)
        {
            dynamic posts = _postService.GetPostBySubjectAndStatus(subjectId, status);
            return Ok(posts);
        }

        [HttpGet("GetPostByStatus")]
        public ActionResult GetPostByStatus(string? status)
        {
            dynamic posts = _postService.GetPostByStatus(status);
            return Ok(posts);
        }
        [HttpGet("GetPostBySubject")]
        public ActionResult GetPostBySubject(int subjectId)
        {
            dynamic posts = _postService.GetPostBySubject(subjectId);
            return Ok(posts);
        }
    }
}
