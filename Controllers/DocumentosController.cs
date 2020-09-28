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

                return Ok("response");
            }catch(Exception exception){
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.INTERNAL_ERROR_MSG,
                            exception.Message,
                            exception
                        )
                    )
                );
            }
        }
    }
}
