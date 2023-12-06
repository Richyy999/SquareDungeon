using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreHabilidad : Cofre
    {
        public CofreHabilidad(AbstractHabilidad habilidad) : base(NOMBRE_COFRE_HABILIDAD, DESC_COFRE_HABILIDAD, habilidad) { }

        public override AbstractHabilidad AbrirCofre() => (AbstractHabilidad)contenido;
    }
}
