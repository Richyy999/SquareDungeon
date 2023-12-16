using SquareDungeon.Salas;
using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class PocionFuerza : AbstractObjeto
    {
        public PocionFuerza() : base(1, NOMBRE_POCION_FUERZA, DESC_POCION_FUERZA) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala, Partida partida)
        {
            base.RealizarAccion(jugador, enemigo, sala, partida);
            int fueCom = jugador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            jugador.AlterarStatCombate(AbstractMob.INDICE_FUERZA, (int)(fueCom * 0.2));
        }
    }
}
