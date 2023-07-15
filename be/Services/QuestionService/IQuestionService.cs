namespace be.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<object> GetQuestionByTopicId(int topicId);
    }
}
