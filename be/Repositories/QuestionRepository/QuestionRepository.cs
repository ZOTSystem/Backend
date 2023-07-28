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
