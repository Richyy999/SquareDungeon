using System;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class GrimorioBasico : AbstractArmaMagica
    {
        private const int USOS_MAX = 50;
        private const int DANO = 5;

        public GrimorioBasico() :
            base(DANO, USOS_MAX, NOMBRE_GRIMORIO_BASICO, DESC_GRIMORIO_BASICO, SIN_HABILIDAD)
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
