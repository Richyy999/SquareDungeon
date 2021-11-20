
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class PocionFuerza : Objeto
    {
        public PocionFuerza() : base(1, NOMBRE_POCION_FUERZA, DESC_POCION_FUERZA) { }

        public override void RealizarAccion(Jugador jugador, Enemigo enemigo, Sala sala)
        {
            base.RealizarAccion(jugador, enemigo, sala);
            int fueCom = jugador.GetStatCombate(Mob.INDICE_FUERZA);
            jugador.AlterarStatCombate(Mob.INDICE_FUERZA, (int)(fueCom * 0.2));
        }
    }
}
