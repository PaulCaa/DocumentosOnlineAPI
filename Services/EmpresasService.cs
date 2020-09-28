using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;

namespace DocumentosOnlineAPI.Services {
    public class EmpresasService {
        private DocumentosDbContext DbContext;

        public EmpresasService() {
            this.DbContext = new DocumentosDbContext();
        }

        public List<Empresa> ListAllEmpresas() {
            return DbContext.Empresas.ToList();
        }

        public Empresa FindEmpresaBy(int id) {
            return DbContext.Empresas.Find(id);
        }
    }
}