using be.Repositories.ModRepository;
using be.Repositories.TopicRepository;

namespace be.Services.TopicService
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        public TopicService()
        {
            _topicRepository = new TopicRepository();
        }

        public object GetAllTopcOfExam()
        {
            return _topicRepository.GetAllTopcOfExam();
        }

        public async Task<object> GetTopicByGrade(int? grade, int subjectId, int topicType, int accountId)
        {
            return await _topicRepository.GetTopicByGrade(grade, subjectId, topicType, accountId);

        }
    }
}
