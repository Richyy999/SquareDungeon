
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Habilidades
{
    class SinHabilidad : Habilidad
    {
        public static readonly SinHabilidad SIN_HABILIDAD = new SinHabilidad();

        private SinHabilidad() : base("Sin habilidad") { }

        public override void RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
        }

        public override Habilidad GetHabilidad() => SIN_HABILIDAD;
    }
}
