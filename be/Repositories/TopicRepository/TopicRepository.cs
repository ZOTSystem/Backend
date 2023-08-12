using be.DTOs;
using be.Models;
using System.ComponentModel;
using System.Data.Entity.Core.Mapping;
using System.Diagnostics;
using be.DTOs;

namespace be.Repositories.TopicRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DbZotsystemContext _context;

        public TopicRepository()
        {
            _context = new DbZotsystemContext();
        }

        public object ChangeStatusTopic(int topicId, string status)
        {
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == topicId);
            if (topic == null)
            {
                return new
                {
                    message = "No data to found",
                    status = 400,
                };
            }
            topic.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
                data = topic,
            };
        }

        public object CreateTopic(CreateTopic createTopic)
        {
            try
            {
                var topic = new Topic();
                topic.TopicName = createTopic.TopicName;
                topic.Grade = createTopic.Grade;
                topic.SubjectId = createTopic.SubjectId;
                if(createTopic.Duration != null || createTopic.Duration != "null")
                {
                    topic.Duration = createTopic.Duration;
                }
                topic.TopicType = createTopic.TopicType;
                topic.CreateDate = DateTime.Now;
                topic.Status = "0";
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return new
                {
                    message = "Add successfully",
                    status = 200,
                    data = topic,
                };
            } catch
            {
                return new
                {
                    message = "Add failly",
                    status = 400,
                };
            }
            

        }

        public object EditTopic(EditTopic editTopic)
        {
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == editTopic.TopicId);
            if(topic == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            topic.TopicName = editTopic.TopicName;
            topic.Grade = editTopic.Grade;
            topic.SubjectId = editTopic.SubjectId;
            topic.Duration = editTopic.Duration;
            topic.TopicType = editTopic.TopicType;
            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Update successfully",
                    status = 200,
                    data = topic
                };
            } catch
            {
                return new
                {
                    message = "Update failly",
                    status = 400,
                };
            }
        
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

        public object GetAllTopic()
        {
            List<TopicDTO> topicList = new List<TopicDTO>();
            var subjectList = _context.Subjects.ToList();
            foreach (var item in _context.Topics)
            {
                TopicDTO topicDTO = new TopicDTO();
                topicDTO.TopicId = item.TopicId;
                var subject = subjectList.SingleOrDefault(x => x.SubjectId == item.SubjectId);
                topicDTO.SubjectId = subject.SubjectId;
                topicDTO.SubjectName = subject.SubjectName;
                topicDTO.TopicName = item.TopicName;
                topicDTO.Duration = item.Duration;
                topicDTO.TotalQuestion = item.TotalQuestion;
                topicDTO.TopicType = item.TopicType;
                if (topicDTO.TopicType == 1)
                {
                    topicDTO.TopicTypeName = "Học";
                }
                else if (topicDTO.TopicType == 2)
                {
                    topicDTO.TopicTypeName = "15p";
                }
                else if (topicDTO.TopicType == 3)
                {
                    topicDTO.TopicTypeName = "1 tiết";
                }
                else if (topicDTO.TopicType == 4)
                {
                    topicDTO.TopicTypeName = "Học kì";
                }
                else
                {
                    topicDTO.TopicTypeName = "THPT Quốc Gia";

                }
                topicDTO.Grade = item.Grade;
                topicDTO.CreateDate = item.CreateDate;
                if (item.Status == "0")
                {
                    topicDTO.Status = "Chờ duyệt";
                }
                else if (item.Status == "1")
                {
                    topicDTO.Status = "Đã duyệt";
                } else
                {
                    topicDTO.Status = "Khóa";
                }
                //topicDTO.Status = item.Status;
                topicList.Add(topicDTO);
            }
            var data = topicList.OrderByDescending(x => x.TopicId);
            if (data == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data successfully",
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

        public object GetTopicById(int topicId)
        {
            var result = from topic in _context.Topics where topic.TopicId == topicId
                         select new
                         {
                             topicId = topic.TopicId,
                             topicName = topic.TopicName,
                             subjectId = topic.SubjectId
                         };
            if(result == null)
            {
                return new
                {
                    message = "No data to return",
                    status = 400,
                };
            }
            return new
            {
                message = "Get data sucessfully",
                status = 200,
                data = result.FirstOrDefault(),
            };
        }
    }
}
