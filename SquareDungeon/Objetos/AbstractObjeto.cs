using System;

using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    /// <summary>
    /// Clase básica que define las acciones que realizan los objetos. Todos los objetos deben heredar de esta clase
    /// </summary>
    abstract class AbstractObjeto
    {
        /// <summary>
        /// Cantidad máxima de veces que se puede tener un objeto
        /// </summary>
        public const int CANTIDAD_MAX = 32;

        /// <summary>
        /// Número de veces que se tiene el objeto
        /// </summary>
        protected int cantidad;

        /// <summary>
        /// Nombre del objeto
        /// </summary>
        protected string nombre;
        /// <summary>
        /// Descripciónn del objeto
        /// </summary>
        protected string descripcion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="cantidad">Número de veces que se tiene el objeto</param>
        /// <param name="nombre">Nombre del objeto</param>
        /// <param name="descripcion">Descrìpciónn del objeto</param>
        protected AbstractObjeto(int cantidad, string nombre, string descripcion)
        {
            this.cantidad = cantidad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_OBJETOS, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_OBJETOS, descripcion);
        }

        /// <summary>
        /// Realiza la función del objeto y gasta un uso. Si se queda sin usos se elimina del inventario del jugador.<br/>
        /// Si se sobreescrive este método se deberá llamar al padre en la última instrucción del método
        /// </summary>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> portador del objeto</param>
        /// <param name="enemigo"><see cref="AbstractEnemigo">Enemigo</see> sobre el que se realiza la acción</param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la qiue se encuentra el <paramref name="jugador"/></param>
        public virtual void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            cantidad--;
            if (cantidad == 0)
                jugador.EliminarObjeto(this);
        }

        /// <summary>
        /// Aumenta la cantidad del objeto sin sobrepasar la <see cref="CANTIDAD_MAX">cantidad máxima</see>
        /// </summary>
        /// <param name="cantidad">Cantidad a aumentar</param>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si la cantidad es menor que 1</exception>
        public void AnadirCantidad(int cantidad)
        {
            if (cantidad < 1)
                throw new ArgumentOutOfRangeException("cantidad", "No se puede añadir una cantidad menor a 1");

            if (this.cantidad + cantidad <= CANTIDAD_MAX)
                this.cantidad += cantidad;
            else
                this.cantidad = CANTIDAD_MAX;
        }

        /// <summary>
        /// Devuelve la cantidad del objeto
        /// </summary>
        /// <returns>Cantidad del objeto</returns>
        public int GetCantidad() => cantidad;

        /// <summary>
        /// Devuelve el nombre del objeto
        /// </summary>
        /// <returns>Nombre del objeto</returns>
        public string GetNombre() => nombre;

        /// <summary>
        /// Devuelve la descripción del personaje
        /// </summary>
        /// <returns>Descripción del personaje</returns>
        public string GetDescripcion() => descripcion;
    }
}
