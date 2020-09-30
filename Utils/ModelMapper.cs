using System;
using System.Collections.Generic;
using DocumentosOnlineAPI.Models;
using DocumentosOnlineAPI.Models.DTO;

namespace DocumentosOnlineAPI.Utils {
    public class ModelMapper {

        public static EmpresaDTO Map(Empresa m) {
            EmpresaDTO dto = new EmpresaDTO();
            dto.Id = m.EmpresaId;
            if(m.Nombre != null) dto.Nombre = m.Nombre;
            if(m.Direccion != null) dto.Direccion = m.Direccion;
            if(m.Telefono != null) dto.Telefono = m.Telefono;
            if(m.Web != null) dto.Web = m.Web;
            return dto;
        }
        public static Empresa Map(EmpresaDTO dto) {
            Empresa m = new Empresa();
            if(dto.Nombre != null) m.Nombre = dto.Nombre;
            if(dto.Direccion != null) m.Direccion = dto.Direccion;
            if(dto.Telefono != null) m.Telefono = dto.Telefono;
            if(dto.Web != null) m.Web = dto.Web;
            return m;
        }

        public static Sector Map(SectorDTO dto) {
            Sector m = new Sector();
            m.EmpresaId = dto.EmpresaId;
            if(dto.Nombre != null) m.Nombre = dto.Nombre;
            return m;
        }

    }
}