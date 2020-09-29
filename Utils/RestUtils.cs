using System;
using System.Collections.Generic;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.Rest;

namespace DocumentosOnlineAPI.Utils {
    public class RestUtils {
        public const string RESPONSE_CODE_OK = "OK";
        public const string RESPONSE_OK_MSG = "Request exitosa";
        public const string RESPONSE_NOTFOUND_MSG = "No hay resultados";
        public const string RESPONSE_CODE_ERROR = "ERROR";
        public const string RESPONSE_INTERNAL_ERROR_MSG = "Error interno";

        public static RestResponse GenerateResponseOkEmpty()
        {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_OK;
            header.Message = RESPONSE_OK_MSG;
            response.Header = header;
            return response;
        }

        public static RestResponse GenerateResponseOkWithData(Object data) {
            RestResponse response = GenerateResponseOkEmpty();
            response.AddObjectToData(data);
            return response;
        }

        public static RestResponse GenerateResponseOkWithData(List<Object> data) {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_OK;
            header.Message = RESPONSE_OK_MSG;
            response.Header = header;
            foreach(Object o in data) {
                response.AddObjectToData(o);
            }
            return response;
        }

        public static RestResponse GenerateResponseErrorEmpty() {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_ERROR;
            response.Header = header;
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(ResponseError error) {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            List<ResponseError> errors = new List<ResponseError>();
            errors.Add(error);
            header.ResultCode = RESPONSE_CODE_ERROR;
            header.Errors = errors;
            response.Header = header;
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(List<ResponseError> errors) {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_CODE_ERROR;
            header.Errors = errors;
            response.Header = header;
            return response;
        }
    }
}