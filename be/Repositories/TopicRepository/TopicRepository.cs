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

        public async Task<object> GetTopicByGrade(int grade, int subjectId)
        {
            var data = (from topic in _context.Topics
                        join question in _context.Questions
                        on topic.TopicId equals question.TopicId
                        join subject in _context.Subjects
                        on question.SubjectId equals subject.SubjectId
                        where topic.Grade == grade && subject.SubjectId == subjectId
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
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
