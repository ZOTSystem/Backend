﻿using be.Services.ModService;
using be.Services.SubjectService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace be.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult> GetAllSubject()
        {
            try
            {
                var data = await _subjectService.GetAllSubject();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
