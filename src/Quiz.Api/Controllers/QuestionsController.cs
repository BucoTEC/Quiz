using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quiz.Bll.Dtos;
using Quiz.Bll.Services.QuestionService;

namespace Quiz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController(IQuestionService questionService) : ControllerBase
    {
        private readonly IQuestionService _questionService = questionService;

        [HttpPost]
        public async Task<QuestionResponseDto> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
        {
            return await _questionService.CreateQuestion(createQuestionDto);
        }

        [HttpGet("{id}")]
        public async Task<QuestionResponseDto> CreateQuestion([FromRoute] Guid id, [FromQuery] bool includeQuizzes = false)
        {
            return await _questionService.GetQuestionById(id, includeQuizzes);
        }
    }
}
