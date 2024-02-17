using Quiz.Bll.Dtos;
using Quiz.Dal.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Dal.Entities;
using Quiz.Bll.Exceptions;
using Quiz.Bll.SearchQueries;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications.QuestionSearch;

namespace Quiz.Bll.Services.QuestionService;

/// <inheritdoc/>
public class QuestionService(IUnitOfWork unitOfWork) : IQuestionService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<QuestionResponseDto> GetQuestionById(Guid id, bool includeQuizzes)
    {
        var spec = new QuestionWithQuizzesSpecification(id, includeQuizzes);
        var question = await _unitOfWork.QuestionRepository.GetEntityWithSpec(spec) ?? throw new NotFoundException($"No question found with id: {id}");

        return BuildQuestionResponseDto(question);
    }


    /// <inheritdoc/>
    public async Task<Pagination<QuestionResponseDto>> SearchQuestions(SearchQuestionsQuery searchQuestionsQuery)
    {
        var questionSearchParams = BuildQuestionSearchParams(searchQuestionsQuery);

        var countSpec = new QuestionsCountSpecification(questionSearchParams);
        var spec = new QuestionsSearchSpecification(questionSearchParams);

        var totalCountOfQuestions = await _unitOfWork.QuestionRepository.CountAsync(countSpec);
        var question = await _unitOfWork.QuestionRepository.ListAsync(spec);

        return new Pagination<QuestionResponseDto>(searchQuestionsQuery.PageIndex, searchQuestionsQuery.PageSize, totalCountOfQuestions, question.Select(BuildQuestionResponseDto));
    }

    /// <inheritdoc/>
    public async Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto)
    {
        // verify that same question does not already exists
        var spec = new QuestionWithQuizzesSpecification(createQuestionDto.QuestionText);
        var existingQuestion = await _unitOfWork.QuestionRepository.GetEntityWithSpec(spec);
        if (existingQuestion is not null) throw new BadRequestException($"Question with same text already exists, existing question id {existingQuestion.Id}");

        // create and save new question
        var newQuestion = BuildQuestionEntity(createQuestionDto);
        _unitOfWork.QuestionRepository.Add(newQuestion);
        await _unitOfWork.CompleteAsync();

        return BuildQuestionResponseDto(newQuestion);
    }

    /// <inheritdoc/>
    public async Task<QuestionResponseDto> UpdateQuestion(Guid id, UpdateQuestionDto updateQuestionDto)
    {
        // verify that question with this id exists
        var existingQuestion = await _unitOfWork.QuestionRepository.GetByIdAsync(id) ?? throw new NotFoundException($"No question found with id: {id}");

        // verify that same question does not already exists
        var spec = new QuestionWithQuizzesSpecification(updateQuestionDto.QuestionText);
        var existingQuestionWithSameName = await _unitOfWork.QuestionRepository.GetEntityWithSpec(spec);
        if (existingQuestionWithSameName is not null) throw new BadRequestException($"Question with same text already exists, existing question id {existingQuestion.Id}");

        // update and save new question
        existingQuestion.QuestionText = updateQuestionDto.QuestionText;
        existingQuestion.QuestionAnswer = updateQuestionDto.QuestionAnswer;
        _unitOfWork.QuestionRepository.Update(existingQuestion);
        await _unitOfWork.CompleteAsync();

        return BuildQuestionResponseDto(existingQuestion);
    }

    /// <inheritdoc/>
    public async Task DeleteQuestion(Guid id)
    {
        // verify that question with this id exists
        var question = await _unitOfWork.QuestionRepository.GetByIdAsync(id) ?? throw new NotFoundException($"No question found with id: {id}");

        // delete question
        _unitOfWork.QuestionRepository.Delete(question);
        await _unitOfWork.CompleteAsync();
    }

    /// <summary>
    /// Builds <see cref="QuestionSearchParams"/> from a <see cref="SearchQuestionsQuery"/>.
    /// </summary>
    /// <param name="searchQuestionsQuery">The search query containing parameters.</param>
    /// <returns>The constructed search parameters for questions.</returns>
    private static QuestionSearchParams BuildQuestionSearchParams(SearchQuestionsQuery searchQuestionsQuery)
    {
        return new QuestionSearchParams
        {
            PageIndex = searchQuestionsQuery.PageIndex,
            PageSize = searchQuestionsQuery.PageSize,
            Sort = searchQuestionsQuery.Sort,
            Search = searchQuestionsQuery.SearchByQuestionText,
            IncludeQuizzes = searchQuestionsQuery.IncludeQuizzes
        };
    }

    /// <summary>
    /// Builds a <see cref="QuestionEntity"/> from a <see cref="CreateQuestionDto"/>.
    /// </summary>
    /// <param name="createQuestionDto">The data transfer object containing information for creating a question.</param>
    /// <returns>The constructed question entity.</returns>
    private static QuestionEntity BuildQuestionEntity(CreateQuestionDto createQuestionDto)
    {
        return new QuestionEntity
        {
            QuestionText = createQuestionDto.QuestionText,
            QuestionAnswer = createQuestionDto.QuestionAnswer
        };
    }

    /// <summary>
    /// Builds a <see cref="QuestionResponseDto"/> from a <see cref="QuestionEntity"/>.
    /// </summary>
    /// <param name="questionEntity">The question entity to build the response DTO from.</param>
    /// <returns>The constructed question response DTO.</returns>
    private static QuestionResponseDto BuildQuestionResponseDto(QuestionEntity questionEntity)
    {
        var QuizResponsesDto = questionEntity.Quizzes.Select(q => new QuizResponseDto(q.Id, q.Name, q.CreatedAt, q.UpdatedAt, null)).ToList();
        return new QuestionResponseDto(questionEntity.Id, questionEntity.QuestionText, questionEntity.QuestionAnswer, questionEntity.CreatedAt, questionEntity.UpdatedAt, QuizResponsesDto);

    }
}
