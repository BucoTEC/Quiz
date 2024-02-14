using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Dal.Entities;

namespace Quiz.Bll.Dtos
{
    public class QuizResponseDto
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public List<InnerQuestionDto>? Questions { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public QuizResponseDto(Guid id, string name, DateTime createdAt, DateTime? updatedAt, List<QuestionEntity>? questions)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Questions = questions?.Select(q => new InnerQuestionDto
            {
                Id = q.Id,
                QuestionText = q.QuestionText,
                QuestionAnswer = q.QuestionAnswer,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt

            }).ToList();
        }

        public class InnerQuestionDto
        {
            public Guid Id { get; set; }
            public string QuestionText { get; set; } = string.Empty;
            public string QuestionAnswer { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }

            public DateTime? UpdatedAt { get; set; }
        }
    }


}
