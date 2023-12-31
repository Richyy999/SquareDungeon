using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades
{
    /// <summary>
    /// Entidad situada en una sala
    /// </summary>
    abstract class AbstractEntidad
    {
        /// <summary>
        /// Nombre de la entidad
        /// </summary>
        private string nombre;
        /// <summary>
        /// Descripción de la entidad
        /// </summary>
        private string descripcion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="nombre">Nombre de la entidad</param>
        /// <param name="descripcion">Descripción de la entidad</param>
        protected AbstractEntidad(string nombre, string descripcion)
        {
            if (this is AbstractJugador)
                this.nombre = nombre;
            else
                this.nombre = GetPropiedad(FICHERO_NOMBRE_ENTIDADES, nombre);

            this.descripcion = GetPropiedad(FICHERO_DESC_ENTIDADES, descripcion);
        }

        /// <summary>
        /// Devuelve el <see cref="nombre"/> de la entidad
        /// </summary>
        /// <returns><see cref="nombre">Nombre</see> de la entidad</returns>
        public string GetNombre() => nombre;

        /// <summary>
        /// Devuelve el <see cref="descripcion">descripción</see> de la entidad
        /// </summary>
        /// <returns><see cref="descripcion">Descripción</see> de la entidad</returns>
        public string GetDescripcion() => descripcion;

        public override string ToString() => GetNombre();
    }
}
