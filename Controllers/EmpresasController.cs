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
        private SectoresService sectoresService;

        public EmpresasController(){
            this.empresasService = new EmpresasService();
            this.sectoresService = new SectoresService();
        }

        [HttpGet]
        public IActionResult GetAllEmpresas() {
            try{
                Console.WriteLine("[GetAllEmpresas] -> listar todas las empresas");
                List<Empresa> result = empresasService.ListAllEmpresas();
                if(result == null || result.Count() == 0){
                    Console.WriteLine("[GetAllEmpresas] -> no hay resultados");
                    RestResponse r = RestUtils.GenerateResponseOkEmpty();
                    r.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(r);
                }
                Console.WriteLine("[GetAllEmpresas] -> request exitosa");
                RestResponse response = RestUtils.GenerateResponseOkWithData(result);
                return Ok(response);
            }catch(Exception exception){
                Console.WriteLine("[GetAllEmpresas] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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


        [HttpGet("/api/empresa/{id}")]
        public IActionResult GetEmpresaBy(int id) {
            try{
                Console.WriteLine("[GetEmpresaBy] -> buscar empresa con id: " + id);
                Empresa result = empresasService.FindEmpresaBy(id);
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                if(result == null){
                    Console.WriteLine("[GetEmpresaBy] -> no hay resultados");
                    response.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(response);
                }
                Console.WriteLine("[GetEmpresaBy] -> request exitosa");
                response.Header.Message = RestUtils.RESPONSE_OK_MSG;
                response.AddObjectToData(result);
                return Ok(response);
            }catch(Exception exception){
                Console.WriteLine("[GetEmpresaBy] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpGet("/api/empresa/{id}/sectores/")]
        public IActionResult GetSectoresByEmpresa(int id) {
            try{
                Console.WriteLine("[GetSectoresByEmpresa] -> listar sectores de la empresa id: " + id);
                List<Sector> result = sectoresService.ListarSectoresPorEmpresa(id);
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                if(result == null || result.Count() == 0){
                    Console.WriteLine("[GetSectoresByEmpresa] -> no hay resultados");
                    response.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(response);
                }
                Console.WriteLine("[GetSectoresByEmpresa] -> request exitosa");
                response.Header.Message = RestUtils.RESPONSE_OK_MSG;
                response.AddObjectToData(result);
                return Ok(response);
            }catch(Exception exception) {
                Console.WriteLine("[GetSectoresByEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpGet("/api/empresa/{idEmpresa}/sector/{idSector}")]
        public IActionResult GetSectorByEmpresa(int idEmpresa, int idSector) {
            try{
                Console.WriteLine("[GetSectorByEmpresa] -> buscar sector (" + idSector + ") en empresa id: " + idEmpresa);
                Sector result = sectoresService.FindSectorBy(idEmpresa,idSector);
                RestResponse response = RestUtils.GenerateResponseOkEmpty();
                if(result == null){
                    Console.WriteLine("[GetSectorByEmpresa] -> no hay resultados");
                    response.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(response);
                }
                Console.WriteLine("[GetSectorByEmpresa] -> request exitosa");
                response.Header.Message = RestUtils.RESPONSE_OK_MSG;
                response.AddObjectToData(result);
                return Ok(response);
            }catch(Exception exception) {
                Console.WriteLine("[GetSectorByEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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
