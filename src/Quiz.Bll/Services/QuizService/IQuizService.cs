using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;

namespace Quiz.Bll.Services.QuizService
{
    public interface IQuizService
    {
        Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto);

        Task<QuizResponseDto> GetQuizById(Guid id);

    }
}
