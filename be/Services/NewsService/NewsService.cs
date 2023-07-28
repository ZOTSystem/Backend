using be.Repositories.NewsRepository;
using be.Repositories.TopicRepository;

namespace be.Services.NewsService
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newRepository;
        public NewsService()
        {
            _newRepository = new NewsRepository();
        }
        public object GetAllNews()
        {
            return _newRepository.GetAllNews();
        }

        public object GetAllNewsCategory()
        {
            return _newRepository.GetAllNewsCategory(); 
        }
    }
}
