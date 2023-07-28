using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Data.Entity;

namespace be.Repositories.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly DbZotsystemContext _context;

        public PostRepository()
        {
            _context = new DbZotsystemContext();
        }
        public object AddPost(Post post)
        {
            try
            {
                _context.Add(post);
                _context.SaveChanges();
                return new
                {
                    message = "Add post successfully",
                    post,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    message = "Add post fail",
                    status = 400
                };
            }
        }

        public object ChangeStatusPost(int postId, string status)
        {
            var updateStatus = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (updateStatus == null)
            {
                return new
                {
                    message = "The post doesn't exist in database",
                    status = 400
                };
            }
            else
            {
                updateStatus.Status = status;
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    updateStatus,
                    message = "Post Update successfully!"
                };
            }
        }
        public async Task<object> GetAllPost()
        {
            var data = _context.Posts
                .Include(p => p.Subject)
                .Include(p => p.Account)
                .Include(p => p.Postcomments)
                .Include(p => p.Postlikes)
                .Where(p => p.Status == "Approved")
                .OrderByDescending(p => p.CreateDate)
                .Select(p =>
              new
              {
                  p.PostId,
                  p.SubjectId,
                  p.Subject.SubjectName,
                  p.AccountId,
                  p.Account.FullName,
                  p.PostText,
                  p.PostFile,
                  p.Status,
                  p.CreateDate,
                  countComment = p.Postcomments.Count(),
                  countLike = p.Postlikes.Count()
              }) ;   
            return data;
        }
        public object GetPostById(int postId)
        {
            var data = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public object EditPost(EditPostDTO post)
        {
            try
            {
                var editPost = _context.Posts.SingleOrDefault(x => x.PostId == post.PostId);
                if (editPost == null)
                {
                    return new
                    {
                        message = "Post Not Found",
                        status = 200,
                    };
                }
                editPost.PostText = post.PostText;
                editPost.PostFile = post.PostFile;
                editPost.SubjectId = post.SubjectId;
                editPost.Status = "Approved";
                _context.SaveChanges();
                return new
                {
                    message = "Post edited successfully",
                    status = 200,
                    editPost,
                };
            }
            catch
            {
                return new
                {
                    message = "Comment edited failed",
                    status = 400,
                };
            }
        }

        public dynamic GetPostByStatus(string? status, int accountId)
        {
            var posts = _context.Posts
                .Include(p => p.Subject)
                .Include(p => p.Postcomments)
                .Include(p => p.Postlikes)
                .Where(p => p.Status == status)
                .OrderByDescending(p => p.CreateDate)
                .Select(p =>
                 new
                 {
                     p.PostId,
                     p.Subject.SubjectName,
                     p.Account.FullName,
                     p.PostText,
                     p.PostFile,
                     p.Status,
                     p.CreateDate,
                     countComment = p.Postcomments.Count(),
                     countLike = p.Postlikes.Count()
                 }); ;
            return posts;
        }

        public dynamic GetPostBySubject(int subjectId)
        {
            {
                var posts = _context.Posts
                    .Include(p => p.Subject)
                    .Include(p => p.Postcomments)
                    .Include(p => p.Postlikes)
                    .Where(p => p.Subject.SubjectId == subjectId && p.Status == "Approved")
                    .OrderByDescending(p => p.CreateDate)
                    .Select(p => new
                    {
                        p.PostId,
                        p.Subject.SubjectName,
                        p.Account.FullName,
                        p.PostText,
                        p.PostFile,
                        p.Status,
                        p.CreateDate,
                        countComment = p.Postcomments.Count(),
                        countLike = p.Postlikes.Count()
                    });

                return posts;
            }
        }
        public dynamic GetPostBySubjectAndStatus(int subjectId, string status, int accountId)
        {
            {
                var posts = _context.Posts
                    .Include(p => p.Subject)
                    .Include(p => p.Postcomments)
                    .Include(p => p.Postlikes)
                    .Where(p => p.Subject.SubjectId == subjectId && p.Status == status)
                    .OrderByDescending(p => p.CreateDate)
                    .Select(p => new
                    {
                        p.PostId,
                        p.Subject.SubjectName,
                        p.Account.FullName,
                        p.PostText,
                        p.PostFile,
                        p.Status,
                        p.CreateDate,
                        countComment = p.Postcomments.Count(),
                        countLike = p.Postlikes.Count()
                    });
                return posts;
            }
        }
        public object CountComment(int postId)
        {
            try
            {
                var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
                var countComment = _context.Postcomments.Where(x => x.PostId == postId).Count();
                if (post == null)
                {
                    return new
                    {
                        message = "Cannot find this post",
                        status = 200,
                    };
                }
                return new
                {
                    countComment,
                };
            }
            catch
            {
                return new
                {
                    status = 400,
                };
            }
        }

        public object CountLikedNumberByPost(int postId)
        {
            try
            {
                var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
                var countLiked = _context.Postlikes.Where(x => x.PostId == postId).Count();
                if (post == null)
                {
                    return new
                    {
                        message = "Cannot find this post",
                        status = 200,
                    };
                }
                return new
                {
                    status = 200,
                    countLiked,
                };
            }
            catch
            {
                return new
                {
                    status = 400,
                };
            }
        }

        public object LikePost(int postId, int accountId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return new
                {
                    message = "The post doesn't exist in the database",
                    status = 400
                };
            }
            else
            {
                var postlike = new Postlike
                {
                    AccountId = accountId,
                    PostId = postId,
                    LikeDate = DateTime.Now
                };

                _context.Postlikes.Add(postlike);
                _context.SaveChanges();

                return new
                {
                    status = 200,
                    postlike,
                    message = "Post liked"
                };
            }
        }
        public object UnlikePost(int postLikeId)
        {
            var postUnlike = _context.Postlikes.SingleOrDefault(x => x.PostLikeId == postLikeId);
            if (postUnlike == null)
            {
                return new
                {
                    message = "PostLikeId does not exist in the database!",
                    status = 400
                };
            }
            else
            {
                _context.Postlikes.Remove(postUnlike);
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "Unlike post successfully!"
                };
            }
        }
        public object DeletePost(int postId)
        {
            var post = _context.Posts.SingleOrDefault(x => x.PostId == postId);
            if (post == null)
            {
                return new
                {
                    message = "Post does not exist in the database!",
                    status = 400
                };
            }
            else
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return new
                {
                    status = 200,
                    message = "Delete post successfully!"
                };
            }
        }
    }
}


