using System;

using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Habilidades.SinHabilidad;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class DagaHierro : AbstractArmaFisica
    {
        private const int USOS_MAX = 50;
        private const int DANO = 3;
        public DagaHierro() :
            base(DANO, USOS_MAX, NOMBRE_DAGA_HIERRO, DESC_DAGA_HIERRO, SIN_HABILIDAD)
        { }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "No se puede reparar un arma con usos menores a 1");

            usos = usos / 2;
            if (this.usos + usos <= USOS_MAX)
                this.usos += usos;
            else
                this.usos = USOS_MAX;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
