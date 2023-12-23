using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.DobleGolpe
{
    /// <summary>
    /// El ejecutor de la habilidad ataca dos veces
    /// </summary>
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
            int danoTotal = 0;
            int dano = Util.GetDano(ejecutor, victima);
            if (victima.Esquivar(ejecutor))
                EntradaSalida.MostrarEsquivar(victima, ejecutor);
            else
                danoTotal += dano;

            dano = Util.GetDano(ejecutor, victima);
            if (victima.Esquivar(ejecutor))
                EntradaSalida.MostrarEsquivar(victima, ejecutor);
            else
                danoTotal += dano;

            return danoTotal;
        }
    }
}
