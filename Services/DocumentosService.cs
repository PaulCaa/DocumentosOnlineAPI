using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class DocumentosService {

        public List<Documento> FindDocumentsByEmpresa(int id){
            try{
                Console.WriteLine("[DocumentosService] -> buscando documentos en base de datos");
                return new List<Documento>();
            }catch(Exception exception){
                Console.WriteLine("[DocumentosService] -> error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }

        public List<Documento> FindDocumentoWith(string user, string number, int idEmpresa, int idSector) {
            bool valid = ValidateUser(user,idEmpresa,idSector);
            if(!valid) {
                throw new AccessDeniedException();
            }
            try{
                List<Documento> docs = null;
                Console.WriteLine("[DocumentosService] -> buscando documento: " + number);
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    docs = db.Documentos.Where(d => d.Numero == number & d.EmpresaId == idEmpresa & d.SectorId == idSector).ToList();
                }
                Console.WriteLine("[DocumentosService] -> se encontraron " + docs.Count() + " resultados");
                return docs;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }


        private bool ValidateUser(string user, int idEmpresa, int idSector) {
            try {
                Console.WriteLine("[DocumentosService] -> iniciando validacion de usuario");
                UsuariosService usuariosService = new UsuariosService();
                Usuario result = usuariosService.FindUsuarioBy(user);
                if(result == null || result.UsuarioId == null 
                    || result.EmpresaId != idEmpresa) {
                        Console.WriteLine("[DocumentosService] -> validacion de usuario fallida");
                        return false;
                }
                Console.WriteLine("[DocumentosService] -> se encontro usuario: " + result.ToString());
                UsuarioSector valid = null;
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    valid = db.UsuarioSectores.Where(us => us.UsuarioId == user && us.SectorId == idSector).First();
                }
                if(valid == null) {
                    Console.WriteLine("[DocumentosService] -> usuario usuario sin permisos para este sector");
                    return false;
                }
                Console.WriteLine("[DocumentosService] -> validacion de usuario exitosa");
                return true;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> error en operacion de validacion de usuario");
                throw new DocumentosDatabaseException("Error en operacion de validacion de usuario", exception);
            }
        }
    }
}