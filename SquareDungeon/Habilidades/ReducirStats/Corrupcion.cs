using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.ReducirStats
{
    class Corrupcion : AbstractHabilidad
    {
        public Corrupcion() : base(10, PRIORIDAD_MAXIMA, NOMBRE_CORRUPCION, DESC_CORRUPCION, Categorias.REDUCIR_STATS) { }

        public override bool EjecutarPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            ejecutor.AlterarStatCombate(AbstractMob.INDICE_FUERZA, -5);
            return base.EjecutarPreAtaque(ejecutor, victima, sala);
        }
    }
}
