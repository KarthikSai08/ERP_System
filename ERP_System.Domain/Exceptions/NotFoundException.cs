using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, object id) :
            base($"{entity} with Id '{id}' was not found")
        { }
        public NotFoundException(string message) : base(message) { }
    }
}
