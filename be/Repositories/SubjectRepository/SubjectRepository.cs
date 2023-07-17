using be.Models;
using Microsoft.Identity.Client;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

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

            var data = _context.Subjects.OrderBy(s => s.SubjectId).Select(s =>
              new
              {
                  s.SubjectId,
                  s.SubjectName,
                  s.ImgLink
              });
            return data;
        }
    }
}
