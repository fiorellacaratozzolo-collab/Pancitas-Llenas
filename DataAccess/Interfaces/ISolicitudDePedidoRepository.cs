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
        void Update(SolicitudDePedido solicitud); // Para actualizar el estado o detalles
        SolicitudDePedido? GetByIdWithDetails(Guid id); // Un método más claro si GetById no garantiza los Includes.

    }
}
