using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest
{
    public class RestResponse : ActionResult
    {
        public ResponseHeader Header { set; get; }
        public List<Object> Data { set; get; }
    }
}