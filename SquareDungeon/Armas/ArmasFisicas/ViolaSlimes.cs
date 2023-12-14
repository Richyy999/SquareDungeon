using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;
using SquareDungeon.Habilidades.DanoAdicional.TipoEnemigo;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class ViolaSlimes : AbstractArmaFisica
    {
        private const int USOS_MAX = 200;
        private const int DANO = 7;

        public ViolaSlimes() :
            base(DANO, USOS_MAX, NOMBRE_VIOLA_SLIMES, DESC_VIOLA_SLIMES, new AntiSlime())
        { }

        public override int Atacar(AbstractMob mob, bool ejecutarHabilidad)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int fue = portador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStatCombate(AbstractMob.INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;

            bool danoAdicional = false;

            if (ejecutarHabilidad)
            {
                EjecutorHabilidades ejecutor = new EjecutorHabilidades(this.portador, mob, this.habilidad);
                int res = ejecutor.EjecutarAtaque();
                if (res != AbstractHabilidad.RESULTADO_SIN_ACTIVAR)
                    dano += res;

                danoAdicional = true;
            }

            if (mob is Slime && danoAdicional)
                dano *= 3;

            return dano;
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
