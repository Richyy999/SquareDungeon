using System;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Entidad contenida en las salas de cofre.
    /// <seealso cref="Salas.AbstractSala"/>
    /// </summary>
    abstract class AbstractCofre : AbstractEntidad
    {
        /// <summary>
        /// Contenido del cofre
        /// </summary>
        protected object contenido;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="nombre">Nombre del cofre</param>
        /// <param name="descripcion">Descripción del cofre</param>
        /// <param name="contenido">Contenido del cofre</param>
        protected AbstractCofre(string nombre, string descripcion, object contenido) : base(nombre, descripcion)
        {
            this.contenido = contenido;
        }

        /// <summary>
        /// Devuelve el contenido del cofre
        /// </summary>
        /// <returns>Contenido del cofre</returns>
        public abstract object AbrirCofre();
    }
}
