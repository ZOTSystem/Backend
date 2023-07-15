using be.Models;

namespace be.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DbZotsystemContext _context;

        public SubjectRepository()
        {
            _context = new DbZotsystemContext();
        }

        public async Task<object> GetAllSubject()
        {
            var data = (from subject in _context.Subjects
                        select new
                        {
                            subject.SubjectId,
                            subject.SubjectName,
                            subject.ImgLink,
                        }).ToList();
            return new
            {
                status = 200,
                data,
            };
        }

    }
}
