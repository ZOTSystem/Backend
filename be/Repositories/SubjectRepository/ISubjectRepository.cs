using be.Models;


namespace be.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<object> GetAllSubject(); 
        Task<object> GetSubjectByTopicType(int topicType);
    }
}
