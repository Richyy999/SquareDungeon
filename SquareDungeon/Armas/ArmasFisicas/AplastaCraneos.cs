using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class AplastaCraneos : AbstractArmaFisica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 8;

        public AplastaCraneos() : base(DANO, USOS_MAX, NOMBRE_APLASTA_CRANEOS, DESC_APLASTA_CRANEOS, SIN_HABILIDAD)
        { }

        public override int Atacar(AbstractMob mob)
        {
            int fue = portador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(AbstractMob.INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;

            if (mob.GetType() == typeof(Esqueleto))
                dano = (int)(dano * 2.5);

            if (mob is AbstractJugador)
            {
                AbstractJugador portador = (AbstractJugador)this.portador;
                usos--;
                if (usos == SIN_USOS)
                    portador.EliminarArma(this);
            }

            return dano;
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
