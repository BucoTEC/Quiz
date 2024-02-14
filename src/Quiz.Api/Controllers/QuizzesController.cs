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
    /// <summary>
    /// Represents a controller for managing quizzes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController(IQuizService quizService) : ControllerBase
    {
        private readonly IQuizService _quizService = quizService;

        /// <summary>
        /// Retrieves a quiz by its unique identifier.
        /// </summary>
        /// <remarks>
        /// To retrieve a quiz, provide its unique identifier in the URL path.
        /// Optionally, you can include associated questions by setting the <c>includeQuestions</c> parameter to <c>true</c>.
        /// By default, questions are not included in the response.
        /// After successful retrieval, the requested quiz is returned along with a status code of 200 (OK).
        /// If no quiz is found with the specified identifier, a status code of 404 (Not Found) is returned.
        /// </remarks>
        /// <param name="id">The unique identifier of the quiz.</param>
        /// <param name="includeQuestions">Optional. Indicates whether to include questions associated with the quiz. Default is false.</param>
        /// <returns>The requested quiz.</returns>
        /// <response code="200">Returns the requested quiz.</response>
        /// <response code="404">If no quiz is found with the specified identifier.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuizResponseDto), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<ActionResult<QuizResponseDto>> GetQuizById([FromRoute] Guid id, [FromQuery] bool includeQuestions = false)
        {
            var quiz = await _quizService.GetQuizById(id, includeQuestions);
            return Ok(quiz);
        }

        /// <summary>
        /// Searches for quizzes based on the provided search criteria.
        /// </summary>
        /// <remarks>
        /// To search for quizzes, provide the search criteria in the query parameters.
        /// The search criteria can include parameters such as:
        /// - <c>PageIndex</c>: The index of the page to retrieve (default is 1).
        /// - <c>PageSize</c>: The number of items per page (default is 6, maximum is 50).
        /// - <c>SearchByQuizName</c>: Search by quiz name (case insensitive).
        /// - <c>IncludeQuestions</c>: Indicates whether to include questions associated with the quizzes in the result (default is false).
        /// After a successful search, a paginated list of quizzes matching the search criteria is returned.
        /// </remarks>
        /// <param name="searchQuizzesQuery">The search criteria for quizzes.</param>
        /// <returns>A paginated list of quizzes matching the search criteria.</returns>
        /// <response code="200">Returns the paginated list of quizzes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Pagination<QuizResponseDto>), 200)]
        public async Task<ActionResult<Pagination<QuizResponseDto>>> SearchQuizzes([FromQuery] SearchQuizzesQuery searchQuizzesQuery)
        {
            var quizzes = await _quizService.SearchQuizzes(searchQuizzesQuery);

            return Ok(quizzes);
        }

        /// <summary>
        /// Creates a new quiz.
        /// </summary>
        /// <remarks>
        /// To create a new quiz, provide the necessary data in the request body.
        /// If you want to reuse existing questions and add them to the quiz, include their IDs in the <c>QuestionsIds</c> list of the request payload.
        /// If you want to create brand new questions and add them to the quiz during quiz creation, add them to the <c>Questions</c> list in the request payload.
        /// The request should follow the schema defined by the <c>CreateQuizDto</c> class.
        /// After successful creation, the newly created quiz is returned along with a status code of 201 (Created).
        /// If the request data is invalid, a status code of 400 (Bad Request) is returned.
        /// </remarks>
        /// <param name="createQuizDto">The data for creating the quiz.</param>
        /// <returns>The created quiz.</returns>
        /// <response code="201">Returns the newly created quiz.</response>
        /// <response code="400">If the request data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(typeof(QuizResponseDto), 201)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<ActionResult<QuizResponseDto>> CreateQuiz([FromBody] CreateQuizDto createQuizDto)
        {
            var quiz = await _quizService.CreateQuiz(createQuizDto);
            return CreatedAtAction(nameof(GetQuizById), new { id = quiz.Id }, quiz);
        }


        /// <summary>
        /// Updates an existing quiz.
        /// </summary>
        /// <remarks>
        /// To update a quiz, provide the unique identifier of the quiz to update in the URL path and the updated data in the request body.
        /// After successful update, the updated quiz is returned along with a status code of 200 (OK).
        /// If the request data is invalid, a status code of 400 (Bad Request) is returned.
        /// If no quiz is found with the specified identifier, a status code of 404 (Not Found) is returned.
        /// </remarks>
        /// <param name="id">The unique identifier of the quiz to update.</param>
        /// <param name="updateQuizDto">The data for updating the quiz.</param>
        /// <returns>The updated quiz.</returns>
        /// <response code="200">Returns the updated quiz.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="404">If no quiz is found with the specified identifier.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(QuizResponseDto), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<ActionResult<QuizResponseDto>> UpdateQuiz([FromRoute] Guid id, [FromBody] UpdateQuizDto updateQuizDto)
        {
            var quiz = await _quizService.UpdateQuiz(id, updateQuizDto);
            return Ok(quiz);
        }

        /// <summary>
        /// Deletes a quiz by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the quiz to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">If the quiz is successfully deleted.</response>
        /// <response code="404">If no quiz is found with the specified identifier.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<ActionResult> DeleteQuiz([FromRoute] Guid id)
        {
            await _quizService.DeleteQuiz(id);
            return NoContent();
        }
    }
}
