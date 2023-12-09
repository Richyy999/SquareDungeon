using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Entidades.Mobs.AbstractMob;

namespace SquareDungeon.Armas.ArmasFisicas
{
    abstract class AbstractArmaFisica : AbstractArma
    {

        protected AbstractArmaFisica(int dano, int usos, string nombre, string descripcion, AbstractHabilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override int Atacar(AbstractMob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int fue = this.portador.GetStatCombate(INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(INDICE_DEFENSA);
            int crit = 1 + this.portador.GetCritico();

            dano *= crit;

            GastarArma();

            return dano;
        }

        public override int Atacar(AbstractMob mob, int danoAdicional)
        {
            int ataque = Atacar(mob);
            if (ataque < mob.GetStat(AbstractMob.INDICE_VIDA))
                return danoAdicional;

            return ataque;
        }
    }
}
