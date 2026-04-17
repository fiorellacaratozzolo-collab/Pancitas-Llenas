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
            // 1. MAPEOS BÁSICOS Y ENUMS
            // =========================================================
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<ProveedorProducto, ProveedorProductoDTO>().ReverseMap();
            CreateMap<StockPorSucursal, StockPorSucursalDTO>().ReverseMap();

            CreateMap<EstadoOcenum, EstadoOcenumDTO>().ReverseMap();
            CreateMap<EstadoOpenum, EstadoOpenumDTO>().ReverseMap();
            CreateMap<EstadoSpenum, EstadoSpenumDTO>().ReverseMap();
            CreateMap<EstadoStockEnum, EstadoStockEnumDTO>().ReverseMap();
            CreateMap<TipoClienteEnum, TipoClienteEnumDTO>().ReverseMap();
            CreateMap<TipoSucursalEnum, TipoSucursalEnumDTO>().ReverseMap();

            // =========================================================
            // 2. VENTAS Y STOCKS (Con navegación)
            // =========================================================
            CreateMap<Ventum, VentumDTO>()
                .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.IdClienteNavigation.NombreCliente))
                .ReverseMap()
                .ForMember(dest => dest.IdClienteNavigation, opt => opt.Ignore());

            CreateMap<VentaDetalle, VentaDetalleDTO>().ReverseMap();

            CreateMap<StockPorSucursal, StockPorSucursalDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.Unidad, opt => opt.MapFrom(src => src.IdProductoNavigation.Unidad))
                .ReverseMap();

            // =========================================================
            // 3. FLUJO DE COMPRAS (SP -> OP -> OC) Bidireccional
            // =========================================================

            // --- SOLICITUD DE PEDIDO (SP) ---
            CreateMap<SolicitudDePedido, SolicitudDePedidoDTO>()
                .ForMember(dest => dest.EstadoTexto, opt => opt.MapFrom(src =>
                    src.IdEstadoSp == 1 ? "Pendiente" :
                    src.IdEstadoSp == 2 ? "Aprobada" :
                    src.IdEstadoSp == 3 ? "Rechazada" : "Desconocido"))
                .ReverseMap();

            CreateMap<SolicitudDePedidoDetalle, SolicitudDePedidoDetalleDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ReverseMap();

            // --- ORDEN DE PEDIDO (OP) ---
            CreateMap<OrdenDePedido, OrdenDePedidoDTO>()
                .ForMember(dest => dest.EstadoTexto, opt => opt.MapFrom(src =>
                    src.IdEstadoOp == 1 ? "Pendiente" :
                    src.IdEstadoOp == 2 ? "Aprobada" :
                    src.IdEstadoOp == 3 ? "Rechazada" : "Desconocido"))
                .ReverseMap();

            CreateMap<OrdenDePedidoDetalle, OrdenDePedidoDetalleDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ReverseMap();

            // --- ORDEN DE COMPRA (OC) ---
            CreateMap<OrdenDeCompra, OrdenDeCompraDTO>()
                .ForMember(dest => dest.NombreProveedor, opt => opt.MapFrom(src => src.IdProveedorNavigation.NombreProveedor))
                .ForMember(dest => dest.EstadoTexto, opt => opt.MapFrom(src =>
                    src.IdEstadoOc == 1 ? "Pendiente" :
                    src.IdEstadoOc == 2 ? "Aprobada" :
                    src.IdEstadoOc == 3 ? "Rechazada" : "Desconocido"))
                .ForMember(dest => dest.IdEstadoOcNavigation, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<OrdenDeCompraDetalle, OrdenDeCompraDetalleDTO>()
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ReverseMap();


            // =========================================================
            // 4. TRASPASOS ENTRE SUCURSALES (STP)
            // =========================================================
            CreateMap<SolicitudDeTraspasoDeProducto, SolicitudDeTraspasoDeProductoDTO>()
                .ForMember(dest => dest.NombreSucursalOrigen, opt => opt.MapFrom(src => src.IdSucursalOrigenNavigation.NombreSucursal))
                .ForMember(dest => dest.NombreSucursalDestino, opt => opt.MapFrom(src => src.IdSucursalDestinoNavigation.NombreSucursal))
                .ForMember(dest => dest.DireccionSucursalDestino, opt => opt.MapFrom(src => src.IdSucursalDestinoNavigation.Direccion))
                .ForMember(dest => dest.EstadoTexto, opt => opt.MapFrom(src =>
                    src.IdEstadoStp == 1 ? "Pendiente" :
                    src.IdEstadoStp == 2 ? "Aprobada" :
                    src.IdEstadoStp == 3 ? "Rechazada" : "Desconocido"))
                .ForMember(dest => dest.IdEstadoStpNavigation, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<SolicitudDeTraspasoDeProductosDetalle, SolicitudDeTraspasoDeProductosDetalleDTO>()
                .ForMember(dest => dest.NombreProducto, opt => opt.MapFrom(src => src.IdProductoNavigation.NombreProducto))
                .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.IdProductoNavigation.Marca))
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ReverseMap();


            // =========================================================
            // 5. MAPEOS DE TRANSICIÓN (Lógica de Negocio SP -> OP -> OC)
            // =========================================================

            // SP -> OP
            CreateMap<SolicitudDePedido, OrdenDePedido>()
                .ForMember(dest => dest.IdOrdenDePedido, opt => opt.Ignore())
                .ForMember(dest => dest.FechaOp, opt => opt.Ignore())
                .ForMember(dest => dest.IdEstadoOp, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore());

            CreateMap<SolicitudDePedidoDetalle, OrdenDePedidoDetalle>()
                .ForMember(dest => dest.IdOrdenDePedidoDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdOrdenDePedido, opt => opt.Ignore())
                .ForMember(dest => dest.PrecioUnitario, opt => opt.Ignore());

            // OP -> OC
            CreateMap<OrdenDePedido, OrdenDeCompra>()
                .ForMember(dest => dest.IdOrdenDeCompra, opt => opt.Ignore())
                .ForMember(dest => dest.FechaOc, opt => opt.Ignore())
                .ForMember(dest => dest.IdEstadoOc, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));

            CreateMap<OrdenDePedidoDetalle, OrdenDeCompraDetalle>()
                .ForMember(dest => dest.IdOrdenDeCompraDetalle, opt => opt.Ignore())
                .ForMember(dest => dest.IdOrdenDeCompra, opt => opt.Ignore())
                .ForMember(dest => dest.Cantidad, opt => opt.MapFrom(src => src.Cantidad))
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
                .ForMember(dest => dest.PesoNeto, opt => opt.MapFrom(src => src.IdProductoNavigation.PesoNeto))
                .ForMember(dest => dest.Unidad, opt => opt.Ignore());
        }
    }
}