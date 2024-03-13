using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Modelo;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Efectos
{
    /// <summary>
    /// Clase básica que define la función general de todos los efectos. No se deben instanciar clases que sean hijas directas de esta
    /// </summary>
    abstract class AbstractEfecto
    {
        /// <summary>
        /// Estado del efecto
        /// </summary>
        private bool activo;

        /// <summary>
        /// Nombre del efecto
        /// </summary>
        private string nombre;
        /// <summary>
        /// Descripción del efecto
        /// </summary>
        private string descripcion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        protected AbstractEfecto(string nombre, string descripcion)
        {
            this.nombre = GetPropiedad(FICHERO_NOMBRE_EFECTOS, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_EFECTOS, descripcion);

            Reiniciar();
        }

        /// <summary>
        /// Evalúa las condiciones para que se aplique o no el efecto
        /// </summary>
        /// <returns>True si se aplica el efecto, false en caso contrario</returns>
        public virtual bool EsAplicarEfecto()
        {
            return activo;
        }

        /// <summary>
        /// Aplica el efecto al <see cref="AbstractMob">mob</see>
        /// </summary>
        /// <param name="mob"><see cref="AbstractMob">Mob</see> al que se le aplica el efecto</param>
        public abstract void AplicarEfecto(AbstractMob mob);

        /// <summary>
        /// Muestra un mensaje para informar al usuario del efecto causado
        /// </summary>
        public void MostrarMensaje(AbstractMob mob)
        {
            EntradaSalida.MostrarMensaje(GetMensaje(mob));
        }

        /// <summary>
        /// Obtiene el nombre del efecto
        /// </summary>
        /// <returns>nombre del efecto</returns>
        public string GetNombre() => nombre;

        /// <summary>
        /// obtiene la descripción del efecto
        /// </summary>
        /// <returns>descripción del efecto</returns>
        public string GetDescripcion() => descripcion;

        /// <summary>
        /// Configura el estado inicial del efecto. Se debe usar este para inicializar cualquier efecto
        /// </summary>
        protected virtual void Reiniciar()
        {
            activo = true;
        }

        /// <summary>
        /// Devuelve el mensaje a mostrar en <see cref="MostrarMensaje"/>
        /// </summary>
        /// <returns>Mensaje a mostrar</returns>
        protected abstract string GetMensaje(AbstractMob mob);
    }
}
