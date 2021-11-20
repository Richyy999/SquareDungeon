using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Armas
{
    class Sanacion : Habilidad
    {
        public Sanacion() : base(50, PRIORIDAD_MAXIMA, TIPO_POST_COMBATE, NOMBRE_SANACION, DESC_SANACION)
        { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            int vidaMax = jugador.GetStat(Mob.INDICE_VIDA_TOTAL);
            int curacion = (int)(vidaMax * 0.3);
            jugador.SubirStat(Mob.INDICE_VIDA, curacion);

            return RESULTADO_ACTIVADA;
        }
    }
}
