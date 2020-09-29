using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class SectoresService {

        public List<Sector> ListarSectoresPorEmpresa(int id) {
            List<Sector> sectores = new List<Sector>();
            try{
                Console.WriteLine("[SectoresService] -> buscando sectores en base de datos");
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    sectores = db.Sectores.ToList();
                }
                string str = "[";
                foreach(Sector o in sectores) str += o.ToString();
                str += "]";
                Console.WriteLine("[SectoresService] -> se encontraron: " + str);
                return sectores;
            } catch (Exception exception) {
                Console.WriteLine("[SectoresService] -> se produjo un error error en acceso a la base de datos");
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

    }
}