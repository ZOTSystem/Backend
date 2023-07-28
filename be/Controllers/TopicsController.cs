using be.Services.SubjectService;
using be.Services.TopicService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IConfiguration _configuration;

        public TopicsController(IConfiguration configuration, ITopicService topicService)
        {
            _topicService = topicService;
            _configuration = configuration;
        }

        [HttpGet("getTopicByGrade")]
        public async Task<ActionResult> GetTopicByGrade(int? grade, int subjectId, int topicType, int accountId)
        {
            try
            {
                var data = await _topicService.GetTopicByGrade(grade, subjectId, topicType, accountId);
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllTopicOfExam")]
        public async Task<ActionResult> GetAllTopicOfExam()
        {
            try
            {
                var data = _topicService.GetAllTopcOfExam();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
