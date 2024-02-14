using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Bll.Services.ExporterService
{
    public class QuizExporterManager
    {
        private readonly CompositionContainer _container;

        public QuizExporterManager()
        {
            // Create a catalog of available exporters
            var catalog = new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new Exception("Missing path in directory catalog generation"));

            // Create the container
            _container = new CompositionContainer(catalog);
        }

        public byte[] ExportQuizAsync(string exporterName, QuizEntity quiz)
        {
            // Find the exporter by name
            var exporter = _container.GetExportedValue<IQuizExporter>(exporterName);

            if (exporter == null)
            {
                throw new ArgumentException($"Exporter '{exporterName}' not found", nameof(exporterName));
            }

            // Export the quiz
            return exporter.ExportQuiz(quiz);
        }

        public string[] GetAvailableExporters()
        {
            // Get available exporters
            var exports = _container.GetExports<IQuizExporter>();

            // Extract names of the exporters
            var exporterNames = exports.Select(export => export.GetType().Name).ToArray();

            return exporterNames;

        }
    }

}
