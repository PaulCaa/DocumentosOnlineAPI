using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocumentosOnlineAPI.Services;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Models.Rest;
using DocumentosOnlineAPI.Utils;

namespace DocumentosOnlineAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {

        private LoginService loginService;

        public LoginController() {
            this.loginService = new LoginService();
        }

        [HttpPost]
        public IActionResult Login([FromBody] UsuarioDTO req) {
            try {
                // Request validation
                if(req == null || req.UsuarioId == null || req.HashPwd == null) {
                    Console.WriteLine("[Login] -> Faltan datos requeridos para login");
                    RestResponse responseErr = RestUtils.GenerateResponseErrorWith(
                        new ResponseError(
                            RestUtils.RESPONSE_BADREQUEST_CODE,
                            "Faltan datos requeridos para login"
                        )
                    );
                    responseErr.Header.Message = RestUtils.RESPONSE_BADREQUEST_MSG;
                    return BadRequest(responseErr);
                }
                Console.WriteLine("[Login] -> Iniciando login para user: " + req.UsuarioId);
                UsuarioDTO result = loginService.ValidateUser(req.UsuarioId,req.HashPwd);
                RestResponse response;
                if(result == null) {
                    response = RestUtils.GenerateResponseErrorEmpty();
                    response.Header.Message = "Fallo autenticacion de usuario";
                } else {
                    response = RestUtils.GenerateResponseOkWithData(result);
                }
                Console.WriteLine("[Login] -> " + response.Header.Message);
                return Ok(response);
            } catch(Exception exception) {
                RestResponse response = RestUtils.GenerateResponseErrorWith(
                    new ResponseError(
                        exception.Message,
                        exception.GetType().ToString()
                    )
                );
                // errores generales
                Console.WriteLine("[Login] ->" + RestUtils.RESPONSE_INTERNAL_ERROR_MSG);
                response.Header.Message = RestUtils.RESPONSE_INTERNAL_ERROR_MSG;
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    response
                );
            }
        }
    }
}