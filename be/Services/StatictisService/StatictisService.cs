using be.Repositories.StatictisRepository;
using be.Repositories.SubjectRepository;

namespace be.Services.StatictisService
{
    public class StatictisService : IStatictisService
    {
        private readonly IStatictisRepository _statictisRepository;

        public StatictisService()
        {
            _statictisRepository = new StatictisRepository();
        }
        public object GetTestDetails()
        {
            return _statictisRepository.GetTestDetails();
        }
    }
}
