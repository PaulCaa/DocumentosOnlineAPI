using System;
using System.Collections.Generic;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.Rest;

namespace DocumentosOnlineAPI.Utils {
    public class RestUtils {
        /**** Codigos y Mensajes de respuesta ****/
        public const string RESPONSE_OK_CODE = "OK";
        public const string RESPONSE_OK_MSG = "Request exitosa";
        public const string RESPONSE_NOTFOUND_MSG = "No hay resultados";
        public const string RESPONSE_BADREQUEST_CODE = "BAD REQUEST";
        public const string RESPONSE_BADREQUEST_MSG = "No se puede completar solicitud";
        public const string RESPONSE_ERROR_CODE = "ERROR";
        public const string RESPONSE_INTERNAL_ERROR_MSG = "Error interno";

        /****** Headers Keys ******/
        public const string HEADER_USUARIO = "usuario";
        public const string HEADER_EMPRESA = "empresa";
        public const string HEADER_SECTOR = "sector";

        public static RestResponse GenerateResponseOkEmpty()
        {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_OK_CODE;
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
            header.ResultCode = RESPONSE_OK_CODE;
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
            header.ResultCode = RESPONSE_ERROR_CODE;
            response.Header = header;
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(ResponseError error) {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            List<ResponseError> errors = new List<ResponseError>();
            errors.Add(error);
            header.ResultCode = RESPONSE_ERROR_CODE;
            header.Errors = errors;
            response.Header = header;
            return response;
        }

        public static RestResponse GenerateResponseErrorWith(List<ResponseError> errors) {
            RestResponse response = new RestResponse();
            ResponseHeader header = new ResponseHeader();
            header.ResultCode = RESPONSE_ERROR_CODE;
            header.Errors = errors;
            response.Header = header;
            return response;
        }

    }
}