using be.Models;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Repositories.PostRepository
{
    public interface IPostRepository
    {
        Task<object> GetAllPost();
        object GetPostById(int PostId);
        object AddPost(Post PostId);
        object ChangeStatusPost(int PostId, string status);
        void EditPost(Post post);
        dynamic GetPostBySubject(int subjectId);
        dynamic GetPostByStatusAndSubject(string? status,int? subjectId);
/*        dynamic GetPosts(int page, int pageSize);*/
    }
}
