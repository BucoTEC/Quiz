using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Dtos
{
    public class QuestionResponseDto(Guid id, string questionText, string? questionsAnswer, DateTime createdAt, DateTime? updateAt, List<QuizResponseDto>? quizzes = null)
    {
        public Guid Id { get; set; } = id;
        public string QuestionText { get; set; } = questionText;

        public string? QuestionAnswer { get; set; } = questionsAnswer;

        public List<QuizResponseDto>? Quizzes { get; set; } = quizzes;

        public DateTime CreatedAt { get; private set; } = createdAt;

        public DateTime? UpdatedAt { get; private set; } = updateAt;

    }
}
