using System;
using System.Collections.Generic;
using System.Linq;
//using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class DocumentosService {

        public List<Documento> FindDocumentsByEmpresa(int id){
            try{
                Console.WriteLine("Buscando documentos en base de datos");
                return new List<Documento>();
            }catch(Exception exception){
                Console.WriteLine("Error en proceso de lectura en base de datos");
                throw new DocumentosDatabaseException("Error en proceso de lectura en base de datos",exception);
            }
        }
    }
}