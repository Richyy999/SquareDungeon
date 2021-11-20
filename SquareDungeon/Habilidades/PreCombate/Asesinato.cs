using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.PreCombate
{
    class Asesinato : Habilidad
    {
        public Asesinato() : base(10, PRIORIDAD_MAXIMA, TIPO_PRE_COMBATE, NOMBRE_ASESINATO, DESC_ASESINATO)
        { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            int probCritCom = jugador.GetStatCombate(Mob.INDICE_PROBABILIDAD_CRITICO);
            jugador.AlterarStatCombate(Mob.INDICE_PROBABILIDAD_CRITICO, (int)(probCritCom * 0.4));

            return RESULTADO_ACTIVADA;
        }
    }
}
