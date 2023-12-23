using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    /// <summary>
    /// Arma que duplica el daño si el enemigo es un <see cref="Esqueleto"/>
    /// </summary>
    class AplastaCraneos : AbstractArmaFisica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 8;

        public AplastaCraneos() : base(DANO, USOS_MAX, NOMBRE_APLASTA_CRANEOS, DESC_APLASTA_CRANEOS, SIN_HABILIDAD)
        { }

        public override int Atacar(AbstractMob mob)
        {
            int dano = base.Atacar(mob);

            if (mob is Esqueleto)
                dano = (int)(dano * 2.5);

            return dano;
        }

        public override void RepararArma(int usos)
        {
            usos /= 3;
            if (this.usos + usos < USOS_MAX)
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
