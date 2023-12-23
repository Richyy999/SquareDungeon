using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    /// <summary>
    /// Objeto que aumenta la magia del jugador en combate
    /// </summary>
    class PocionMagia : AbstractObjeto
    {
        public PocionMagia() : base(1, NOMBRE_POCION_MAGIA, DESC_POCION_MAGIA) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            base.RealizarAccion(jugador, enemigo, sala);
            int magCom = jugador.GetStatCombate(AbstractMob.INDICE_MAGIA);
            jugador.AlterarStatCombate(AbstractMob.INDICE_MAGIA, (int)(magCom * 0.2));
        }
    }
}
