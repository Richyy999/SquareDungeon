using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Cofre que contiene una <see cref="AbstractHabilidad">habilidad</see>
    /// </summary>
    class CofreHabilidad : AbstractCofre
    {
        public CofreHabilidad(AbstractHabilidad habilidad) : base(NOMBRE_COFRE_HABILIDAD, DESC_COFRE_HABILIDAD, habilidad) { }

        public override AbstractHabilidad AbrirCofre() => (AbstractHabilidad)contenido;
    }
}
