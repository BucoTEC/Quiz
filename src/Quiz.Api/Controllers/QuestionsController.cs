using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
using Microsoft.AspNetCore.Mvc;
using Quiz.Bll.Services.QuestionService;

namespace Quiz.Api.Controllers;

/// <summary>
/// Provides endpoints to manage questions.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class QuestionsController(IQuestionService questionService) : ControllerBase
{
    private readonly IQuestionService _questionService = questionService;

    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <remarks>
    /// To retrieve a question, provide its unique identifier in the URL path.
    /// Optionally, you can include associated quizzes by setting the <c>includeQuizzes</c> parameter to <c>true</c>.
    /// By default, quizzes are not included in the response.
    /// After successful retrieval, the requested question is returned.
    /// </remarks>
    /// <param name="id">The unique identifier of the question.</param>
    /// <param name="includeQuizzes">Optional. Indicates whether to include quizzes associated with the question. Default is false.</param>
    /// <response code="200">Returns the requested question.</response>
    /// <response code="404">If no question is found with the specified identifier.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuestionResponseDto), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult<QuestionResponseDto>> GetQuestionById([FromRoute] Guid id, [FromQuery] bool includeQuizzes = false)
    {
        return Ok(await _questionService.GetQuestionById(id, includeQuizzes));
    }

    /// <summary>
    /// Searches for questions based on the provided search criteria.
    /// </summary>
    /// <remarks>
    /// To search for questions, provide the search criteria in the query parameters.
    /// The search criteria can include parameters such as:
    /// - <c>PageIndex</c>: The index of the page to retrieve (default is 1).
    /// - <c>PageSize</c>: The number of items per page (default is 6, maximum is 50).
    /// - <c>SearchByQuestionText</c>: Search by question text (case insensitive).
    /// - <c>Sort</c>: The field to sort the results by its creation date. Accepted values are "oldFirst" or "newFirst". If not provided, default sorting is applied which is "newFirst".
    /// - <c>IncludeQuizzes</c>: Indicates whether to include quizzes associated with the questions in the result (default is false).
    /// After a successful search, a paginated list of questions matching the search criteria is returned.
    /// </remarks>
    /// <param name="searchQuestionsQuery">The search criteria for questions.</param>
    /// <returns>A paginated list of questions matching the search criteria.</returns>
    /// <response code="200">Returns the paginated list of quizzes.</response>
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<QuestionResponseDto>), 200)]
    public async Task<ActionResult<Pagination<QuestionResponseDto>>> SearchQuestions([FromQuery] SearchQuestionsQuery searchQuestionsQuery)
    {
        return Ok(await _questionService.SearchQuestions(searchQuestionsQuery));
    }

    /// <summary>
    /// Creates a new question.
    /// </summary>
    /// <remarks>
    /// To create a new question, provide the necessary data in the request body following the schema defined by the <c>CreateQuestionDto</c> class.
    /// After successful creation, the newly created question is returned.
    /// </remarks>
    /// <param name="createQuestionDto">The data for creating the question.</param>
    /// <returns>The newly created question.</returns>
    /// <response code="201">Returns the newly created question.</response>
    /// <response code="400">If the request data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(typeof(QuestionResponseDto), 200)]
    [ProducesResponseType(typeof(void), 400)]
    public async Task<ActionResult<QuestionResponseDto>> CreateQuestion([FromBody] CreateQuestionDto createQuestionDto)
    {
        var question = await _questionService.CreateQuestion(createQuestionDto);
        return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
    }

    /// <summary>
    /// Updates an existing question.
    /// </summary>
    /// <remarks>
    /// To update a question, provide the unique identifier of the question to update in the URL path and the updated data in the request body.
    /// After successful update, the updated question is returned along with a status code of 200 (OK).
    /// If the request data is invalid, a status code of 400 (Bad Request) is returned.
    /// If no question is found with the specified identifier, a status code of 404 (Not Found) is returned.
    /// </remarks>
    /// <param name="id">The unique identifier of the question to update.</param>
    /// <param name="updateQuestionDto">The data for updating the question.</param>
    /// <returns>The updated question.</returns>
    /// <response code="200">Returns the updated question.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="404">If no question is found with the specified identifier.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(QuestionResponseDto), 200)]
    [ProducesResponseType(typeof(void), 400)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult<QuestionResponseDto>> CreateQuestion([FromRoute] Guid id, [FromBody] UpdateQuestionDto updateQuestionDto)
    {
        return Ok(await _questionService.UpdateQuestion(id, updateQuestionDto));
    }

    /// <summary>
    /// Deletes a question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the question to delete.</param>
    /// <returns>No content.</returns>
    /// <response code="204">If the question is successfully deleted.</response>
    /// <response code="404">If no question is found with the specified identifier.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<ActionResult> DeleteQuestion([FromRoute] Guid id)
    {
        await _questionService.DeleteQuestion(id);
        return NoContent();
    }
}
