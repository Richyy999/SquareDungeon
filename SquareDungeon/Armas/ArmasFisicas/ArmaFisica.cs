using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

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

            int fue = portador.GetStatCombate(INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;

            try
            {
                Jugador portador = (Jugador)this.portador;
                usos--;
                if (usos == SIN_USOS)
                    portador.EliminarArma(this);
            }
            catch (InvalidCastException)
            {

            }

            return mob.Danar(dano);
        }

        public override bool Atacar(Mob mob, int danoAdicional)
        {
            bool ataque = Atacar(mob);
            if (!ataque)
                return mob.Danar(danoAdicional);

            return ataque;
        }
    }
}
