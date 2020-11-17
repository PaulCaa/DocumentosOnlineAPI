using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;
using DocumentosOnlineAPI.Exceptions;
using DocumentosOnlineAPI.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocumentosOnlineAPI.Services {
    public class DocumentosService {

        public List<DocumentoDTO> FindDocumentosByEmpresa(int idEmpresa){
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

        public List<DocumentoDTO> FindDocumentosBySector(int idEmpresa, int idSector) {
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

        public List<DocumentoDTO> FindDocumentoWith(string number, int idEmpresa, int idSector) {
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

        public DocumentoDTO AddNewDocument(DocumentoDTO documentoDTO, int idEmpresa, int idSector) {
            try {
                Console.WriteLine("[DocumentosService] -> insertando nuevo documento");
                Documento documento = ModelMapper.Map(documentoDTO);
                documento.EmpresaId = idEmpresa;
                documento.SectorId = idSector;
                int idInserted = 0;
                Console.WriteLine("[DocumentosService] -> insertando documento: " + documento.ToString());
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    EntityEntry<Documento> result = db.Documentos.Add(documento);
                    db.SaveChanges();
                    idInserted = result.Entity.DocumentoId;
                }
                if(idInserted == 0){
                    Console.WriteLine("[DocumentosService] -> operacion fallida");
                    return null;
                }
                documentoDTO.DocumentoId = idInserted;
                documentoDTO.Fecha = documento.Fecha.ToString();
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
            // Armado de objeto respuesta no tiene que afectar el flujo, ya que se insertó registro en DB
            try{
                documentoDTO.Empresa = getNombreEmpresaBy(idEmpresa);
                documentoDTO.Sector = getNombreSectorBy(idSector,idEmpresa);
            } catch (Exception exception) {
                Console.WriteLine("[DocumentosService] -> error al obtener nombre para respuesta: " + exception.Message);
            }
            Console.WriteLine("[DocumentosService] -> se registro documento con id" + documentoDTO.DocumentoId);
            return documentoDTO;
        }

        public int DeleteDocumento(string number, int idEmpresa, int idSector){
            try {
                List<Documento> toDelete = null;
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    toDelete = db.Documentos.Where(d => d.Numero == number & d.EmpresaId == idEmpresa & d.SectorId == idSector).ToList();
                }
                if(toDelete.Count == 0) {
                    Console.WriteLine("[DocumentosService] -> no se encontraron registro a eliminar");
                    return 0;
                }
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    db.Documentos.RemoveRange(toDelete);
                    db.SaveChanges();
                }
                Console.WriteLine("[DocumentosService] -> registros eliminados: " + toDelete.Count);
                return toDelete.Count;
            } catch(Exception exception) {
                Console.WriteLine("[DocumentosService] -> se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
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

    }
}