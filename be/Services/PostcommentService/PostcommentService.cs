using be.DTOs;
using be.Models;
using be.Repositories.PostcommentRepository;

namespace be.Services.PostcommentService
{
    public class PostcommentService : IPostcommentService
    {
        private readonly IPostcommentRepository _postcommentRepository;

        public PostcommentService()
        {
            _postcommentRepository = new PostcommentRepository();
        }
        public object AddPostcomment(Postcomment postcomment)
        {
            var result = _postcommentRepository.AddPostcomment(postcomment);
            return result;
        }

        public object ChangeStatusPostcomment(int postcommentId, string status)
        {
            return _postcommentRepository.ChangeStatusPostcomment(postcommentId, status);
        }

        public void EditComment(Postcomment postcomment)
        {
            _postcommentRepository.EditComment(postcomment);
        }
    }
}
