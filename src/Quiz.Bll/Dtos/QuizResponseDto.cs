using Quiz.Dal.Entities;

namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object for representing a quiz.
/// </summary>
public class QuizResponseDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuizResponseDto"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the quiz.</param>
    /// <param name="name">The name of the quiz.</param>
    /// <param name="createdAt">The date and time when the quiz was created.</param>
    /// <param name="updatedAt">Optional. The date and time when the quiz was last updated.</param>
    /// <param name="questions">Optional. The list of questions associated with the quiz.</param>
    public QuizResponseDto(Guid id, string name, DateTime createdAt, DateTime? updatedAt, List<QuestionEntity>? questions)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Questions = questions?.Select(q => new InnerQuestionDto
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            QuestionAnswer = q.QuestionAnswer,
            CreatedAt = q.CreatedAt,
            UpdatedAt = q.UpdatedAt

        }).ToList();
    }

    /// <summary>
    /// Gets the unique identifier of the quiz.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the name of the quiz.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of questions associated with the quiz.
    /// </summary>
    public List<InnerQuestionDto>? Questions { get; set; }

    /// <summary>
    /// Gets the date and time when the quiz was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time when the quiz was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// Data transfer object for representing an inner question.
    /// </summary>
    public class InnerQuestionDto
    {
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
        public string QuestionAnswer { get; set; } = string.Empty;

        /// <summary>
        /// Gets the date and time when the question was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets the date and time when the question was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
