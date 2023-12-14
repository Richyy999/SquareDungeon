using System;

using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    abstract class AbstractObjeto
    {
        public const int CANTIDAD_MAX = 32;

        protected int cantidad;

        protected string nombre;
        protected string descripcion;

        protected AbstractObjeto(int cantidad, string nombre, string descripcion)
        {
            this.cantidad = cantidad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_OBJETOS, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_OBJETOS, descripcion);
        }

        public virtual void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            cantidad--;
            if (cantidad == 0)
                jugador.EliminarObjeto(this);
        }

        public void AnadirCantidad(int cantidad)
        {
            if (cantidad < 1)
                throw new ArgumentOutOfRangeException("cantidad", "No se puede añadir una cantidad menor a 1");

            if (this.cantidad + cantidad <= CANTIDAD_MAX)
                this.cantidad += cantidad;
            else
                this.cantidad = CANTIDAD_MAX;
        }

        public int GetCantidad() => cantidad;

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;
    }
}
