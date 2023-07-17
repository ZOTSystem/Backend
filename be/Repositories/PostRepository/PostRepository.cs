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

            var data = _context.Posts.Include(p => p.Subject).Include(p => p.Account).OrderBy(p => p.CreateDate).Select(p =>
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
                  p.CreateDate
              });
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

        public void EditPost(Post post)
        {
            Post update = _context.Posts.Where(x => x.PostId == post.PostId).FirstOrDefault();
            update.SubjectId = post.SubjectId;
            update.PostText = post.PostText;
            update.PostFile = post.PostFile;
            try
            {
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
        }
        public dynamic GetPostBySubject(string subjectName)
        {
            var posts = _context.Posts.Include(p => p.Subject).Where(p => p.Subject.SubjectName == subjectName).OrderBy(p => p.CreateDate).Select(p =>
            new 
            {
                p.PostId,
                p.SubjectId,
                p.Subject.SubjectName,
                p.AccountId,
                p.PostText,
                p.PostFile,
                p.Status,
                p.CreateDate
            });
            return posts;
        }
    }

}


