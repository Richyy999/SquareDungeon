using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Habilidades.Cura
{
    abstract class AbstractSanacion : AbstractHabilidad
    {
        protected AbstractSanacion(int activacion, int prioridad, string nombre, string descripcion, string categoria)
            : base(activacion, prioridad, nombre, descripcion, categoria) { }

        protected virtual void Sanar(AbstractMob receptor, int curacion)
        {
            receptor.SubirStat(AbstractMob.INDICE_VIDA, curacion);
        }
    }
}
