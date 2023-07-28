using be.Models;
using be.Repositories.QuestionRepository;
using be.Repositories.TopicRepository;

namespace be.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionService()
        {
            _questionRepository = new QuestionRepository();
        }

        public void AddQuestionByExcel(Question question)
        {
            _questionRepository.AddQuestionByExcel(question);
        }

        public async Task<object> GetQuestionByTopicId(int topicId)
        {
            return await _questionRepository.GetQuestionByTopicId(topicId);
        }
    }
}
