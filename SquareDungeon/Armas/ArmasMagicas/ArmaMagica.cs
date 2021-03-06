using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Habilidades;

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

            int mag = portador.GetStatCombate(Mob.INDICE_MAGIA);
            int ata = mag + this.dano;

            int dano = ata - mob.GetStatCombate(Mob.INDICE_RESISTENCIA);
            int crit = 1 + mob.GetCritico();

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
