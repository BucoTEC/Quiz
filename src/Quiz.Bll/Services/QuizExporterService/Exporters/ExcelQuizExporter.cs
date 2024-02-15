using Quiz.Dal.Dtos;
using ClosedXML.Excel;
using Quiz.Dal.Entities;
using System.ComponentModel.Composition;

namespace Quiz.Bll.Services.QuizExporterService.Exporters;

/// <inheritdoc/>
[Export(typeof(IQuizExporter))]
[ExportMetadata("Name", "ExcelQuizExporter")]
public class ExcelQuizExporter : IQuizExporter
{
    private const string dataType = "xlsx";
    private const string responseFormat = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public ExportQuizData ExportQuiz(QuizEntity quiz)
    {
        var excelContent = GenerateExcelContent(quiz);

        return new ExportQuizData
        {
            Data = excelContent,
            DataType = dataType,
            ResponseFormat = responseFormat
        };
    }

    private static byte[] GenerateExcelContent(QuizEntity quiz)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Quiz Questions");

        // Header
        worksheet.Cell(1, 1).Value = "Question Text";

        // Data
        var rowIndex = 2;
        foreach (var question in quiz.Questions)
        {
            worksheet.Cell(rowIndex, 1).Value = question.QuestionText;
            rowIndex++;
        }

        // Save the workbook to a memory stream
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
