using be.DTOs;
using be.Models;
using Microsoft.Identity.Client;

namespace be.Repositories.QuestionRepository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DbZotsystemContext _context;

        public QuestionRepository()
        {
            _context = new DbZotsystemContext();
        }

        public void AddQuestionByExcel(Question question)
        {
            _context.Questions.Add(question);   
            _context.SaveChanges();
            var count = _context.Questions.Where(x => x.TopicId == question.TopicId).Count();
            var topic = _context.Topics.SingleOrDefault(x => x.TopicId == question.TopicId);
            topic.TotalQuestion = count;
            _context.SaveChanges();
        }

        public object ApproveAllQuestionOfTopic(int topicId)
        {
            var questionList = _context.Questions.Where(x => x.TopicId == topicId && x.Status == "0");
            foreach(var question in questionList)
            {
                question.Status = "1";
            }
            try
            {
                _context.SaveChanges();
                return new
                {
                    message = "Change status susscessfuly",
                    status = 200,
                };
            } catch
            {
                return new
                {
                    message = "Change status failly",
                    status = 400,
                };
            }
        }

        public object ChangeStatusQuestion(int questionId, string status)
        {
            var question = _context.Questions.SingleOrDefault(x => x.QuestionId == questionId);
            if (question == null)
            {
                return new
                {
                    message = "No data to found",
                    status = 400,
                };
            }
            question.Status = status;
            _context.SaveChanges();
            return new
            {
                message = "Change status successfully",
                status = 200,
                data = question,
            };
        }

        public object CreateQuestion(CreateQuestionDTO questionDTO)
        {
            var question = new Question();
            question.SubjectId = questionDTO.SubjectId;
            question.AccountId = questionDTO.AccountId;
            question.AnswerId = questionDTO.AnswerId;
            question.LevelId = questionDTO.LevelId;
            question.TopicId = questionDTO.TopicId;
            question.Image = questionDTO.Image;
            question.QuestionContext = questionDTO.QuestionContent;
            question.OptionA = questionDTO.OptionA;
            question.OptionB = questionDTO.OptionB;
            question.OptionC = questionDTO.OptionC;
            question.OptionD = questionDTO.OptionD;
            question.Solution = questionDTO.Solution;
            question.Status = "0";
            question.CreateDate = DateTime.Now;
            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                var topic = _context.Topics.SingleOrDefault(x => x.TopicId == questionDTO.TopicId);
                topic.TotalQuestion = _context.Questions.Where(x => x.TopicId == questionDTO.TopicId).Count();
                _context.SaveChanges();
                return new
                {
                    message = "Add Successfully",
                    status = 200,
                };
            } catch
            {
                return new
                {
                    message = "Add Failly",
                    status = 400,
                };
            }
        }

        public object EditQuestion(EditQuestionDTO questionDTO)
        {
            var question = _context.Questions.SingleOrDefault(x => x.QuestionId == questionDTO.QuestionId);
            if (question == null)
            {
                return new
                {
                    message = "Not found to return",
                    status = 400,
                };
            }
            try
            {
                question.LevelId = questionDTO.LevelId;
                question.QuestionContext = questionDTO.QuestionContent;
                question.Image = questionDTO.Image;
                question.OptionA = questionDTO.OptionA;
                question.OptionB = questionDTO.OptionB;
                question.OptionC = questionDTO.OptionC;
                question.OptionD = questionDTO.OptionD;
                question.AnswerId = questionDTO.AnswerId;
                question.Solution = questionDTO.Solution;
                _context.SaveChanges();
                return new
                {
                    message = "Edit Question Successfully",
                    status = 200,
                    question,
                };
            }
            catch
            {
                return new
                {
                    message = "Edit Question Failly",
                    status = 400,
                };
            }
        }

        public object GetAllQuestionByTopicId(int topicId)
        {
            var result = (from question in _context.Questions
                        where question.TopicId == topicId
                        select new
                        {
                            questionId = question.QuestionId,
                            subjectId = question.SubjectId,
                            subjectName = question.Subject.SubjectName,
                            accountId = question.AccountId,
                            accountName = question.Account.FullName,
                            answerId = question.AnswerId,
                            answer = question.Answer.AnswerName,
                            level = question.Level.LevelName,
                            levelId = question.Level.LevelId,
                            topic = question.Topic.TopicName,
                            topicId = question.Topic.TopicId,
                            image = question.Image,
                            questionContent = question.QuestionContext,
                            optionA = question.OptionA,
                            optionB = question.OptionB,
                            optionC = question.OptionC,
                            optionD = question.OptionD,
                            solution = question.Solution,
                            status = question.Status,
                            createDate = question.CreateDate,
                            statusString = question.Status == "0" ? "Chờ duyệt" : question.Status == "1" ? "Đã duyệt" : "Khóa",
                        });
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
                message = "Get data successfully",
                status = 200,
                data = result.ToList().OrderByDescending(x => x.questionId),
            };
        }

        public async Task<object> GetQuestionByTopicId(int topicId)
        {
            var data = (from question in _context.Questions
                        join anwser in _context.Answers
                        on question.AnswerId equals anwser.AnswerId
                        join level in _context.Levels
                        on question.LevelId equals level.LevelId
                        join topic in _context.Topics
                        on question.TopicId equals topic.TopicId
                        where question.TopicId == topicId
                        select new
                        {
                            topicId = topic.TopicId,
                            topicName = topic.TopicName,
                            questionId = question.QuestionId,
                            subjectId = question.SubjectId,
                            answerId = question.AnswerId,
                            answerName = anwser.AnswerName,
                            levelId = question.LevelId,
                            levelName = level.LevelName,
                            image = question.Image,
                            questionContext = question.QuestionContext,
                            optionA = question.OptionA,
                            optionB = question.OptionB,
                            optionC = question.OptionC,
                            optionD = question.OptionD,
                            solution = question.Solution,
                        }).ToList();
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
