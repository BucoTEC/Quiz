using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using System.ComponentModel.Composition;

namespace Quiz.Bll.Services.QuizExporterService
{
    /// <summary>
    /// Interface for exporting quiz data to various formats.
    /// </summary>
    public interface IQuizExporter
    {
        /// <summary>
        /// Exports the specified quiz data.
        /// </summary>
        /// <param name="quiz">The quiz entity to export.</param>
        /// <returns>An instance of <see cref="ExportQuizData"/> containing the exported data.</returns>
        ExportQuizData ExportQuiz(QuizEntity quiz);
    }
}
