using System;

using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Entidades.Cofres
{
    abstract class AbstractCofre : AbstractEntidad
    {
        protected Object contenido;

        protected AbstractCofre(string nombre, string descripcion, Object contenido) : base(nombre, descripcion)
        {
            this.contenido = contenido;
        }

        public abstract bool AbrirCofre(AbstractJugador jugador, AbstractSala sala, Partida partida);

        protected abstract Object getContenido();
    }
}
