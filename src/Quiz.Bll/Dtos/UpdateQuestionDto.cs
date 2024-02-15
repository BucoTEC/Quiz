using System.ComponentModel.DataAnnotations;

namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object for updating a question.
/// </summary>
public class UpdateQuestionDto
{
    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    [Required(ErrorMessage = "The QuestionText is required.")]
    [MinLength(5, ErrorMessage = "The QuestionText must be at least 5 characters long.")]
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the answer to the question.
    /// </summary>
    [Required(ErrorMessage = "The QuestionAnswer is required.")]
    [MinLength(2, ErrorMessage = "The QuestionAnswer must be at least 2 characters long.")]
    public string QuestionAnswer { get; set; } = string.Empty;
}
