﻿using be.DTOs;
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

        public object AddTestDetail(int accountId)
        {
            try
            {
                Testdetail testdetail = new Testdetail();
                testdetail.AccountId = accountId;
                testdetail.Score = 0;
                testdetail.Submitted = false;
                testdetail.CreateDate = DateTime.Now;
                _context.Add(testdetail);
                _context.SaveChanges();
                return new
                {
                    testdetail,
                    status = 200
                };
            }
            catch
            {
                return new
                {
                    status = 400
                };
            }

        }
        public object UpdateTestDetail(int testDetailId)
        {
            try
            {
                var dataUpdate = _context.Testdetails.SingleOrDefault(x => x.TestDetailId == testDetailId);
                var listQuestion = (from question in _context.Questions
                                    join questionTest in _context.Questiontests
                                    on question.QuestionId equals questionTest.QuestionId
                                    join topic in _context.Topics
                                    on question.TopicId equals topic.TopicId
                                    where questionTest.TestDetailId == testDetailId
                                    select new
                                    {
                                        answerRight = question.AnswerId,
                                        answerChoose = questionTest.AnswerId
                                    }).ToList();
                if (listQuestion == null)
                {
                    return new
                    {
                        message = "List question Not Found",
                        status = 200,
                    };
                }

                if (dataUpdate == null)
                {
                    return new
                    {
                        message = "List test detail Not Found",
                        status = 200,
                    };
                }
                int count = 0;
                foreach (var item in listQuestion)
                {
                    if (item.answerRight == item.answerChoose)
                    {
                        count++;
                    }
                }

                dataUpdate.Score = count * 10 / listQuestion.Count();
                dataUpdate.Submitted = true;
                _context.Update(dataUpdate);
                _context.SaveChanges();
                return new
                {
                    message = "Update successfully",
                    status = 200,
                    dataUpdate,
                };
            }
            catch
            {
                return new
                {
                    message = "Update failed",
                    status = 400,
                };
            }
        }

        public object GetTestDetailByTestDetailId(int testDetailId)
        {
            var listRightAnswer = (from questionTest in _context.Questiontests
                                   join question in _context.Questions
                                   on new { A = questionTest.QuestionId, B = questionTest.AnswerId }
                                   equals new { A = (int?)question.QuestionId, B = question.AnswerId }
                                   where questionTest.TestDetailId == testDetailId
                                   select new
                                   {
                                       answerRight = question.QuestionId,
                                       answerChoose = questionTest.AnswerId,
                                   })?.Count();
            var data = (from testDetail in _context.Testdetails
                        join questionTest in _context.Questiontests
                        on testDetail.TestDetailId equals questionTest.TestDetailId
                        join question in _context.Questions
                        on questionTest.QuestionId equals question.QuestionId
                        join subject in _context.Subjects
                        on question.SubjectId equals subject.SubjectId
                        join topic in _context.Topics
                        on question.TopicId equals topic.TopicId
                        where testDetail.TestDetailId == testDetailId
                        select new
                        {
                            testDetail.TestDetailId,
                            topic.TopicName,
                            subject.SubjectName,
                            topic.Duration,
                            answerRight = listRightAnswer,
                            topic.TotalQuestion,
                            testDetail.Score,
                        }).Distinct().ToList();
            return new
            {
                message = "Get Data Successfully",
                status = 200,
                data,
            };
        }

        public async Task<object> GetQuestionTestByTestDetailId(int testDetailId)
        {
            var data = (from question in _context.Questions
                        join questionTest in _context.Questiontests
                        on question.QuestionId equals questionTest.QuestionId
                        where questionTest.TestDetailId == testDetailId
                        select new
                        {
                            questionId = question.QuestionId,
                            image = question.Image,
                            questionContext = question.QuestionContext,
                            optionA = question.OptionA,
                            optionB = question.OptionB,
                            optionC = question.OptionC,
                            optionD = question.OptionD,
                            solution = question.Solution,
                            answerRightByQuestion = question.AnswerId,
                            answerUserChoose = questionTest.AnswerId
                        }).ToList();
            return new
            {
                status = 200,
                data,
            };
        }
    }
}
