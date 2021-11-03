using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Habilidades
{
    abstract class Habilidad
    {
        protected string nombre;

        protected Habilidad(string nombre)
        {
            this.nombre = nombre;
        }

        public abstract void RealizarAccion(Jugador jugador, Enemigo enemigo);

        public abstract Habilidad GetHabilidad();
    }
}
