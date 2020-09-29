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
    public class UsuariosController : ControllerBase {

        private UsuariosService usuariosService;

        public UsuariosController() {
            this.usuariosService = new UsuariosService();
        }
        
        [HttpGet]
        public IActionResult GetAllUsuarios() {
            try {
                Console.WriteLine("[GetAllUsuarios] -> listar todos los usuarios");
                List<Usuario> result = usuariosService.ListAllUsuarios();
                if(result == null || result.Count() == 0) {
                    Console.WriteLine("[GetAllUsuarios] -> no hay resultados");
                    RestResponse r = RestUtils.GenerateResponseOkEmpty();
                    r.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(r);
                }
                Console.WriteLine("[GetAllUsuarios] -> request exitosa");
                RestResponse response = RestUtils.GenerateResponseOkWithData(result);
                return Ok(response);
            } catch(Exception exception) {
                Console.WriteLine("[GetAllUsuarios] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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

        [HttpGet("/api/usuarios/{user}")]
        public IActionResult GetUsuarioBy(string user) {
            try {
                Console.WriteLine("[GetUsuarioBy] -> buscar usuario: " + user);
                Usuario result = usuariosService.FindUsuarioBy(user);
                if(result == null) {
                    Console.WriteLine("[GetUsuarioBy] -> no hay resultados");
                    RestResponse r = RestUtils.GenerateResponseOkEmpty();
                    r.Header.Message = RestUtils.RESPONSE_NOTFOUND_MSG;
                    return NotFound(r);
                }
                Console.WriteLine("[GetUsuarioBy] -> request exitosa");
                RestResponse response = RestUtils.GenerateResponseOkWithData(result);
                return Ok(response);
            } catch(Exception exception) {
                Console.WriteLine("[GetUsuarioBy] -> " + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
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
