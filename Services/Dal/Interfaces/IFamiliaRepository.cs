using Services.DomainModel.Composite;

namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato para las operaciones de acceso a datos de los roles o grupos de permisos (Familias).
    /// </summary>
    public interface IFamiliaRepository : IGenericRepository<Familia>
    {
    }
}