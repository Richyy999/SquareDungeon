using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// ofre que contiene un <see cref="AbstractObjeto">objeto</see>
    /// </summary>
    class CofreObjeto : AbstractCofre
    {
        public CofreObjeto(AbstractObjeto objeto) : base(NOMBRE_COFRE_OBJETO, DESC_COFRE_OBJETO, objeto) { }

        public override AbstractObjeto AbrirCofre() => (AbstractObjeto)contenido;
    }
}
