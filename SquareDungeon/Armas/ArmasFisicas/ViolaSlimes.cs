using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades.Ataque;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class ViolaSlimes : ArmaFisica
    {
        private const int USOS_MAX = 40;
        private const int DANO = 10;

        public ViolaSlimes() :
            base(DANO, USOS_MAX, NOMBRE_VIOLA_SLIMES, DESC_VIOLA_SLIMES, new AntiSlime())
        { }

        public override bool Atacar(Mob mob)
        {
            int fue = portador.GetStat(Mob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int dano = ata - mob.GetStat(Mob.INDICE_DEFENSA);
            int crit = 1 + portador.GetCritico();

            dano *= crit;
            if (habilidad.Ejecutar())
            {
                EntradaSalida.MostrarHabilidad(this, habilidad);
                dano *= 3;
            }

            if (mob.GetType() == typeof(Slime))
                dano *= 3;

            return mob.Danar(dano);
        }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "Los usos deben ser mayores a 0");

            if (this.usos + usos <= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
