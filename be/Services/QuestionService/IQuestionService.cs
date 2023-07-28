using be.Models;

namespace be.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<object> GetQuestionByTopicId(int topicId);

        public void AddQuestionByExcel(Question question);
    }
}
