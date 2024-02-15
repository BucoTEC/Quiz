namespace Quiz.Bll.Dtos;

/// <summary>
/// Data transfer object for exporting a quiz.
/// </summary>
public class ExportQuizResponseDto
{
    /// <summary>
    /// Gets or sets the name of the quiz.
    /// </summary>
    public string QuizName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the data of the exported quiz.
    /// </summary>
    public byte[] QuizData { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of data.
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the format of the response.
    /// </summary>
    public string ResponseFormat { get; set; } = string.Empty;
}
