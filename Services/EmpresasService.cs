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
    public class EmpresasService {

        public List<Empresa> ListAllEmpresas() {
            List<Empresa> empresas = new List<Empresa>();
            try{
                Console.WriteLine("[EmpresasService] -> listando empresas en base de datos");
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    empresas = db.Empresas.ToList();
                }
                string str = "[";
                foreach(Empresa o in empresas) str += o.ToString();
                str += "]";
                Console.WriteLine("[EmpresasService] -> se encontraron: " + str);
                return empresas;
            } catch (Exception exception) {
                Console.WriteLine("[EmpresasService] -> se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

        public Empresa FindEmpresaBy(int id) {
            Empresa empresa = null;
            try{
                Console.WriteLine("[EmpresasService] -> buscando empresa por id en base de datos");
                using(DocumentosDbContext db = new DocumentosDbContext()){
                    empresa = db.Empresas.Find(id);
                }
                if(empresa == null || empresa.EmpresaId == 0){
                    Console.WriteLine("[EmpresasService] -> no se encontraron resultado en base de datos");
                    return null;
                }
                Console.WriteLine("[EmpresasService] -> resultado: " + empresa.ToString());
                return empresa;
            } catch (Exception exception) {
                Console.WriteLine("[EmpresasService] -> se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

        public int AddNewEmpresa(Empresa empresa) {
            int idGenerated = 0;
            try{
                Console.WriteLine("[EmpresasService] -> insertando nueva empresa");
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    EntityEntry<Empresa> result = db.Empresas.Add(empresa);
                    db.SaveChanges();
                    idGenerated = result.Entity.EmpresaId;
                }
            } catch (Exception exception) {
                Console.WriteLine("[EmpresasService] -> se produjo un error error en el proceso con la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en el proceso con la base de datos",exception);
            }
            if(idGenerated == 0) {
                Console.WriteLine("[EmpresasService] -> no se completo proceso");
            }
            Console.WriteLine("[EmpresasService] -> se registro nueva empresa con id: " + idGenerated);
            return idGenerated;
        }

        public int AddNewSectorInEmpresa(int idEmpresa, Sector sector) {
            Console.WriteLine("[EmpresasService] -> buscando empresa");
            Empresa valid = FindEmpresaBy(idEmpresa);
            if(valid == null || valid.Nombre == null) {
                Console.WriteLine("[EmpresasService] -> no existe empresa id = " + idEmpresa);
                return -99;
            }
            int idGenerated = 0;
            try{
                sector.EmpresaId = idEmpresa;
                Console.WriteLine("[EmpresasService] -> insertando nuevo sector");
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    EntityEntry<Sector> result = db.Sectores.Add(sector);
                    db.SaveChanges();
                    idGenerated = result.Entity.SectorId;
                }
            } catch (Exception exception) {
                Console.WriteLine("[EmpresasService] -> se produjo un error error en el proceso con la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en el proceso con la base de datos",exception);
            }
            if(idGenerated == 0) {
                Console.WriteLine("[EmpresasService] -> no se completo proceso");
            }
            Console.WriteLine("[EmpresasService] -> se registro nuevo sector con id: " + idGenerated);
            return idGenerated;
        }

        public EmpresaDTO ModifyEmpresa(int idEmpresa, Empresa empresa) {
            Console.WriteLine("[EmpresasService] -> buscando empresa con id: " + idEmpresa);
            Empresa valid = FindEmpresaBy(idEmpresa);
            if(valid == null || valid.Nombre == null) {
                Console.WriteLine("[EmpresasService] -> no existe empresa id = " + idEmpresa);
                return null;
            }
            Console.WriteLine("[EmpresasService] -> actualizando empresa...");
            try{
                if(empresa.Nombre != null) valid.Nombre = empresa.Nombre;
                if(empresa.Direccion != null) valid.Direccion = empresa.Direccion;
                if(empresa.Telefono != null) valid.Telefono = empresa.Telefono;
                if(empresa.Web != null) valid.Web = empresa.Web;
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    EntityEntry<Empresa> result= db.Empresas.Update(valid);
                }
                return ModelMapper.Map(valid);
            } catch (Exception exception) {
                Console.WriteLine("[EmpresasService] -> se produjo un error error en el proceso con la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en el proceso con la base de datos",exception);
            }
        }

    }
}