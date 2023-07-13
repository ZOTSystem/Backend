using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;

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
                    message = "Add post successfully",
                    postcomment,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    message = "Add mod fail",
                    status = 400
                };
            }
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
