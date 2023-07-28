using be.Models;

namespace be.Repositories.QuestionRepository
{
    public interface IQuestionRepository
    {
        Task<object> GetQuestionByTopicId(int topicId);
        public void AddQuestionByExcel(Question question);

    }
}
