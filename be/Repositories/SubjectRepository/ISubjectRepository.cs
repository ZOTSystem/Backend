using be.Models;


namespace be.Repositories.SubjectRepository
{
    public interface ISubjectRepository
    {
        Task<object> GetAllSubject();
    }
}
