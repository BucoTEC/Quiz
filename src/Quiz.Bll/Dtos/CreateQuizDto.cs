using System.ComponentModel.DataAnnotations;

namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object representing the details needed to create a new quiz.
/// </summary>
public class CreateQuizDto
{
    /// <summary>
    /// Gets or sets the name of the quiz.
    /// </summary>
    [Required]
    [MinLength(5, ErrorMessage = "The Name must be at least 5 characters long.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the list of existing question IDs that need to be associated with the quiz.
    /// </summary>
    public List<Guid>? QuestionsIds { get; set; }

    /// <summary>
    /// Gets or sets the list of questions to be created and added to the quiz.
    /// </summary>
    public List<CreateQuestionDto>? Questions { get; set; }

}
