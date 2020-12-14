using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DocumentosOnlineAPI.Services;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Models.Rest;
using DocumentosOnlineAPI.Utils;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase {

        private LoginService loginService;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration configuration) {
            this.loginService = new LoginService();
            this.configuration = configuration;
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

        [HttpPost("/api/login/bearer/")]
        [AllowAnonymous]
        public IActionResult LoginBearer([FromBody] RequestLogin req) {
            try {
                // Request validation
                if(req == null || req.User == null || req.Password == null) {
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
                Console.WriteLine("[Login] -> Iniciando login para user: " + req.User);
                UsuarioDTO result = loginService.ValidateUser(req.User,req.Password);
                RestResponse response;
                if(result == null) {
                    response = RestUtils.GenerateResponseErrorEmpty();
                    response.Header.Message = "Fallo autenticacion de usuario";
                    Console.WriteLine("[Login] -> " + response.Header.Message);
                    return Unauthorized(response);
                } else {
                    response = RestUtils.GenerateResponseOkWithData(result);
                    Console.WriteLine("[Login] -> " + response.Header.Message);
                    //this.genTokn2(result);
                    string tokenString = this.generateToken(result);
                    Console.WriteLine("[Login] -> token generated");
                    response.Data.Add(new {token = tokenString});
                    return Ok(response);
                }
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

        private string generateToken(UsuarioDTO user) {
            try{
                Console.WriteLine("[Login] -> generating token");
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:SecretKey"]));
                var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                var claims = new [] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UsuarioId),
                    new Claim("name", user.Nombre + " " + user.Apellido),
                    new Claim("email", user.Email),
                    new Claim("empresaId", user.EmpresaId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = new JwtSecurityToken(
                    issuer: this.configuration["Jwt.Issuer"],
                    audience: this.configuration["Jwt.Audiencie"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials:credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            } catch(Exception exception) {
                Console.WriteLine("[Login] -> se produjo un error en la generacion de token");
                Console.WriteLine("causa: " + exception.Message);
                throw new TokenGenerationException(exception);
            }
        }

    }
}