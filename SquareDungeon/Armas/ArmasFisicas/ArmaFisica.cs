using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

using static SquareDungeon.Entidades.Mobs.Mob;

namespace SquareDungeon.Armas.ArmasFisicas
{
    abstract class ArmaFisica : Arma
    {

        protected ArmaFisica(int dano, int usos, string nombre, Habilidad habilidad, Mob portador) :
            base(dano, usos, nombre, habilidad, portador)
        { }

        public override bool Danar(Mob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int fue = portador.GetStat(INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStat(INDICE_DEFENSA);

            return mob.Danar(dano);
        }
    }
}
