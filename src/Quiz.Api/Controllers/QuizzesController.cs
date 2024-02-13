using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quiz.Bll.Dtos;
using Quiz.Bll.Services.QuizService;

namespace Quiz.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController(IQuizService quizService) : ControllerBase
    {
        private readonly IQuizService _quizService = quizService;


        [HttpPost]
        public async Task<ActionResult<QuizResponseDto>> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
        {
            return await _quizService.CreateQuiz(createQuizDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResponseDto>> GetQuizById([FromRoute] Guid id, [FromQuery] bool includeQuestions = false)
        {
            return await _quizService.GetQuizById(id, includeQuestions);
        }
    }
}
