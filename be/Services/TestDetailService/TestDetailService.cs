using be.Repositories.PostRepository;
using be.Repositories.TestDetailRepository;

namespace be.Services.TestDetailService
{
    public class TestDetailService : ITestDetailService
    {
        private readonly ITestDetailRepository _testDetailRepository;

        public TestDetailService()
        {
            _testDetailRepository = new TestDetailRepository();
        }

        public object GetAllSubject()
        {
            return _testDetailRepository.GetAllSubject();
        }

        public object GetAllTestDetailByAccountID(int accountID)
        {
            return _testDetailRepository.GetAllTestDetailByAccountID(accountID);
        }

        public object StatictisUnderstanding(int accountId, string subjectName)
        {
            return _testDetailRepository.StatictisUnderstanding(accountId, subjectName);
        }
    }
}
