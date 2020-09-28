using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest
{
    public class ResponseError
    {
        public string Message { set; get; }
        public string Description { set; get; }

        public ResponseError() {}

        public ResponseError(string message, string description){
            this.Message = message;
            this.Description = description;
        }


        public override string ToString() {
            return "{\"Message\": \"" + Message + "\", "
            + "\"Description\": \"" + Description + "\"}";
        }
    }
}