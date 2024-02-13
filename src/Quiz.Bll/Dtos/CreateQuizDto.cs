using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Dtos
{
    public class CreateQuizDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "The Name must be at least 5 characters long.")]
        public string Name { get; set; } = null!;

        public List<Guid>? QuestionsIds { get; set; }

        public List<CreateQuestionDto>? Questions { get; set; }

    }
}
