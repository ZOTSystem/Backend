namespace be.Services.TopicService
{
    public interface ITopicService
    {
        Task<object> GetTopicByGrade(int grade, int subjectId);
    }
}
