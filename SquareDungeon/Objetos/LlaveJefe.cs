using SquareDungeon.Salas;
using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class LlaveJefe : AbstractObjeto
    {
        public LlaveJefe() : base(1, NOMBRE_LLAVE_JEFE, DESC_LLAVE_JEFE) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala, Partida partida)
        {
            if (sala is SalaJefe)
                jugador.EliminarObjeto(this);
        }
    }
}
