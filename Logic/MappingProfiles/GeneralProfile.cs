using AutoMapper;
using DataAccess.Models;
using ModelsDTO;

namespace Logic.MappingProfiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // =========================================================
            // 1. Mapeos CRUD Bidireccional (Entidad <-> DTO)
            // =========================================================

            // Mapeos de Cabeceras
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<Ventum, VentumDTO>().ReverseMap();
            CreateMap<Ventum, VentumDTO>()
                .ForMember(dest => dest.NombreCliente,
               opt => opt.MapFrom(src => src.IdClienteNavigation.NombreCliente))
                .ReverseMap()
                .ForMember(dest => dest.IdClienteNavigation, opt => opt.Ignore());

            // Mapeos de Estados y Tipos (Enums)
            CreateMap<EstadoOcenum, EstadoOcenumDTO>().ReverseMap();
            CreateMap<EstadoOpenum, EstadoOpenumDTO>().ReverseMap();
            CreateMap<EstadoSpenum, EstadoSpenumDTO>().ReverseMap();
            CreateMap<EstadoStockEnum, EstadoStockEnumDTO>().ReverseMap();
            CreateMap<TipoClienteEnum, TipoClienteEnumDTO>().ReverseMap();
            CreateMap<TipoSucursalEnum, TipoSucursalEnumDTO>().ReverseMap();

            CreateMap<SolicitudDePedido, SolicitudDePedidoDTO>().ReverseMap();
            CreateMap<SolicitudDePedidoDetalle, SolicitudDePedidoDetalleDTO>()
                .ForMember(dest => dest.NombreProducto,
                   opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca,
                   opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ReverseMap();

            CreateMap<OrdenDePedido, OrdenDePedidoDTO>().ReverseMap();
            CreateMap<OrdenDePedidoDetalle, OrdenDePedidoDetalleDTO>().ReverseMap();
            CreateMap<OrdenDePedidoDetalle, OrdenDePedidoDetalleDTO>()
            .ForMember(dest => dest.NombreProducto,
               opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
            .ForMember(dest => dest.Marca,
               opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
            .ReverseMap();

            CreateMap<OrdenDeCompra, OrdenDeCompraDTO>().ReverseMap();
            CreateMap<OrdenDeCompraDetalle, OrdenDeCompraDetalleDTO>().ReverseMap();

            // Otros Mapeos
            CreateMap<ProveedorProducto, ProveedorProductoDTO>().ReverseMap();
            CreateMap<VentaDetalle, VentaDetalleDTO>().ReverseMap();
            CreateMap<StockPorSucursal, StockPorSucursalDTO>().ReverseMap();
            CreateMap<SolicitudDeTraspasoDeProducto, SolicitudDeTraspasoDeProductoDTO>().ReverseMap();
            CreateMap<SolicitudDeTraspasoDeProductosDetalle, SolicitudDeTraspasoDeProductosDetalleDTO>().ReverseMap();

            CreateMap<StockPorSucursal, StockPorSucursalDTO>()
                .ForMember(dest => dest.NombreProducto,
               opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca,
               opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.Unidad, opt => opt.MapFrom(src => src.IdProductoNavigation.Unidad))
                .ReverseMap();

            // =========================================================
            // 2. Mapeos de TRANSICIÓN (Entidad -> Entidad)
            //    Para el flujo SP -> OP -> OC
            // =========================================================

            // A. SolicitudDePedido (SP) -> OrdenDePedido (OP)

            // Cabecera: Copia campos comunes, ignora los que se establecen en la lógica
            CreateMap<SolicitudDePedido, OrdenDePedido>()
                .ForMember(dest => dest.IdOrdenDePedido, opt => opt.Ignore())
                .ForMember(dest => dest.FechaOp, opt => opt.Ignore())
                .ForMember(dest => dest.IdEstadoOp, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore());

            // Detalle: Copia cantidad/producto, ignora los campos específicos de OP
            CreateMap<SolicitudDePedidoDetalle, OrdenDePedidoDetalle>()
                .ForMember(dest => dest.IdOrdenDePedidoDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdOrdenDePedido, opt => opt.Ignore())
                .ForMember(dest => dest.PrecioUnitario, opt => opt.Ignore());


            // B. OrdenDePedido (OP) -> OrdenDeCompra (OC)

            // Cabecera: Copia el Total, ignora los que se establecen en la lógica
            CreateMap<OrdenDePedido, OrdenDeCompra>()
                .ForMember(dest => dest.IdOrdenDeCompra, opt => opt.Ignore())
                .ForMember(dest => dest.FechaOc, opt => opt.Ignore())
                .ForMember(dest => dest.IdEstadoOc, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));;

            // Detalle: Copia cantidad/precio, ignora los campos específicos de OC
            CreateMap<OrdenDePedidoDetalle, OrdenDeCompraDetalle>()
                .ForMember(dest => dest.IdOrdenDeCompraDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdOrdenDeCompra, opt => opt.Ignore())
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))   
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.Unidad, opt => opt.Ignore());

            // Mapeo de Cabecera: OC Entidad -> OC DTO
            CreateMap<OrdenDeCompra, OrdenDeCompraDTO>()
                // Buscamos el Nombre del Proveedor navegando por la relación
                .ForMember(dest => dest.NombreProveedor,
                           opt => opt.MapFrom(src => src.IdProveedorNavigation.NombreProveedor))
                .ReverseMap();

            // Mapeo de Detalle: OC Detalle Entidad -> OC Detalle DTO
            CreateMap<OrdenDeCompraDetalle, OrdenDeCompraDetalleDTO>()
                // Buscamos el Peso y el Nombre navegando por la relación IdProductoNavigation
                .ForMember(dest => dest.PesoNeto,
                           opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.NombreProducto,
                           opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca,
               opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ReverseMap();

            // =========================================================
            // Mapeo de Solicitud de Traspaso (Cabecera)
            // =========================================================
            CreateMap<SolicitudDeTraspasoDeProducto, SolicitudDeTraspasoDeProductoDTO>()
            .ForMember(dest => dest.NombreSucursalOrigen,
               opt => opt.MapFrom(src => src.IdSucursalOrigenNavigation.NombreSucursal))
            .ForMember(dest => dest.NombreSucursalDestino,
               opt => opt.MapFrom(src => src.IdSucursalDestinoNavigation.NombreSucursal))

            // --> ESTO AGREGA LA DIRECCIÓN PARA LA GRILLA <--
            .ForMember(dest => dest.DireccionSucursalDestino,
               opt => opt.MapFrom(src => src.IdSucursalDestinoNavigation.Direccion))

            .ForMember(dest => dest.IdEstadoStpNavigation,
               opt => opt.MapFrom(src => src.IdEstadoStpNavigation))
            .ReverseMap();


            // =========================================================
            // Mapeo de Detalle de Traspaso (Renglones)
            // =========================================================
            CreateMap<SolicitudDeTraspasoDeProductosDetalle, SolicitudDeTraspasoDeProductosDetalleDTO>()
                .ForMember(dest => dest.NombreProducto,
                           opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca,
                           opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ForMember(dest => dest.PesoNeto,
                           opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ReverseMap();
        }
    }
}