using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Exceptions
{
    public class BadRequestException(string message = "Bad request") : Exception(message)
    {

    }
}
