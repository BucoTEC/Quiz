using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Quiz.Dal.Entities;

/// <summary>
/// Represents a quiz entity.
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class QuizEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the quiz.
    /// </summary>
    [Required(ErrorMessage = "Quiz name is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Quiz name must be between 3 and 50 characters.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of questions related to this quiz.
    /// </summary>
    public List<QuestionEntity> Questions { get; set; } = [];
}
