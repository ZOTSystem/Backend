using be.DTOs;
using be.Models;
using be.Repositories.PostRepository;

namespace be.Services.PostService
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService()
        {
            _postRepository = new PostRepository();
        }
        public object AddPost(Post post)
        {
            var result = _postRepository.AddPost(post);
            return result;
        }

        public object ChangeStatusPost(int postId, string status)
        {
            return _postRepository.ChangeStatusPost(postId, status);
        }


        public async Task<object> GetAllPost()
        {
            return await _postRepository.GetAllPost();
        }

        public object GetPostById(int postId)
        {
            return _postRepository.GetPostById(postId);
        }

        public void EditPost(Post post)
        {
            _postRepository.EditPost(post);
        }
        public dynamic GetPostBySubject(int subjectId)
        {
            return _postRepository.GetPostBySubject(subjectId);
        }
        public dynamic GetPostByStatusAndSubject(string? status, int? subjectId)
        {
            return _postRepository.GetPostByStatusAndSubject(status, subjectId);
        }
    }
}
