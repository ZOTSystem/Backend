using be.Models;

namespace be.Repositories.TestDetailRepository
{
    public interface ITestDetailRepository
    {
        public object GetAllTestDetailByAccountID (int accountID);
        public object GetAllSubject();

        public object StatictisUnderstanding(int accountId, string subjectName);
    }
}
