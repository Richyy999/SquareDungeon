using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class Pocion : AbstractObjeto
    {
        public Pocion() : base(1, NOMBRE_POCION, DESC_POCION) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            base.RealizarAccion(jugador, enemigo, sala);
            jugador.SubirStat(AbstractMob.INDICE_VIDA, 20);
        }
    }
}
