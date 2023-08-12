using be.Models;

namespace be.Services.SubjectService
{
    public interface ISubjectService
    {
        Task<object> GetAllSubject(); 
        Task<object> GetSubjectByTopicType(int topicType);

    }
}
