using System;
using System.Collections.Generic;
using System.Linq;
using DocumentosOnlineAPI.Data;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Exceptions;

namespace DocumentosOnlineAPI.Services {
    public class UsuariosService {

        public List<Usuario> ListAllUsuarios() {
            List<Usuario> usuarios = null;
            try{
                Console.WriteLine("[UsuariosService] -> listando todos los usuarios en base de datos");
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    usuarios = db.Usuarios.ToList();
                }
                string str = "[";
                foreach(Usuario o in usuarios) str += o.ToString();
                str += "]";
                Console.WriteLine("[SectoresService] -> se encontraron: " + str);
                return usuarios;
            } catch (Exception exception) {
                Console.WriteLine("[UsuariosService] -> se produjo un error error en acceso a la base de datos");
                Console.WriteLine("[UsuariosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

        public Usuario FindUsuarioBy(string userId) {
            Usuario usuario = null;
            try{
                Console.WriteLine("[UsuariosService] -> buscando usuario en base de datos");
                using(DocumentosDbContext db = new DocumentosDbContext()) {
                    usuario = db.Usuarios.Where(us => us.UsuarioId == userId).First();
                }
                if(usuario == null || usuario.UsuarioId.Count() == 0){
                    Console.WriteLine("[UsuariosService] -> no se encontraron resultado en base de datos");
                    return null;
                }
                Console.WriteLine("[UsuariosService] -> resultado: " + usuario.ToString());
                return usuario;
            } catch(InvalidOperationException exception) {
                Console.WriteLine("[UsuariosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                return null;
            } catch (Exception exception) {
                Console.WriteLine("[UsuariosService] -> se produjo un error error en acceso a la base de datos");
                Console.WriteLine("[UsuariosService] -> " + exception.GetType().ToString() + ": " + exception.Message);
                throw new DocumentosDatabaseException("Se produjo un error error en acceso a la base de datos",exception);
            }
        }

    }
}