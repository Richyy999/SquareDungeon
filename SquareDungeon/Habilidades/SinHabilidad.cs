using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    class SinHabilidad : Habilidad
    {
        public static readonly SinHabilidad SIN_HABILIDAD = new SinHabilidad();

        private SinHabilidad() :
            base(100, PRIORIDAD_MINIMA, TIPO_PRE_COMBATE, NOMBRE_SIN_HABILIDAD, DESC_SIN_HABILIDAD)
        { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo) => RESULTADO_SIN_ACTIVAR;
    }
}
