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

        public async Task<object> GetSubjectByTopicType(int topicType)
        {
            var data = (from subject in _context.Subjects
                        join question in _context.Questions
                        on subject.SubjectId equals question.SubjectId
                        join topic in _context.Topics
                        on question.TopicId equals topic.TopicId
                        where topic.TopicType == topicType && topic.FinishTestDate >= DateTime.Now
                        select new
                        {
                            topicType = topicType,
                            subject.SubjectId,
                            subject.SubjectName,
                        }).Distinct().ToList();
            return new
            {
                status = 200,
                data,
            };
        }


    }
}
