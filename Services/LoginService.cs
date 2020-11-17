using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Utils;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class LoginService {

        public UsuarioDTO ValidateUser(string userId, string hashPwd) {
            try{
                Usuario result = null;
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    Console.WriteLine("[LoginService] -> Buscando registro de usuario");
                    result = db.Usuarios.Where(u => u.UsuarioId == userId).First();
                }
                if(result == null) {
                    Console.WriteLine("[LoginService] -> No se encontraron resultados");
                    return null;
                }
                if(result.HashPwd != hashPwd){
                    Console.WriteLine("[LoginService] -> Contraseña invalida");
                    return null;
                }
                string empresa = getNombreEmpresaBy(result.EmpresaId);
                return ModelMapper.Map(result,empresa);
            } catch(Exception exception) {
                Console.WriteLine("[LoginService] -> error en operacion de validacion de usuario");
                throw new DocumentosDatabaseException("Error en operacion de validacion de usuario", exception);
            }
        }

        public string getNombreEmpresaBy(int idEmpresa) {
            Empresa e = null;
            Console.WriteLine("[LoginService] -> buscando nombre de empresa " + idEmpresa);
            using(DocumentosDbContext db = new DocumentosDbContext()) {
                e = db.Empresas.Where(e => e.EmpresaId == idEmpresa).FirstOrDefault();
            }
            if(e == null) {
                Console.WriteLine("[LoginService] -> no se encontro nombre de empresa asociado al id " + idEmpresa);
                return "";
            }
            return e.Nombre;
        }

        public string getNombreSectorBy(int idSector, int idEmpresa) {
            Sector s = null;
            Console.WriteLine("[LoginService] -> buscando nombre de sector " + idSector);
            using(DocumentosDbContext db = new DocumentosDbContext()) {
                s = db.Sectores.Where(s => s.SectorId == idSector & s.EmpresaId == idEmpresa).FirstOrDefault();
            }
            if(s == null) {
                Console.WriteLine("[LoginService] -> no se encontro nombre del sector asociado al id " + idEmpresa);
                return "";
            }
            return s.Nombre;
        }

    }
}