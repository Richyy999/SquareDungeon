using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    class SinHabilidad : AbstractHabilidad
    {
        public static readonly SinHabilidad SIN_HABILIDAD = new SinHabilidad();

        private SinHabilidad() :
            base(100, PRIORIDAD_MINIMA, NOMBRE_SIN_HABILIDAD, DESC_SIN_HABILIDAD, Categorias.SIN_HABILIDAD)
        { }
    }
}
