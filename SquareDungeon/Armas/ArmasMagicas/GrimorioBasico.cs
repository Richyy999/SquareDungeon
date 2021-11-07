using System;

using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class GrimorioBasico : ArmaMagica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 5;

        public GrimorioBasico(Mob portador, string nombre, string descripcion) :
            base(DANO, USOS_MAX, nombre, descripcion, SIN_HABILIDAD, portador)
        { }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "No se puede reparar un arma con usos menores a 1");

            if (this.usos + usos <= USOS_MAX)
                this.usos += usos;
            else
                this.usos = USOS_MAX;
        }
    }
}
