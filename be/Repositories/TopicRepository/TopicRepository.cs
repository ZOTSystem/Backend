using be.DTOs;
using be.Models;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;

namespace be.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DbZotsystemContext _context;

        public TopicRepository()
        {
            _context = new DbZotsystemContext();
        }

        public object GetAllTopcOfExam()
        {
            var data = _context.Topics.Where(x => x.TopicType != 1);
            if(data == null)
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
                data,
            };
        }

        public async Task<object> GetTopicByGrade(int? grade, int subjectId, int topicType, int accountId)
        {
            var listTopic = (from topic in _context.Topics
                             join question in _context.Questions
                             on topic.TopicId equals question.TopicId
                             join subject in _context.Subjects
                             on question.SubjectId equals subject.SubjectId
                             where topic.Grade == grade && subject.SubjectId == subjectId && topic.TopicType == topicType && topic.Status == "0"
                             select new
                             {
                                 topic.TopicId,
                                 subject.SubjectId,
                                 topic.TopicName,
                                 subject.SubjectName,
                                 topic.Duration,
                                 topic.TotalQuestion,
                                 topic.TopicType,
                                 topic.Grade,
                                 topic.CreateDate,
                                 topic.Status
                             }).Distinct().ToList();

            var listTopicSubmited = (from topic in _context.Topics
                                     join question in _context.Questions
                                     on topic.TopicId equals question.TopicId
                                     join questionTest in _context.Questiontests
                                     on question.QuestionId equals questionTest.QuestionId
                                     join testDetail in _context.Testdetails
                                     on questionTest.TestDetailId equals testDetail.TestDetailId
                                     join account in _context.Accounts
                                     on testDetail.AccountId equals account.AccountId
                                     join subject in _context.Subjects
                                     on question.SubjectId equals subject.SubjectId
                                     where account.AccountId == accountId && testDetail.Submitted == true
                                     select new
                                     {
                                         topic.TopicId,
                                         account.AccountId,
                                         testDetail.CreateDate,
                                         testDetail.Score,
                                     }).OrderBy(x => x.Score).ToList();

            var data = new List<TopicDTO>();

            foreach (var item in listTopic)
            {
                var topicDTO = new TopicDTO();
                topicDTO.TopicId = item.TopicId;
                topicDTO.TopicName = item.TopicName;
                topicDTO.TotalQuestion = item.TotalQuestion;
                topicDTO.Duration = item.Duration;
                foreach (var itemSubmited in listTopicSubmited)
                {
                    if (item.TopicId == itemSubmited.TopicId)
                    {
                        topicDTO.Score = itemSubmited.Score;
                    }
                }
                data.Add(topicDTO);
            }

            return new
            {
                status = 200,
                data,
            };
        }
    }
}
