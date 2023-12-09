using System;

using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    internal class EspadaElfica : AbstractArmaFisica
    {
        private const int USOS_MAX = 50;
        private const int DANO = 5;
        public EspadaElfica() :
            base(DANO, USOS_MAX, NOMBRE_ESPADA_ELFICA, DESC_ESPADA_ELFICA, SIN_HABILIDAD)
        { }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "No se puede reparar un arma con usos menores a 1");

            usos /= 2;
            if (this.usos + usos <= USOS_MAX)
                this.usos += usos;
            else
                this.usos = USOS_MAX;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
