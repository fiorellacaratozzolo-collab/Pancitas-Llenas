namespace Services.Dal.Interfaces
{
    /// <summary>
    /// Define el contrato para los adaptadores encargados de mapear arreglos de datos provenientes de la base de datos a objetos de dominio fuertemente tipados.
    /// </summary>
    internal interface IAdapter<T>
    {
        /// <summary>
        /// Construye y devuelve una instancia del tipo genérico a partir de un arreglo de valores.
        /// </summary>
        T Get(object[] values);
    }
}