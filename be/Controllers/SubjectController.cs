using be.Models;
using be.Services.SubjectService;
using Microsoft.AspNetCore.Mvc;
using be.DTOs;

namespace be.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IConfiguration _configuration;

        public SubjectController(IConfiguration configuration, ISubjectService subjectService)
        {
            _subjectService = subjectService;
            _configuration = configuration;
        }

        [HttpGet("getAllSubject")]
        public ActionResult GetAllSubject()
        {
            var result = _subjectService.GetAllSubject();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}








