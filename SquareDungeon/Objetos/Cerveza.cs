using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class Cerveza : AbstractObjeto
    {
        public Cerveza() : base(1, NOMBRE_CERVEZA, DESC_CERVEZA) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, Sala sala)
        {
            base.RealizarAccion(jugador, enemigo, sala);
            jugador.SubirStat(AbstractMob.INDICE_VIDA, 45);
            jugador.AlterarStatCombate(AbstractMob.INDICE_HABILIDAD, -3);
            jugador.AlterarStatCombate(AbstractMob.INDICE_AGILIDAD, -3);
        }
    }
}