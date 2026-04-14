using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISolicitudDeTraspasoRepository
    {
        Guid Create(SolicitudDeTraspasoDeProducto solicitud);
        List<SolicitudDeTraspasoDeProducto> GetAll();
        List<SolicitudDeTraspasoDeProducto> GetPendientesPorSucursalOrigen(Guid idSucursalOrigen);
        SolicitudDeTraspasoDeProducto GetById(Guid idSolicitud);
        void Update(SolicitudDeTraspasoDeProducto solicitud);
    }
}
