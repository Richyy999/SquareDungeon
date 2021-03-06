using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Habilidades.Ataque;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class ViolaSlimes : ArmaFisica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 7;

        public ViolaSlimes() :
            base(DANO, USOS_MAX, NOMBRE_VIOLA_SLIMES, DESC_VIOLA_SLIMES, new AntiSlime())
        { }

        public override bool Atacar(Mob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int fue = portador.GetStatCombate(Mob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(Mob.INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;
            if (habilidad.Ejecutar() && mob.GetType() == typeof(Slime))
            {
                EntradaSalida.MostrarHabilidad(this, habilidad);
                dano *= 3;
            }

            if (mob.GetType() == typeof(Slime))
                dano *= 3;

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
            if (usos <= 0)
                throw new ArgumentException("usos", "Los usos deben ser mayores a 0");

            usos = usos / 5;

            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
