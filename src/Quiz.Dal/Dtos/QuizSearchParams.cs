using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Dal.Dtos
{
    public class QuizSearchParams
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string? Sort { get; set; }

        public string? Search { get; set; }

    }
}
