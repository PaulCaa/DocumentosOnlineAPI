using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocumentosOnlineAPI.Services;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.Rest;
using DocumentosOnlineAPI.Utils;

namespace DocumentosOnlineAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class DocumentosController : ControllerBase {
        private DocumentosService documentosService;

        public DocumentosController(){
            this.documentosService = new DocumentosService(); 
        }

        [HttpGet("/api/documentos/empresa/{id}")]
        public IActionResult GetDocumentsByEmpresa(int id){
            try{
                Console.WriteLine("Se van a buscar los documentos de la empresa con id: " + id);
                List<Documento> docs = documentosService.FindDocumentsByEmpresa(id);
                Console.WriteLine(docs.ToString());
                RestResponse response = RestUtils.GenerateResponseOkWithData(docs);
                return Ok(response);
            }catch(Exception exception){
                Console.WriteLine(RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
                RestResponse response = RestUtils.GenerateResponseErrorWith(
                    new ResponseError(
                        exception.Message,
                        exception.GetType().ToString()
                    )
                );
                response.Header.Message = RestUtils.RESPONSE_INTERNAL_ERROR_MSG;
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    response
                );
            }
        }
    }
}
