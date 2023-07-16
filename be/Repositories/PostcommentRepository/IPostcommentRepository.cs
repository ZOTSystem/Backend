using be.DTOs;
using be.Models;

namespace be.Repositories.PostcommentRepository
{
    public interface IPostcommentRepository
    {
        object AddPostcomment(Postcomment postcomment);
        object ChangeStatusPostcomment(int postCommentId, string status);
        void EditComment(Postcomment postcomment);
    }
}
