using be.Repositories.ModRepository;
using be.Models;
using be.Repositories.SubjectRepository;

namespace be.Services.SubjectService
{
    public class SubjectService:ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService()
        {
            _subjectRepository = new SubjectRepository();
        }

        public async Task<object> GetAllSubject()
        {
            return await _subjectRepository.GetAllSubject();
        }
        public async Task<object> GetSubjectByTopicType(int topicType)
        {
            return await _subjectRepository.GetSubjectByTopicType(topicType);
        }
    }
}
