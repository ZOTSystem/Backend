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
        object EditPost(EditPostDTO post);
        dynamic GetPostByStatus(string? status);
        dynamic GetPostBySubject(int subjectId);
        dynamic GetPostBySubjectAndStatus(int subjectId, string status);
        object CountComment(int PostId);
        object CountLikedNumberByPost(int postId);
        object LikePost(int postId, int accountId);
        object UnlikePost(int postLikeId);
    }
}
