using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Ataque
{
    class BanoMagia : Habilidad
    {
        public BanoMagia() : base(40, PRIORIDAD_MEDIA, TIPO_ATAQUE, NOMBRE_BANO_MAGIA, DESC_BANO_MAGIA)
        { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            Arma arma = jugador.GetArmaCombate();
            int danoAdicional = jugador.GetStatCombate(Mob.INDICE_MAGIA);
            if (arma.Atacar(enemigo, danoAdicional))
                return RESULTADO_MOB_DERROTADO;

            return RESULTADO_ACTIVADA;
        }
    }
}
