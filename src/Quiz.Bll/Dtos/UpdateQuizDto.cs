using System.ComponentModel.DataAnnotations;

namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object for updating a quiz.
/// </summary>
public class UpdateQuizDto
{
    /// <summary>
    /// Gets or sets the name of the quiz.
    /// </summary>
    [Required(ErrorMessage = "The Name is required.")]
    [MinLength(5, ErrorMessage = "The Name must be at least 5 characters long.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the IDs of the questions associated with the quiz.
    /// </summary>
    public List<Guid> QuestionsIds { get; set; } = new List<Guid>();
}
