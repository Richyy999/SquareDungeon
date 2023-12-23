using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.SubirStats
{
    /// <summary>
    /// Aumenta la probabilidad de crítico del ejecutor
    /// </summary>
    class Asesinato : AbstractSubirStats
    {
        public Asesinato() : base(PRIORIDAD_MAXIMA, NOMBRE_ASESINATO, DESC_ASESINATO, Categorias.SUBIR_STATS)
        { }

        public override bool EjecutarPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int habilidad = ejecutor.GetStatCombate(AbstractMob.INDICE_HABILIDAD);
            double extra = (AbstractMob.NIVEL_MAX - ejecutor.GetNivel()) / 10;
            double porcentaje = Util.GetPorcentaje(habilidad, 25f, extra);

            return Util.Probabilidad(porcentaje);
        }

        public override void RealizarAccionPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int probCrit = ejecutor.GetStatCombate(AbstractMob.INDICE_PROBABILIDAD_CRITICO);
            SubirStat(ejecutor, AbstractMob.INDICE_PROBABILIDAD_CRITICO, (int)(probCrit * 0.4));
        }
    }
}
