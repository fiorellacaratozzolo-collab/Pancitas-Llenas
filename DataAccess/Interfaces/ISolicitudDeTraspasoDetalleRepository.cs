using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISolicitudDeTraspasoDetalleRepository
    {
        void Create(SolicitudDeTraspasoDeProductosDetalle detalle);
        List<SolicitudDeTraspasoDeProductosDetalle> GetAll();
        List<SolicitudDeTraspasoDeProductosDetalle> GetByIdSolicitud(Guid idSolicitud);
    }
}
