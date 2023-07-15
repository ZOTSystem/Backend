namespace be.Repositories.QuestionRepository
{
    public interface IQuestionRepository
    {
        Task<object> GetQuestionByTopicId(int topicId);
    }
}
