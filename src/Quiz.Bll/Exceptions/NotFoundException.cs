using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Exceptions
{
    public class NotFoundException(string message = "Not found") : Exception(message)
    {

    }
}