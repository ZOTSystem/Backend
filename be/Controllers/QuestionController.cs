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
    }
}
