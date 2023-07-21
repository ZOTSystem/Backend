using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;
using System.Collections;

namespace be.Repositories.TestDetailRepository
{
    public class TestDetailRepository : ITestDetailRepository
    {
        private readonly DbZotsystemContext _context;

        public TestDetailRepository()
        {
            _context = new DbZotsystemContext();
        }

        public object GetAllSubject()
        {
            var subjectList = _context.Subjects.ToList();
            if (subjectList == null)
            {
                return new
                {
                    message = "No Data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get All Subjects Successfully",
                status = 200,
                subjectList,
            };
        }

        public object GetAllTestDetailByAccountID(int accountID)
        {
            var testHistory = new List<HistoryDTO>();
            var testDetailByAccountId = _context.Testdetails.Where(x => x.AccountId == accountID).ToList().OrderByDescending(x => x.TestDetailId);
            foreach (var testDetail in testDetailByAccountId)
            {
                var historyDTO = new HistoryDTO();
                historyDTO.TestDetailId = testDetail.TestDetailId;
                historyDTO.Score = (float)testDetail.Score;
                historyDTO.SubmitDate = (DateTime)testDetail.CreateDate;
                var getQuestion = _context.Questiontests.Where(x => x.TestDetailId == testDetail.TestDetailId).FirstOrDefault();
                var question = _context.Questions.SingleOrDefault(x => x.QuestionId == getQuestion.QuestionId);
                var subject = _context.Subjects.SingleOrDefault(x => x.SubjectId == question.SubjectId);
                historyDTO.SubjectName = subject.SubjectName;
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);
                historyDTO.Topic = topic.TopicName;
                historyDTO.Duration = topic.Duration;
                testHistory.Add(historyDTO);
            }
            if (testHistory == null)
            {
                return new
                {
                    message = "No Data to return",
                    status = 400,
                };
            }
            float totalScore = 0;
            foreach (var test in testHistory)
            {
                totalScore += test.Score;
            }
            var average = totalScore / testHistory.Count();
            float underStading = (average * 100) / 10;
            return new
            {
                message = "Get Data Successfully",
                status = 200,
                data = testHistory,
                levelOfUnderStanding = underStading,
            };
        }

        public object StatictisUnderstanding(int accountId, string subjectName)
        {
            var testHistory = new List<HistoryDTO>();
            var testDetailByAccountId = _context.Testdetails.Where(x => x.AccountId == accountId).ToList().OrderByDescending(x => x.TestDetailId);
            foreach (var testDetail in testDetailByAccountId)
            {
                var historyDTO = new HistoryDTO();
                historyDTO.TestDetailId = testDetail.TestDetailId;
                historyDTO.Score = (float)testDetail.Score;
                historyDTO.SubmitDate = (DateTime)testDetail.CreateDate;
                var getQuestion = _context.Questiontests.Where(x => x.TestDetailId == testDetail.TestDetailId).FirstOrDefault();
                var question = _context.Questions.SingleOrDefault(x => x.QuestionId == getQuestion.QuestionId);
                var subject = _context.Subjects.SingleOrDefault(x => x.SubjectId == question.SubjectId);
                historyDTO.SubjectName = subject.SubjectName;
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);
                historyDTO.Topic = topic.TopicName;
                historyDTO.Duration = topic.Duration;
                testHistory.Add(historyDTO);
            }
            float totalScore;
            float average;
            float underStading;
            if (subjectName.Contains("Tất cả các môn"))
            {
                totalScore = 0;
                foreach (var test in testHistory)
                {
                    totalScore += test.Score;
                }
                average = totalScore / testHistory.Count();
                underStading = (average * 100) / 10;
                return new
                {
                    message = "Get Data Successfully",
                    status = 200,
                    data = testHistory,
                    levelOfUnderStanding = underStading,
                };
            }
            var filterTest = testHistory.Where(x => x.SubjectName.Contains(subjectName));
            if (filterTest.Count() == 0)
            {
                return new
                {
                    message = "No Data to return",
                    status = 400,
                };
            }
            totalScore = 0;
            foreach(var test in filterTest)
            {
                totalScore += test.Score;
            }
            average = totalScore / filterTest.Count();
            underStading = (average * 100) / 10; 
            return new
            {
                message = "Get Data Successfully",
                status = 200,
                data = filterTest,
                subject = subjectName,
                levelOfUnderStanding = underStading,
            };
        }
    }
}
