using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.SearchQueries
{
    public class SearchQuestionsQuery : BaseSearchQuery
    {
        private string? _search;
        public string SearchByQuestionText
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }
        public bool IncludeQuizzes { get; set; } = false;

    }
}
