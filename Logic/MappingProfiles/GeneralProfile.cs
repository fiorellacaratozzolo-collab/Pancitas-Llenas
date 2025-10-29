using AutoMapper;
using DataAccess.Models;
using ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MappingProfiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Mapeo bidireccional (DAO Entity <-> DTO)
            // Mapeo inverso: de DTO a Entidad para guardar
                
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<EstadoOcenum, EstadoOcenumDTO>().ReverseMap();
            CreateMap<EstadoOpenum, EstadoOpenumDTO>().ReverseMap();
            CreateMap<EstadoSpenum, EstadoSpenumDTO>().ReverseMap();
            CreateMap<EstadoStockEnum, EstadoStockEnumDTO>().ReverseMap();
            CreateMap<EstadoStpenum, EstadoStpenumDTO>().ReverseMap();
            CreateMap<OrdenDeCompra, OrdenDeCompraDTO>().ReverseMap();
            CreateMap<OrdenDeCompraDetalle, OrdenDeCompraDetalleDTO>().ReverseMap();
            CreateMap<OrdenDePedido, OrdenDePedidoDTO>().ReverseMap();
            CreateMap<OrdenDePedidoDetalle, OrdenDePedidoDetalleDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<ProveedorProducto, ProveedorProductoDTO>().ReverseMap();
            CreateMap<SolicitudDePedido, SolicitudDePedidoDTO>().ReverseMap();
            CreateMap<SolicitudDePedidoDetalle, SolicitudDePedidoDetalleDTO>().ReverseMap();
            CreateMap<SolicitudDeTraspasoDeProducto, SolicitudDeTraspasoDeProductoDTO>().ReverseMap();
            CreateMap<SolicitudDeTraspasoDeProductosDetalle, SolicitudDeTraspasoDeProductosDetalleDTO>().ReverseMap();
            CreateMap<StockPorSucursal, StockPorSucursalDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<TipoClienteEnum, TipoClienteEnumDTO>().ReverseMap();
            CreateMap<TipoSucursalEnum, TipoSucursalEnumDTO>().ReverseMap();
            CreateMap<TipoVentaEnum, TipoVentaEnumDTO>().ReverseMap();
            CreateMap<Ventum, VentumDTO>().ReverseMap();
            CreateMap<VentaDetalle, VentaDetalleDTO>().ReverseMap();
            CreateMap<SolicitudDePedidoDTO, SolicitudDePedido>().ReverseMap();
            CreateMap<SolicitudDePedidoDetalleDTO, SolicitudDePedidoDetalle>()
                .ForMember(dest => dest.IdSolicitudDePedidoDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdSolicitudDePedidoNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IdProductoNavigation, opt => opt.Ignore()) // ← CLAVE
                .ForMember(dest => dest.IdSolicitudDePedido, opt => opt.Ignore());
        }
    }
}
