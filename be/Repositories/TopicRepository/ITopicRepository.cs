using be.Models;
using be.DTOs;

namespace be.Repositories.TopicRepository
{
    public interface ITopicRepository
    {
        Task<object> GetTopicByGrade(int? grade, int subject, int topicType, int accountId);
        object GetAllTopcOfExam();

        #region - Manage Topic 
        object GetAllTopic();
        public object ChangeStatusTopic(int topicId, string status);
        public object CreateTopic(CreateTopic createTopic);
        public object EditTopic(EditTopic editTopic);

        public object GetTopicById (int topicId);   
        #endregion
    }
}
