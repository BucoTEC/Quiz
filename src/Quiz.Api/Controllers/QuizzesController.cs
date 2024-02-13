using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
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
            var quiz = await _quizService.CreateQuiz(createQuizDto);
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizResponseDto>> GetQuizById([FromRoute] Guid id, [FromQuery] bool includeQuestions = false)
        {
            var quiz = await _quizService.GetQuizById(id, includeQuestions);
            return Ok(quiz);
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<QuizResponseDto>>> SearchQuizzes([FromQuery] SearchQuizzesQuery searchQuizzesQuery)
        {
            var quizzes = await _quizService.SearchQuizzes(searchQuizzesQuery);

            return Ok(quizzes);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuizResponseDto>> UpdateQuiz([FromRoute] Guid id, [FromBody] UpdateQuizDto updateQuizDto)
        {
            var quiz = await _quizService.UpdateQuiz(id, updateQuizDto);
            return Ok(quiz);
        }

        // delete quiz keep in mind not to deleted question related to quiz
    }
}
