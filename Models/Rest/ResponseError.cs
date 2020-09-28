using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest
{
    public class ResponseError
    {
        public string Message { set; get; }
        public string Description { set; get; }
        public Exception Cause { set; get; }

        public ResponseError() {}

        public ResponseError(string message, string description){
            this.Message = message;
            this.Description = description;
        }

        public ResponseError(string message, string description, Exception cause){
            this.Message = message;
            this.Description = description;
            this.Cause = cause;
        }
    }
}