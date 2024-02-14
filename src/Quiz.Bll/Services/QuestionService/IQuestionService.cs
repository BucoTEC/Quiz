using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;

namespace Quiz.Bll.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto);
        Task<QuestionResponseDto> GetQuestionById(Guid id, bool includeQuizzes);
    }
}
