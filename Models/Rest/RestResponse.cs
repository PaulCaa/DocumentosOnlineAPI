using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DocumentosOnlineAPI.Models.Rest {
    public class RestResponse : ActionResult {
        public ResponseHeader Header { set; get; }
        public List<Object> Data { get; }

        public RestResponse() {
            this.Data = new List<Object>();
        }

        public void AddObjectToData(Object o){
            this.Data.Add(o);
        }

        public override string ToString() {
            return "{\"Header\": \"" + Header.ToString() + "\", "
            + "\"Data\"; \"" + Data.ToString() + "\"}";
        }
    }
}