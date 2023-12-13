using System;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Especiales
{
    internal class Esquivar : AbstractHabilidad
    {
        public Esquivar() : base(50, PRIORIDAD_MAXIMA, NOMBRE_ESQUIVAR, DESC_ESQUIVAR, Categorias.ESQUIVAR)
        { }

        public override bool EjecutarPreCombate(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            ejecutor.AlterarStatCombate(AbstractMob.INDICE_AGILIDAD, 10);
            ejecutor.AlterarStatCombate(AbstractMob.INDICE_HABILIDAD, 10);
            return base.EjecutarPreCombate(ejecutor, victima, sala);
        }
    }
}
