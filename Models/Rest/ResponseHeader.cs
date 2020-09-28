using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest
{
    public class ResponseHeader
    {
        public string ResultCode { set; get; }
        public string Message { set; get; }
        public List<ResponseError> Errors { set; get; }

        public ResponseHeader() {
            this.Errors = new List<ResponseError>();
        }

        public override string ToString() {
            return "{\"ResultCode\": \"" + ResultCode + "\", "
            + "\"Message\": \"" + Message + "\", "
            + "\"Errors\"; \"" + Errors.ToString() + "\"}";
        }
    }
}