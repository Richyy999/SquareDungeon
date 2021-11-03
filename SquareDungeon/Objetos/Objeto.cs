using System;

using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Objetos
{
    abstract class Objeto
    {
        public const int CANTIDAD_MAX = 32;

        protected int cantidad;

        protected string nombre;

        protected Objeto(int cantidad, string nombre)
        {
            this.cantidad = cantidad;
            this.nombre = nombre;
        }

        public abstract void RealizarAccion(Jugador jugador, Enemigo enemigo, Sala sala);

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
    }
}
