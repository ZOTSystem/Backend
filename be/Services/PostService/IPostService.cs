using be.DTOs;
using be.Models;

namespace be.Services.PostService
{
    public interface IPostService
    {
        Task<object> GetAllPost();
        object GetPostById(int PostId);
        object AddPost(Post PostId);
        object ChangeStatusPost(int PostId, string status);
        void EditPost(Post post);
        dynamic GetPostBySubject(int subjectId);
        dynamic GetPostByStatus(string? status);

    }
}
