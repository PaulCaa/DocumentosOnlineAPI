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
    public class EmpresasController : ControllerBase {
        private EmpresasService empresasService;

        public EmpresasController(){
            this.empresasService = new EmpresasService(); 
        }

        [HttpGet]
        public IActionResult GetAll(){
            try{
                RestResponse response = RestUtils.GenerateResponseOkWithData(
                    empresasService.ListAllEmpresas()
                );
                return Ok(response);
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


        [HttpGet("{id}")]
        public IActionResult GetDocumentsByEmpresa(int id){
            try{
                RestResponse response = RestUtils.GenerateResponseOkWithData(
                    empresasService.FindEmpresaBy(id)
                );
                return Ok(response);
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
