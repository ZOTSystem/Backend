namespace be.Services.TestDetailService
{
    public interface ITestDetailService 
    {
        public object GetAllTestDetailByAccountID(int accountID);
        public object GetAllSubject();
        public object StatictisUnderstanding(int accountId, string subjectName);

    }
}
