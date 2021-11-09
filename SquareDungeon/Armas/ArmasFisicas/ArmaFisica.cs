using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

using static SquareDungeon.Entidades.Mobs.Mob;

namespace SquareDungeon.Armas.ArmasFisicas
{
    abstract class ArmaFisica : Arma
    {

        protected ArmaFisica(int dano, int usos, string nombre, string descripcion, Habilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override bool Atacar(Mob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int fue = portador.GetStat(INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStat(INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            usos--;

            dano *= crit;
            return mob.Danar(dano);
        }
    }
}
