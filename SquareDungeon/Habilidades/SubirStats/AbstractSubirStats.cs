using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Habilidades.SubirStats
{
    class AbstractSubirStats : AbstractHabilidad
    {
        protected AbstractSubirStats(int activacion, int prioridad, string nombre, string descripcion, string categoria) :
            base(activacion, prioridad, nombre, descripcion, categoria)
        { }

        protected AbstractSubirStats( int prioridad, string nombre, string descripcion, string categoria) :
            base(prioridad, nombre, descripcion, categoria)
        { }

        protected virtual void SubirStat(AbstractMob receptor, int stat, int aumento)
        {
            receptor.AlterarStatCombate(stat, aumento);
        }
    }
}
