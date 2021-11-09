using SquareDungeon.Armas;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreArma : Cofre
    {
        public CofreArma(Arma arma) : base(NOMBRE_COFRE_ARMA, DESC_COFRE_ARMA, arma) { }

        public override Arma AbrirCofre() => (Arma)contenido;
    }
}
