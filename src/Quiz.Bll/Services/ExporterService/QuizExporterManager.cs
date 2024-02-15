using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Quiz.Dal.Dtos;
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

            _container = new CompositionContainer(catalog);
        }

        public ExportQuizData ExportQuizAsync(string exporterName, QuizEntity quiz)
        {
            try
            {
                var exporter = _container.GetExports<IQuizExporter>().FirstOrDefault(e => e.Value.GetType().Name == exporterName) ?? throw new Exception("No found exporter");

                return exporter.Value.ExportQuiz(quiz);
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                throw;
            }

        }

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
