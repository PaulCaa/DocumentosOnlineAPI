using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest
{
    public class RequestLogin
    {
        public string User { set; get; }
        public string Password { set; get; }
    }
}