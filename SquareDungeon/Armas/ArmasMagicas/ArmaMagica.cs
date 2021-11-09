using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

using static SquareDungeon.Entidades.Mobs.Mob;

namespace SquareDungeon.Armas.ArmasMagicas
{
    abstract class ArmaMagica : Arma
    {
        protected ArmaMagica(int dano, int usos, string nombre, string descripcion, Habilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override bool Atacar(Mob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int mag = portador.GetStat(INDICE_MAGIA);
            int ata = mag + this.dano;

            int dano = ata - mob.GetStat(INDICE_RESISTENCIA);
            int crit = 1 + mob.GetCritico();

            dano *= crit;
            return mob.Danar(dano);
        }
    }
}
