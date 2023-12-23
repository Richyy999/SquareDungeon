using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Habilidades.SubirStats
{
    /// <summary>
    /// Clase base de las habilidades que suban los stats del ejecutor. Todas las habilidades que suban los stats del ejecutor deben heredar de esta clase
    /// </summary>
    class AbstractSubirStats : AbstractHabilidad
    {
        protected AbstractSubirStats(int activacion, int prioridad, string nombre, string descripcion, string categoria) :
            base(activacion, prioridad, nombre, descripcion, categoria)
        { }

        protected AbstractSubirStats(int prioridad, string nombre, string descripcion, string categoria) :
            base(prioridad, nombre, descripcion, categoria)
        { }

        protected virtual void SubirStat(AbstractMob receptor, int stat, int aumento)
        {
            if (aumento > 0)
                receptor.AlterarStatCombate(stat, aumento);
        }
    }
}
