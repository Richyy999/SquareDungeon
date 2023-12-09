using System;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class EspadaMaldita : AbstractArmaFisica
    {
        private const int USOS_MAX = 25;
        private const int DANO = 8;

        public EspadaMaldita() : base(DANO, USOS_MAX, NOMBRE_ESPADA_MALDITA, DESC_ESPADA_MALDITA, SIN_HABILIDAD)
        { }

        public override int Atacar(AbstractMob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            double fue = portador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int ata = (int)fue + this.dano;

            int dano = ata - mob.GetStatCombate(AbstractMob.INDICE_RESISTENCIA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;

            int disminucionFue = (int)((fue *= 0.03) * -1);

            portador.AlterarStatCombate(AbstractMob.INDICE_FUERZA, disminucionFue);

            GastarArma();

            return dano;
        }

        public override void RepararArma(int usos) { }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
