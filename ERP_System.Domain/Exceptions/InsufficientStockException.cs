using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Exceptions
{
    public class InsufficientStockException : Exception
    {
        public InsufficientStockException(string productName, int requested, int available)
            : base($"Insufficient Stock for '{productName}'. Requested : {requested}, Available : {available}") { }

    }
}
