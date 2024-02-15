using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Dal.Dtos
{
    public class ExportQuizData
    {
        public byte[] Data { get; set; } = null!;

        public string DataType { get; set; } = string.Empty;

        public string ResponseFormat { get; set; } = string.Empty;
    }
}
