namespace Quiz.Dal.Dtos;

/// <summary>
/// Represents the data for exporting a quiz.
/// </summary>
public class ExportQuizData
{
    /// <summary>
    /// Gets or sets the byte array containing the exported data.
    /// </summary>
    public byte[] Data { get; set; } = null!;

    /// <summary>
    /// Gets or sets the type of data being exported.
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the format of the response.
    /// </summary>
    public string ResponseFormat { get; set; } = string.Empty;
}
