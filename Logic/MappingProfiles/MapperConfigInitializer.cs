using AutoMapper;

namespace Logic.MappingProfiles
{
    /// <summary>
    /// Inicializador estático que configura y provee una instancia única y segura para subprocesos (thread-safe) de AutoMapper en toda la aplicación.
    /// </summary>
    public static class MapperConfigInitializer
    {
        private static readonly Lazy<IMapper> LazyMapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile());
            });

            return config.CreateMapper();
        });

        /// <summary>
        /// Obtiene la instancia global configurada de AutoMapper lista para ser utilizada.
        /// </summary>
        public static IMapper Mapper => LazyMapper.Value;
    }
}
