using be.Models;

namespace be.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<object> GetTopicByGrade(int grade, int subject);
    }
}
