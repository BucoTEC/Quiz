
using Quiz.Dal.Dtos;
using Quiz.Dal.Entities;
using System.Reflection;
using System.ComponentModel.Composition.Hosting;

namespace Quiz.Bll.Services.QuizExporterService
{
    /// <summary>
    /// Manages the export of quizzes using available exporters.
    /// </summary>
    public class QuizExporterManager
    {
        private readonly CompositionContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizExporterManager"/> class.
        /// </summary>
        public QuizExporterManager()
        {
            // Create a catalog of available exporters
            var catalog = new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Missing path in directory catalog generation"));

            _container = new CompositionContainer(catalog);
        }

        /// <summary>
        /// Exports a quiz using the specified exporter.
        /// </summary>
        /// <param name="exporterName">The name of the exporter to use.</param>
        /// <param name="quiz">The quiz to export.</param>
        /// <returns>The exported quiz data.</returns>
        public ExportQuizData ExportQuizAsync(string exporterName, QuizEntity quiz)
        {
            var exporter = _container.GetExports<IQuizExporter>().FirstOrDefault(e => e.Value.GetType().Name == exporterName) ?? throw new Exception("Exporter not found");

            return exporter.Value.ExportQuiz(quiz);

        }

        /// <summary>
        /// Retrieves the names of available exporters.
        /// </summary>
        /// <returns>An array containing the names of available exporters.</returns>
        public string[] GetAvailableExporters()
        {
            // Get available exporters
            var exports = _container.GetExports<IQuizExporter>();

            // Extract names of the exporters
            var exporterNames = exports.Select(export => export.Value.GetType().Name).ToArray();

            return exporterNames;
        }
    }
}
