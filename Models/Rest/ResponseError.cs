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
    }
}