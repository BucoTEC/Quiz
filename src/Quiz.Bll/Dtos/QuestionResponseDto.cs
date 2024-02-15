namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object for representing a question.
/// </summary>
public class QuestionResponseDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionResponseDto"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the question.</param>
    /// <param name="questionText">The text of the question.</param>
    /// <param name="questionAnswer">The answer to the question.</param>
    /// <param name="createdAt">The date and time when the question was created.</param>
    /// <param name="updatedAt">Optional. The date and time when the question was last updated.</param>
    /// <param name="quizzes">Optional. The list of quizzes associated with the question.</param>
    public QuestionResponseDto(Guid id, string questionText, string? questionAnswer, DateTime createdAt, DateTime? updatedAt, List<QuizResponseDto>? quizzes = null)
    {
        Id = id;
        QuestionText = questionText;
        QuestionAnswer = questionAnswer;
        Quizzes = quizzes;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the question.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the answer to the question.
    /// </summary>
    public string? QuestionAnswer { get; set; }

    /// <summary>
    /// Gets or sets the list of quizzes associated with the question.
    /// </summary>
    public List<QuizResponseDto>? Quizzes { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the question was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets or sets the date and time when the question was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }
}
