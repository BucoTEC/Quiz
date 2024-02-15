using Quiz.Bll.Services.QuizService;
using Quiz.Bll.Services.QuestionService;
using Quiz.Bll.Services.QuizExporterService;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Bll
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementBusinessLogicLayer(this IServiceCollection services)
        {

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddSingleton<QuizExporterManager>();

            return services;
        }
    }
}
