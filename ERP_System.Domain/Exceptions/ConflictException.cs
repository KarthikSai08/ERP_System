using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Exceptions
{
    public class ConflictException : Exception
    {

        public ConflictException(string message) :base(message) { }
    }
}
