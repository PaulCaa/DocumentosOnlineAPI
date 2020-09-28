using System;
using System.Text.Json;
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
    public class EmpresasController : ControllerBase {
        private EmpresasService empresasService;

        public EmpresasController(){
            this.empresasService = new EmpresasService(); 
        }

        [HttpGet]
        public IActionResult GetAll(){
            try{
                Console.WriteLine("Se van a listar las empresas");
                List<Empresa> result = empresasService.ListAllEmpresas();
                if(result == null){
                    result = new List<Empresa>();
                }
                string empStr = "";
                foreach(Empresa e in result){
                    empStr += e.Nombre + " ";
                }
                Console.WriteLine("Resultado de busqueda: " + empStr);
                RestResponse response = RestUtils.GenerateResponseOkWithData(result.ToString());
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


        [HttpGet("/api/empresas/{id}")]
        public IActionResult GetDocumentsByEmpresa(int id){
            try{
                Console.WriteLine("Se va a buscar empresa con id: " + id);
                Empresa result = empresasService.FindEmpresaBy(id);
                if(result == null){
                    result = new Empresa();
                }
                Console.WriteLine("Resultado de busqueda: " + result.ToString());
                RestResponse response = RestUtils.GenerateResponseOkWithData(result);
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
