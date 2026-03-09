using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }

        public ValidationException(string message) : base(message) 
            => Errors = new Dictionary<string, string[]>();
        public ValidationException(IDictionary<string, string[]> errors) 
            : base("One or more validation error occurred.")
            => Errors = new Dictionary<string, string[]>(errors);
    }
}
