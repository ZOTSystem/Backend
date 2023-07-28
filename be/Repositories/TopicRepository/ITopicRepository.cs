using be.Models;

namespace be.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<object> GetTopicByGrade(int? grade, int subject, int topicType, int accountId);
        object GetAllTopcOfExam();
    }
}
