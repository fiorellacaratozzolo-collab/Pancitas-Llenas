using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISolicitudDePedidoRepository
    {
        Guid Create(SolicitudDePedido solicitud);
        List<SolicitudDePedido> GetAll();
        SolicitudDePedido? GetById(Guid id);
    }
}
