using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    /// <summary>
    /// Objeto que aumenta la probabilidad de crítico del jugador en combate
    /// </summary>
    class PocionAsesina : AbstractObjeto
    {
        public PocionAsesina() : base(1, NOMBRE_POCION_ASESINA, DESC_POCION_ASESINA) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            base.RealizarAccion(jugador, enemigo, sala);
            int probCritCom = jugador.GetStatCombate(AbstractMob.INDICE_PROBABILIDAD_CRITICO);
            jugador.AlterarStatCombate(AbstractMob.INDICE_PROBABILIDAD_CRITICO, (int)(probCritCom * 0.15));
        }
    }
}
