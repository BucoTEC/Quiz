using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.SearchQueries
{
    public class SearchQuizzesQuery : BaseSearchQuery
    {
        private string? _search;
        public string SearchByQuizName
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }
        public bool IncludeQuestions { get; set; } = false;
    }
}
