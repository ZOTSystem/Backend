using be.Models;
using be.Services.QuestionService;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IConfiguration _configuration;

        public QuestionController(IConfiguration configuration, IQuestionService QuestionService)
        {
            _questionService = QuestionService;
            _configuration = configuration;
        }

        [HttpGet("getQuestionByTopicId")]
        public async Task<ActionResult> GetQuestionByTopicId(int topicId)
        {
            try
            {
                var data = await _questionService.GetQuestionByTopicId(topicId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addQuestionByExcel")]
        public async Task<ActionResult> AddQuestionByExcel([FromBody] IList<IList<string>> record)
        {
            Question question = null;
            for(int i = 1; i < record.Count; i++) 
            {
                question = new Question();
                question.SubjectId = Convert.ToInt32(record[i][0]);


                await Task.Run(() => _questionService.AddQuestionByExcel(question));
            }
            return Ok(new
            {
                message = "Add Sucessfully",
                status = 200,
            });
        }

    }
}
