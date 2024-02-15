using System.Text;
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using System.ComponentModel.Composition;

namespace Quiz.Bll.Services.QuizExporterService.Exporters;

/// <inheritdoc/>
[Export(typeof(IQuizExporter))]
[ExportMetadata("Name", "CsvQuizExporter")]
public class CsvQuizExporter : IQuizExporter
{
    private const string dataType = "csv";
    private const string responseFormat = "text/csv";

    public ExportQuizData ExportQuiz(QuizEntity quiz)
    {
        var csvContent = GenerateCsvContent(quiz);

        return new ExportQuizData
        {
            Data = Encoding.UTF8.GetBytes(csvContent),
            DataType = dataType,
            ResponseFormat = responseFormat

        };
    }

    private static string GenerateCsvContent(QuizEntity quiz)
    {

        // Generate CSV content based on the quiz data
        var sb = new StringBuilder();

        // Header
        sb.AppendLine("Question Text");

        foreach (var question in quiz.Questions)
        {
            sb.AppendLine(question.QuestionText);
        }

        return sb.ToString();
    }
}
