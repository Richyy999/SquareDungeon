using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Salas;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.ReducirStats
{
    class Corrupcion : AbstractHabilidad
    {
        private bool ejecutado;

        public Corrupcion() : base(10, PRIORIDAD_MAXIMA, NOMBRE_CORRUPCION, DESC_CORRUPCION, Categorias.REDUCIR_STATS) { }

        public override bool EjecutarPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return !ejecutado;
        }

        public override void RealizarAccionPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int fue = ejecutor.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int fueReducida = (int)(fue * 0.3);
            if (fueReducida == 0)
                fueReducida = 1;

            fueReducida *= -1;
            ejecutor.AlterarStatCombate(AbstractMob.INDICE_FUERZA, fueReducida);

            ejecutado = true;
        }

        public override void ResetearHabilidad()
        {
            base.ResetearHabilidad();
            ejecutado = false;
        }
    }
}
