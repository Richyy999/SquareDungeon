using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Modelo;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Efectos
{
    class Veneno : AbstractEfecto
    {

        private const int DANO_BASE = 5;

        private int dano;

        private AbstractMob mobDañado;

        public Veneno() : base(NOMBRE_VENENO, DESC_VENENO) { }

        protected override void Reiniciar()
        {
            base.Reiniciar();
            dano = DANO_BASE;
        }

        protected override string GetMensaje(AbstractMob mob)
        {
            return $"El veneno ha infligido {dano} puntos de daño a {mob.GetNombre()}";
        }

        public override void AplicarEfecto(AbstractMob mob)
        {
            mob.DanarSinMatar(dano);
            dano += DANO_BASE * (mob.GetNivel() / 2);
        }
    }
}
