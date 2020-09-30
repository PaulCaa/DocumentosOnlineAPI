using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocumentosOnlineAPI.Services;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;
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

        [HttpPost]
        public IActionResult InsertEmpresa([FromBody] EmpresaDTO body) {
            try{
                Console.WriteLine("[InsertEmpresa] -> request: " + body.ToString());
                // se valida body de request
                if(body == null || body.Nombre == null) {
                    Console.WriteLine("[InsertEmpresa] -> empresa sin nombre o body nulo");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Empresa sin nombre o request body nulo"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                // se realiza insersion
                int result = empresasService.AddNewEmpresa(
                    ModelMapper.Map(body)
                );
                // se valida resultado de operacion
                if(result == 0){
                    Console.WriteLine("[InsertEmpresa] -> operacion fallida");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_INTERNAL_ERROR_MSG,
                            "Operacion fallida, no se completo proceso"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_ERROR_CODE;
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        responseErr
                    );
                }
                body.Id = result;
                Console.WriteLine("[InsertEmpresa] -> operacion exitosa");
                return StatusCode(
                    StatusCodes.Status201Created,
                    RestUtils.GenerateResponseOkWithData(body)
                );
            } catch(Exception exception) {
                Console.WriteLine("[InsertEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpPost("/api/empresa/{idEmpresa}/sector")]
        public IActionResult InsertNewSectorEmpresa([FromBody] SectorDTO body, int idEmpresa) {
            try{
                Console.WriteLine("[InsertNewSectorEmpresa] -> request: " + body.ToString());
                // se valida param y body de request
                if(body == null || body.Nombre == null || idEmpresa == 0) {
                    Console.WriteLine("[InsertNewSectorEmpresa] -> falta sector o idEmpresa en request");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Falta sector o idEmpresa en request"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                // se realiza insersion
                int result = empresasService.AddNewSectorInEmpresa(
                    idEmpresa,
                    ModelMapper.Map(body)
                );
                // se valida resultado de operacion
                if(result == 0){
                    Console.WriteLine("[InsertNewSectorEmpresa] -> operacion fallida");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_INTERNAL_ERROR_MSG,
                            "Operacion fallida, no se completo proceso"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_ERROR_CODE;
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        responseErr
                    );
                }else if(result == -99){
                    Console.WriteLine("[InsertNewSectorEmpresa] -> operacion fallida");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_NOTFOUND_MSG,
                            "Operacion fallida, no se pudo insertar sector porque no hay empresa asociada al id" + idEmpresa
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_ERROR_CODE;
                    return NotFound(responseErr);
                }
                body.EmpresaId = idEmpresa;
                body.SectorId = result;
                Console.WriteLine("[InsertNewSectorEmpresa] -> operacion exitosa");
                return StatusCode(
                    StatusCodes.Status201Created,
                    RestUtils.GenerateResponseOkWithData(body)
                );
            } catch(Exception exception) {
                Console.WriteLine("[InsertNewSectorEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpPut]
        public IActionResult UpdateEmpresa([FromBody] EmpresaDTO body){
            try{
                Console.WriteLine("[UpdateEmpresa] -> request body: " + body.ToString());
                // se validan datos de entrada
                if(body == null || body.Id == 0) {
                    Console.WriteLine("[UpdateEmpresa] -> falta id de empresa en request body");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Falta id de empresa en request body"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                // se envia info a modificar
                EmpresaDTO result = empresasService.ModifyEmpresa(
                    body.Id,
                    ModelMapper.Map(body)
                );
                // se valida resultado
                if(result == null) {
                    Console.WriteLine("[UpdateEmpresa] -> operacion fallida");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_NOTFOUND_MSG,
                            "Operacion fallida, no se puede modifica empresa , ya que no hay resultados asociados al id" + body.Id
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_ERROR_CODE;
                    return NotFound(responseErr);
                }
                Console.WriteLine("[UpdateEmpresa] -> operacion exitosa");
                return Ok(RestUtils.GenerateResponseOkWithData(result));
            } catch(Exception exception) {
                Console.WriteLine("[UpdateEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpDelete("{idEmpresa}")]
        public IActionResult DeleteEmpresaWith(int idEmpresa) {
            try{
                // validacion de parametro
                if(idEmpresa < 1){
                    Console.WriteLine("[DeleteEmpresaWith] -> idEmpresa invalido");
                    return BadRequest(
                        RestUtils.GenerateResponseErrorWith(
                            new ResponseError(RestUtils.RESPONSE_BADREQUEST_CODE,"idEmpresa invalido")
                        )
                    );
                }
                Console.WriteLine("[DeleteEmpresaWith] -> se va a eliminar empresa con id: " + idEmpresa);
                EmpresaDTO result = empresasService.DeleteEmpresa(idEmpresa);
                if(result == null){
                    Console.WriteLine("[DeleteEmpresaWith] -> no se encontro empresa a eliminar");
                    return NotFound(
                        RestUtils.GenerateResponseErrorWith(
                            new ResponseError(RestUtils.RESPONSE_NOTFOUND_MSG,"No se encontro empresa con id " + idEmpresa)
                        )
                    );
                }
                Console.WriteLine("[DeleteEmpresaWith] -> operacion exitosa");
                return Ok(RestUtils.GenerateResponseOkWithData(result));
            }catch(Exception exception) {
                Console.WriteLine("[UpdateEmpresa] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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
