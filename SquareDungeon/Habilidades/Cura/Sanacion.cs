using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Cura
{
    class Sanacion : AbstractSanacion
    {
        public Sanacion() : base(50, PRIORIDAD_MAXIMA, NOMBRE_SANACION, DESC_SANACION, Categorias.CURAR)
        { }

        public override bool EjecutarPostCombate(AbstractMob ejecutor, AbstractMob victima, Sala sala) => true;

        public override void RealizarAccionPostCombate(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            int vidaMax = ejecutor.GetStat(AbstractMob.INDICE_VIDA_TOTAL);
            int curacion = (int)(vidaMax * 0.3);
            Sanar(ejecutor, curacion);
        }
    }
}
