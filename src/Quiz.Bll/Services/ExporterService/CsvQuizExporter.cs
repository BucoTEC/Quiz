using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Bll.Services.ExporterService
{
    public class CsvQuizExporter : IQuizExporter
    {
        public byte[] ExportQuizAsync(QuizEntity quiz)
        {
            // Generate CSV content
            var csvContent = GenerateCsvContent(quiz);
            return Encoding.UTF8.GetBytes(csvContent);
        }

        private string GenerateCsvContent(QuizEntity quiz)
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
}
