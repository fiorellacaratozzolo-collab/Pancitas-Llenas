using System;

namespace Services.DomainModel.Composite
{
    /// <summary>
    /// Representa un permiso individual o acción específica del sistema. Funciona como el nodo "hoja" dentro del patrón Composite.
    /// </summary>
    public class Patente : Component
    {
        public string DataKey { get; set; }
        public TipoAcceso TipoAcceso { get; set; }

        /// <summary>
        /// Lanza una excepción, ya que una Patente es un nodo hoja y no puede contener otros componentes.
        /// </summary>
        public override void Add(Component c)
        {
            throw new InvalidOperationException("No se puede agregar elementos a una Patente.");
        }

        /// <summary>
        /// Lanza una excepción, ya que una Patente es un nodo hoja y no contiene elementos para quitar.
        /// </summary>
        public override void Remove(Component c)
        {
            throw new InvalidOperationException("No se puede quitar elementos de una Patente.");
        }

        /// <summary>
        /// Retorna siempre 0, dado que un permiso individual no contiene componentes hijos.
        /// </summary>
        public override int GetCount()
        {
            return 0;
        }
    }

    /// <summary>
    /// Define los niveles de autorización técnica que puede conceder una Patente.
    /// </summary>
    public enum TipoAcceso
    {
        Lectura = 1,
        Escritura = 2,
        ControlTotal = 3
    }
}
