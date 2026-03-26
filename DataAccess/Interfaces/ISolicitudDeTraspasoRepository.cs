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
        SolicitudDeTraspasoDeProducto? GetById(Guid id);
        void Update(SolicitudDeTraspasoDeProducto solicitud);
    }
}
