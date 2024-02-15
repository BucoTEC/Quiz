using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Dtos
{
    public class ExportQuizResponseDto
    {
        public string QuizName { get; set; } = string.Empty;

        public MemoryStream QuizData { get; set; } = null!;

        public string DataType { get; set; } = string.Empty;

        public string ResponseFormat { get; set; } = string.Empty;
    }
}
