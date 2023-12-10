using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Habilidades.SinHabilidad;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class EspadaMagica : AbstractArmaMagica
    {
        private const int USOS_MAX = 25;

        private const int DANO = 7;

        public EspadaMagica() : base(DANO, USOS_MAX, NOMBRE_ESPADA_MAGICA, DESC_ESPADA_MAGICA, SIN_HABILIDAD)
        { }

        public override int Atacar(AbstractMob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int mag = portador.GetStatCombate(AbstractMob.INDICE_MAGIA);
            int ata = mag + this.dano;

            int dano = ata - mob.GetStatCombate(AbstractMob.INDICE_DEFENSA);
            int crit = 1 + mob.GetCritico();

            dano *= crit;

            GastarArma();

            return dano;
        }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "Los usos deben ser mayores a 0");

            usos /= 3;

            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
