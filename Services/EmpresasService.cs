using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class EmpresasService {
        private DocumentosDbContext DbContext;

        public EmpresasService() {
            this.DbContext = new DocumentosDbContext();
        }

        public List<Empresa> ListAllEmpresas() {
            try{
                Console.WriteLine("Buscando empresas en base de datos");
                return DbContext.Empresas.ToList();
            } catch (Exception exception) {
                Console.WriteLine("Se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

        public Empresa FindEmpresaBy(int id) {
            try{
                Console.WriteLine("Buscando empresa por id en base de datos");
                return DbContext.Empresas.Find(id);
            } catch (Exception exception) {
                Console.WriteLine("Se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }
    }
}