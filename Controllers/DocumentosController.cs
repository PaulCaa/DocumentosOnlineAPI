using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using DocumentosOnlineAPI.Services;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Models.Rest;
using DocumentosOnlineAPI.Utils;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase {
        private DocumentosService documentosService;

        public DocumentosController(){
            this.documentosService = new DocumentosService(); 
        }

        [HttpGet("/api/documentos/empresa/{idEmpresa}")]
        public IActionResult GetDocumentsByEmpresa([FromHeader] string usuario, int idEmpresa){
            try{
                // se valida header
                if(usuario == null) {
                    Console.WriteLine("[GetDocumentsByEmpresa] -> falta header 'usuario' en la request");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Falta header 'usuario' en la request"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                // busqueda de documentos
                Console.WriteLine("[GetDocumentsByEmpresa] -> se van a buscar los documentos de la empresa con id: " + idEmpresa);
                List<DocumentoDTO> result = documentosService.FindDocumentosByEmpresa(usuario,idEmpresa);
                // validacion de resultados
                if(result == null || result.Count() == 0){
                    Console.WriteLine("[GetDocumentsByEmpresa] -> no se encontraron resultados");
                }else{
                    Console.WriteLine("[GetDocumentsByEmpresa] -> imprimiendo resultados");
                    foreach(DocumentoDTO d in result){
                        response.AddObjectToData(d);
                    }
                }
                return Ok(response);
            } catch(Exception exception) {
                RestResponse response = RestUtils.GenerateResponseErrorWith(
                    new ResponseError(
                        exception.Message,
                        exception.GetType().ToString()
                    )
                );
                // respuesta usuario sin permisos            
                if(typeof(AccessDeniedException).IsInstanceOfType(exception)){
                    Console.WriteLine("[GetDocumentsByEmpresa] ->" + exception.Message);
                    response.Header.Message = exception.Message;
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        response
                    );
                }
                // errores generales
                Console.WriteLine("[GetDocumentsByEmpresa] ->" + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
                response.Header.Message = RestUtils.RESPONSE_INTERNAL_ERROR_MSG;
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    response
                );
            }
        }

        [HttpGet("/api/documentos/empresa/{idEmpresa}/sector/{idSector}")]
        public IActionResult GetDocumentsBySector([FromHeader] string usuario, int idEmpresa, int idSector){
            try{
                // se valida header
                if(usuario == null) {
                    Console.WriteLine("[GetDocumentsBySector] -> falta header 'usuario' en la request");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Falta header 'usuario' en la request"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                // busqueda de documentos
                Console.WriteLine("[GetDocumentsBySector] -> se van a buscar los documentos de la empresa con id: " + idEmpresa + " y sector " + idSector);
                List<DocumentoDTO> result = documentosService.FindDocumentosBySector(usuario,idEmpresa,idSector);
                // validacion de resultados
                if(result == null || result.Count() == 0){
                    Console.WriteLine("[GetDocumentsBySector] -> no se encontraron resultados");
                }else{
                    Console.WriteLine("[GetDocumentsBySector] -> imprimiendo resultados");
                    foreach(DocumentoDTO d in result){
                        response.AddObjectToData(d);
                    }
                }
                return Ok(response);
            } catch(Exception exception) {
                RestResponse response = RestUtils.GenerateResponseErrorWith(
                    new ResponseError(
                        exception.Message,
                        exception.GetType().ToString()
                    )
                );
                // respuesta usuario sin permisos            
                if(typeof(AccessDeniedException).IsInstanceOfType(exception)){
                    Console.WriteLine("[GetDocumentsBySector] ->" + exception.Message);
                    response.Header.Message = exception.Message;
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        response
                    );
                }
                // errores generales
                Console.WriteLine("[GetDocumentsBySector] ->" + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
                response.Header.Message = RestUtils.RESPONSE_INTERNAL_ERROR_MSG;
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    response
                );
            }
        }

        [HttpGet("/api/documentos/get/")]
        public IActionResult GetDocumento(
            [FromHeader] string documento,
            [FromHeader] string usuario,
            [FromHeader] int empresa,
            [FromHeader] int sector) {
            try{
                // se validan headers
                if(documento == null || usuario == null || empresa == 0 || sector == 0) {
                    Console.WriteLine("[GetDocumento] -> Faltan headers en la request");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Faltan headers en la request"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                // se ejecuta busqueda
                Console.WriteLine("[GetDocumento] -> Buscar documento numero: " + documento);
                List<DocumentoDTO> result = documentosService.FindDocumentoWith(usuario,documento,empresa,sector);
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                // se validan resultados
                if(result == null || result.Count() == 0){
                    Console.WriteLine("[GetDocumento] -> no hay resultados");
                    response.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(response);
                }
                Console.WriteLine("[GetDocumento] -> request exitosa");
                foreach(DocumentoDTO d in result) {
                    response.AddObjectToData(d);
                }
                return Ok(response);
            } catch(Exception exception) {
                RestResponse response = RestUtils.GenerateResponseErrorWith(
                    new ResponseError(
                        exception.Message,
                        exception.GetType().ToString()
                    )
                );
                // respuesta usuario sin permisos            
                if(typeof(AccessDeniedException).IsInstanceOfType(exception)){
                    Console.WriteLine("[GetDocumento] ->" + exception.Message);
                    response.Header.Message = exception.Message;
                    return StatusCode(
                        StatusCodes.Status403Forbidden,
                        response
                    );
                }
                // errores generales
                Console.WriteLine("[GetDocumento] ->" + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
                response.Header.Message = RestUtils.RESPONSE_INTERNAL_ERROR_MSG;
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    response
                );
            }

        }
    }
}
