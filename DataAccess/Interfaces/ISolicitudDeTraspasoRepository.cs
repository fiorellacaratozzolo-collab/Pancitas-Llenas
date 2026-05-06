using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    /// <summary>
    /// Define el contrato de persistencia para las cabeceras de las solicitudes de transferencia de inventario.
    /// </summary>
    public interface ISolicitudDeTraspasoRepository
    {
        /// <summary>Inserta una nueva solicitud de traspaso y retorna su identificador único.</summary>
        Guid Create(SolicitudDeTraspasoDeProducto solicitud);

        /// <summary>Recupera el listado completo de solicitudes de traspaso del sistema.</summary>
        List<SolicitudDeTraspasoDeProducto> GetAll();

        /// <summary>Filtra y obtiene todas las solicitudes emitidas desde una sucursal origen específica.</summary>
        List<SolicitudDeTraspasoDeProducto> GetTodasPorSucursalOrigen(Guid idSucursal);

        /// <summary>Recupera una solicitud de traspaso específica por su identificador.</summary>
        SolicitudDeTraspasoDeProducto GetById(Guid idSolicitud);

        /// <summary>Actualiza el estado u otros datos de una cabecera de solicitud existente.</summary>
        void Update(SolicitudDeTraspasoDeProducto solicitud);
    }
}