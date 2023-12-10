using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Armas.ArmasMagicas
{
    abstract class AbstractArmaMagica : AbstractArma
    {
        protected AbstractArmaMagica(int dano, int usos, string nombre, string descripcion, AbstractHabilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override int Atacar(AbstractMob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int mag = portador.GetStatCombate(AbstractMob.INDICE_MAGIA);
            int ata = mag + this.dano;

            int dano = ata - mob.GetStatCombate(AbstractMob.INDICE_RESISTENCIA);
            int crit = 1 + mob.GetCritico();

            dano *= crit;

            GastarArma();

            return dano;
        }

        public override int Atacar(AbstractMob mob, int danoAdicional)
        {
            int dano = Atacar(mob);

            return dano + danoAdicional;
        }
    }
}
