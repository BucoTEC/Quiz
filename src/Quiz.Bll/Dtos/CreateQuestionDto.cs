using System.ComponentModel.DataAnnotations;

namespace Quiz.Bll.Dtos;
/// <summary>
/// Data transfer object for creating a new question.
/// </summary>
public class CreateQuestionDto
{
    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    [Required(ErrorMessage = "Question text is required.")]
    [MinLength(5, ErrorMessage = "The QuestionText must be at least 5 characters long.")]
    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the answer to the question.
    /// </summary>
    [Required(ErrorMessage = "Question answer is required.")]
    [MinLength(2, ErrorMessage = "The QuestionAnswer must be at least 2 characters long.")]
    public string QuestionAnswer { get; set; } = string.Empty;
}
