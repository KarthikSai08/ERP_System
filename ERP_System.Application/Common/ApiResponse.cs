using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Sucess") 
            => new() { Success = true, Message = message };

        public static ApiResponse<T> Fail(string message)
            => new() { Success = false, Message = message };
    }
}
