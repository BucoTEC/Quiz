using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.SearchQueries
{
    public class SearchQuizzesQuery
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 50;

        public string QuizName { get; set; } = string.Empty;
    }
}
