using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace be.Repositories.PostcommentRepository
{
    public class PostcommentRepository : IPostcommentRepository
    {
        private readonly DbZotsystemContext _context;

        public PostcommentRepository()
        {
            _context = new DbZotsystemContext();
        }
        public object AddPostcomment(Postcomment postcomment)
        {
            try
            {
                _context.Add(postcomment);
                _context.SaveChanges();
                return new
                {
                    message = "Comment successfully",
                    postcomment,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    message = "Cannot comment in this post",
                    status = 400
                };
            }
        }
        public dynamic GetCommentByPost(int postId)
        {
            var postcomments = _context.Postcomments.Include(p => p.Post).Where(p => p.PostId == postId).Select(p =>
            new
            {
                p.PostCommentId,
                p.AccountId,
                p.Account.FullName,
                p.PostId,
                p.Content,
                p.FileComment,
                p.Status,
                p.CommentDate
            });
            return postcomments;
        }
        public object ChangeStatusPostcomment(int postcommentId, string status)
        {
            var updateStatus = _context.Postcomments.SingleOrDefault(x => x.PostCommentId == postcommentId);
            if (updateStatus == null)
            {
                return new
                {
                    message = "The comment doesn't exist in database",
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
                    message = "Update successfully!"
                };
            }
        }
        public void EditComment(Postcomment postcomment)
        {
            var updatePostcomment = _context.Postcomments.SingleOrDefault(x => x.PostCommentId == postcomment.PostCommentId);
            updatePostcomment.Content = postcomment.Content;
            updatePostcomment.FileComment = postcomment.FileComment;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

    } 
}
