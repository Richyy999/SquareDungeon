using System;

using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Habilidades.SinHabilidad;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    /// <summary>
    /// Espada que ataca al enemigo en función de la magia del <see cref="AbstractArma.portador">portador</see> y de la defensa física del enemigo
    /// </summary>
    class EspadaMagica : AbstractArmaMagica
    {
        private const int USOS_MAX = 25;

        private const int DANO = 7;

        public EspadaMagica() : base(DANO, USOS_MAX, NOMBRE_ESPADA_MAGICA, DESC_ESPADA_MAGICA, SIN_HABILIDAD)
        { }

        public override int GetDanoBase(AbstractMob mob)
        {
            int mag = portador.GetStatCombate(AbstractMob.INDICE_MAGIA);
            int ata = mag + this.dano;
            int defEnemiga = mob.GetStatCombate(AbstractMob.INDICE_DEFENSA);

            return ata - defEnemiga;
        }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "Los usos deben ser mayores a 0");

            usos /= 3;

            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
