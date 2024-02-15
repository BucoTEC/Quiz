using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Dal.Entities;

/// <summary>
/// Represents a question entity.
/// </summary>
[Index(nameof(QuestionEntity.QuestionText), IsUnique = true)]
public class QuestionEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    [Required(ErrorMessage = "Question text is required.")]
    [StringLength(300, MinimumLength = 5, ErrorMessage = "Question text must be at least 5 characters long.")]
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the answer to the question.
    /// </summary>
    [Required(ErrorMessage = "Question answer is required.")]
    [StringLength(300, MinimumLength = 2, ErrorMessage = "Question answer must be at least 2 characters long.")]
    public string QuestionAnswer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of quizzes related to this question.
    /// </summary>
    public List<QuizEntity> Quizzes { get; set; } = [];
}
