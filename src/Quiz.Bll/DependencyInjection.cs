using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quiz.Bll.Services.QuestionService;
using Quiz.Bll.Services.QuizService;

namespace Quiz.Bll
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementBusinessLogicLayer(this IServiceCollection services)
        {

            services.AddScoped<IQuizService, QuizService>();
            services.AddScoped<IQuestionService, QuestionService>();

            return services;
        }
    }
}
