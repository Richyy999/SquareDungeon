using SquareDungeon.Modelo;
using SquareDungeon.Armas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.DobleGolpe
{
    class DosPorUno : AbstractHabilidad
    {

        public DosPorUno() : base(PRIORIDAD_MAXIMA, NOMBRE_DOS_POR_UNO, DESC_DOS_POR_UNO, Categorias.DOBLE_GOLPE)
        { }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int agilidad = ejecutor.GetStatCombate(AbstractMob.INDICE_AGILIDAD);
            double extra = (AbstractMob.NIVEL_MAX - ejecutor.GetNivel()) / 10;
            double porcentaje = Util.GetPorcentaje(agilidad, 25f, extra);

            return Util.Probabilidad(porcentaje);
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int dano = 0;
            if (ejecutor is AbstractJugador)
            {
                dano += Util.GetDano(ejecutor, victima);
                // TODO añadir mecánica de esquivar
                dano += Util.GetDano(ejecutor, victima);
            }
            else if (ejecutor is AbstractEnemigo)
            {
                dano += Util.GetDano(ejecutor, victima);
                // TODO añadir mecánica de esquivar
                dano += Util.GetDano(ejecutor, victima);
            }

            return dano;
        }
    }
}
