using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Utils;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {

    public class LoginServiceOLD {

        /*public Boolean ValidateUser(string user, int idEmpresa, int idSector) {
            if(!ValidateUser(user,idEmpresa)){
                return false;
            }
            try {
                UsuarioSector valid = null;
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    valid = db.UsuarioSectores.Where(us => us.UsuarioId == user & us.SectorId == idSector).First();
                }
                if(valid == null) {
                    Console.WriteLine("[DocumentosService] -> usuario usuario sin permisos para este sector");
                    return false;
                }
                Console.WriteLine("[DocumentosService] -> validacion de usuario exitosa");
                return true;
            } catch(InvalidOperationException exception) {
                Console.WriteLine("[DocumentosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                return false;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> error en operacion de validacion de usuario");
                throw new DocumentosDatabaseException("Error en operacion de validacion de usuario", exception);
            }
        }

        public Boolean ValidateUser(string user, int idEmpresa) {
            try{
                Console.WriteLine("[DocumentosService] -> iniciando validacion de usuario");
                UsuariosService usuariosService = new UsuariosService();
                Usuario result = usuariosService.FindUsuarioBy(user);
                if(result == null || result.UsuarioId == null 
                    || result.EmpresaId != idEmpresa) {
                        Console.WriteLine("[DocumentosService] -> validacion de usuario fallida");
                        return false;
                }
                Console.WriteLine("[DocumentosService] -> se encontro usuario: " + result.ToString());
                return true;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> error en operacion de validacion de usuario");
                throw new DocumentosDatabaseException("Error en operacion de validacion de usuario", exception);
            }
        }*/
    }
}