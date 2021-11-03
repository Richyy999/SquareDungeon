using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

using static SquareDungeon.Entidades.Mobs.Mob;

namespace SquareDungeon.Armas.ArmasMagicas
{
    abstract class ArmaMagica : Arma
    {
        protected ArmaMagica(int dano, int usos, string nombre, Habilidad habilidad, Mob portador) :
            base(dano, usos, nombre, habilidad, portador)
        { }

        public override bool Danar(Mob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int mag = portador.GetStat(INDICE_MAGIA);
            int ata = mag + this.dano;

            int dano = ata - mob.GetStat(INDICE_RESISTENCIA);

            return mob.Danar(dano);
        }
    }
}
