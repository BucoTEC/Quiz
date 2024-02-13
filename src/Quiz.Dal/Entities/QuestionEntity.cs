using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Dal.Entities
{
    public class QuestionEntity : BaseEntity
    {
        public string QuestionText { get; set; } = string.Empty;

        public string QuestionAnswer { get; set; } = string.Empty;

        public List<QuizEntity>? Quizzes { get; set; }
    }
}
