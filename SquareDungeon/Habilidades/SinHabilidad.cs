using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    /// <summary>
    /// Habilidad vacía. Debe usarse en el caso de que solicite una habilidad y no se desea que posea alguna.<br/>
    /// No se debe instanciar esta clase, se debe usar la variable <see cref="SIN_HABILIDAD"/>
    /// </summary>
    class SinHabilidad : AbstractHabilidad
    {
        /// <summary>
        /// Devuelve una instancia de esta clase
        /// </summary>
        public static readonly SinHabilidad SIN_HABILIDAD = new SinHabilidad();

        private SinHabilidad() :
            base(0, PRIORIDAD_MINIMA, NOMBRE_SIN_HABILIDAD, DESC_SIN_HABILIDAD, Categorias.SIN_HABILIDAD)
        { }
    }
}
