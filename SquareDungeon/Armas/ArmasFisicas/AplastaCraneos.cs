using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class AplastaCraneos : ArmaFisica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 8;

        public AplastaCraneos() : base(DANO, USOS_MAX, NOMBRE_APLASTA_CRANEOS, DESC_APLASTA_CRANEOS, SIN_HABILIDAD)
        { }

        public override bool Atacar(Mob mob)
        {
            int fue = portador.GetStatCombate(Mob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(Mob.INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;

            if (mob.GetType() == typeof(Esqueleto))
                dano = (int)(dano * 2.5);

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

        public override void RepararArma(int usos)
        {
            usos /= 3;
            if (this.usos + usos < USOS_MAX)
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
