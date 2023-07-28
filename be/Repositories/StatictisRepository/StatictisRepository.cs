using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;

namespace be.Repositories.StatictisRepository
{
    public class StatictisRepository : IStatictisRepository
    {
        private readonly DbZotsystemContext _context;

        public StatictisRepository()
        {
            _context = new DbZotsystemContext();
        }

        public object GetTestDetails()
        {
            var testHistory = new List<HistoryDTO>();
            var testDetailByAccountId = _context.Testdetails.ToList().OrderByDescending(x => x.TestDetailId);
            foreach (var testDetail in testDetailByAccountId)
            {
                var historyDTO = new HistoryDTO();
                historyDTO.TestDetailId = testDetail.TestDetailId;
                historyDTO.SubmitDate = (DateTime)testDetail.CreateDate;
                var getQuestion = _context.Questiontests.Where(x => x.TestDetailId == testDetail.TestDetailId).FirstOrDefault();
                var question = _context.Questions.SingleOrDefault(x => x.QuestionId == getQuestion.QuestionId);
                var subject = _context.Subjects.SingleOrDefault(x => x.SubjectId == question.SubjectId);
                historyDTO.SubjectName = subject.SubjectName;
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);
                historyDTO.Topic = topic.TopicName;
                testHistory.Add(historyDTO);
            }

            var result = testHistory
       .GroupBy(h => new { h.SubmitDate.Date, h.SubjectName })
       .Select(group => new
       {
           SubmitDate = group.Key.Date,
           SubjectName = group.Key.SubjectName,
           Count = group.Count()
       })
       .ToList();
            if (result == null)
            {
                return new
                {
                    message = "No Data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get Data Successfully",
                status = 200,
                data = result
            };
        }
    }
}
