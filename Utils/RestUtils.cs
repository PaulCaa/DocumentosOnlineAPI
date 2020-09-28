using System;
using System.Collections.Generic;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.Rest;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocumentosOnlineAPI.Utils
{
    public class RestUtils
    {
        public const string INTERNAL_ERROR_MSG = "Error interno";
        private const string RESPONSE_CODE_OK = "OK";
        private const string RESPONSE_CODE_MSG = "Request sucess";
        private const string RESPONSE_CODE_ERROR = "ERROR";

        public static RestResponse GenerateResponseOkEmpty()
        {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_OK;
            header.Message = RESPONSE_CODE_MSG;
            response.Header = header;
            response.Data = "";
            return response;
        }

        public static RestResponse GenerateResponseOkWithData(Object data)
        {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_OK;
            header.Message = RESPONSE_CODE_MSG;
            response.Header = header;
            response.Data = JsonSerializer.Serialize(data);
            return response;
        }

        public static RestResponse GenerateResponseErrorEmpty(){
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_ERROR;
            response.Header = header;
            response.Data = "";
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(ResponseError error){
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            List<ResponseError> errors = new List<ResponseError>();
            errors.Add(error);
            header.ResultCode = RESPONSE_CODE_ERROR;
            header.Errors = errors;
            response.Header = header;
            response.Data = "";
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(List<ResponseError> errors){
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_ERROR;
            header.Errors = errors;
            response.Header = header;
            response.Data = "";
            return response;
        }
    }
}