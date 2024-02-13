using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Dtos
{
    public class CreateQuestionDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "The QuestionText must be at least 5 characters long.")]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        [MinLength(2, ErrorMessage = "The QuestionAnswer must be at least 2 characters long.")]
        public string QuestionAnswer { get; set; } = string.Empty;
    }
}
