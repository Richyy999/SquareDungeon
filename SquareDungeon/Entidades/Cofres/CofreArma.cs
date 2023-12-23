using SquareDungeon.Armas;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Cofre que contiene un <see cref="AbstractArma">arma</see>
    /// </summary>
    class CofreArma : AbstractCofre
    {
        public CofreArma(AbstractArma arma) : base(NOMBRE_COFRE_ARMA, DESC_COFRE_ARMA, arma) { }

        public override AbstractArma AbrirCofre() => (AbstractArma)contenido;
    }
}
