using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Exceptions;
using DocumentosOnlineAPI.Utils;

namespace DocumentosOnlineAPI.Services {
    public class DocumentosService {

        public List<DocumentoDTO> FindDocumentosByEmpresa(string usuario, int idEmpresa){
            if(!ValidateUser(usuario,idEmpresa)) {
                throw new AccessDeniedException();
            }
            try{
                List<Documento> docs = null;
                Console.WriteLine("[DocumentosService] -> buscando documentos de empresa " + idEmpresa);
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    docs = db.Documentos.Where(d => d.EmpresaId == idEmpresa).ToList();
                }
                Console.WriteLine("[DocumentosService] -> se encontraron " + docs.Count() + " resultados");
                return ProcessResult(docs);
            } catch(InvalidOperationException exception) {
                Console.WriteLine("[DocumentosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                return null;
            } catch(Exception exception){
                Console.WriteLine("[DocumentosService] -> error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }

        public List<DocumentoDTO> FindDocumentosBySector(string usuario, int idEmpresa, int idSector) {
            if(!ValidateUser(usuario,idEmpresa,idSector)) {
                throw new AccessDeniedException();
            }
            try{
                List<Documento> docs = null;
                Console.WriteLine("[DocumentosService] -> buscando documentos de empresa " + idEmpresa + " y sector " + idSector);
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    docs = db.Documentos.Where(d => d.EmpresaId == idEmpresa & d.SectorId == idSector).ToList();
                }
                Console.WriteLine("[DocumentosService] -> se encontraron " + docs.Count() + " resultados");
                return ProcessResult(docs);
            } catch(InvalidOperationException exception) {
                Console.WriteLine("[DocumentosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                return null;
            } catch(Exception exception){
                Console.WriteLine("[DocumentosService] -> error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }

        public List<DocumentoDTO> FindDocumentoWith(string usuario, string number, int idEmpresa, int idSector) {
            if(!ValidateUser(usuario,idEmpresa,idSector)) {
                throw new AccessDeniedException();
            }
            try{
                List<Documento> docs = null;
                Console.WriteLine("[DocumentosService] -> buscando documento: " + number);
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    docs = db.Documentos.Where(d => d.Numero == number && d.EmpresaId == idEmpresa && d.SectorId == idSector).ToList();
                }
                Console.WriteLine("[DocumentosService] -> se encontraron " + docs.Count() + " resultados");
                return ProcessResult(docs);
            } catch(InvalidOperationException exception) {
                Console.WriteLine("[DocumentosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                return null;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }

        /**** LIST MODELT -> LIST DTO *****/
        private List<DocumentoDTO> ProcessResult(List<Documento> docs) {
            List<DocumentoDTO> dtoList = new List<DocumentoDTO>();
            Console.WriteLine("[DocumentosService] -> procesando resultados");
            foreach(Documento d in docs) {
                DocumentoDTO dto = ModelMapper.Map(d);
                dto.Empresa = getNombreEmpresaBy(d.EmpresaId);
                dto.Sector = getNombreSectorBy(d.SectorId, d.EmpresaId);
                dtoList.Add(dto);
            }
            return dtoList;
        }
        
        /**** OBTENER NOMBRES ASOCIADOS A IDs *****/
        private string getNombreEmpresaBy(int idEmpresa) {
            Empresa e = null;
            Console.WriteLine("[DocumentosService] -> buscando nombre de empresa " + idEmpresa);
            using(DocumentosDbContext db = new DocumentosDbContext()) {
                e = db.Empresas.Where(e => e.EmpresaId == idEmpresa).FirstOrDefault();
            }
            if(e == null) {
                Console.WriteLine("[DocumentosService] -> no se encontro nombre de empresa asociado al id " + idEmpresa);
                return "";
            }
            return e.Nombre;
        }

        private string getNombreSectorBy(int idSector, int idEmpresa) {
            Sector s = null;
            Console.WriteLine("[DocumentosService] -> buscando nombre de sector " + idSector);
            using(DocumentosDbContext db = new DocumentosDbContext()) {
                s = db.Sectores.Where(s => s.SectorId == idSector & s.EmpresaId == idEmpresa).FirstOrDefault();
            }
            if(s == null) {
                Console.WriteLine("[DocumentosService] -> no se encontro nombre del sector asociado al id " + idEmpresa);
                return "";
            }
            return s.Nombre;
        }

        /**** VALIDACIONES DE PERMISOS *****/
        private bool ValidateUser(string user, int idEmpresa, int idSector) {
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

        private bool ValidateUser(string user, int idEmpresa) {
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
        }
    }
}