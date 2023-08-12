using be.DTOs;

namespace be.Repositories.NewsRepository
{
    public interface INewsRepository
    {
        public object GetAllNews();
        public object GetAllNewsCategory();
        public object Addews(NewsDTO newsDTO);

        public object EditNews(NewsDTO newsDTO);

        public object ChangeStatusNews(int newsId, string status);

    }
}
