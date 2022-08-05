﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnexolTask.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(T data, string error, string message = null)
        {
            Succeeded = true;
            Message = message;
            ErrorMessage = error;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
