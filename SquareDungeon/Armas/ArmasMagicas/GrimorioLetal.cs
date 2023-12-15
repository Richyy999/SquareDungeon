using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades.SubirStats;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class GrimorioLetal : AbstractArmaMagica
    {
        private const int USOS_MAX = 15;
        private const int DANO = 9;

        public GrimorioLetal() :
            base(DANO, USOS_MAX, NOMBRE_GRIMORIO_LETAL, DESC_GRIMORIO_LETAL, new Asesinato())
        { }

        public override int Atacar(AbstractMob mob)
        {
            EjecutorHabilidades ejecutor = new EjecutorHabilidades(this.portador, mob, this.habilidad);
            ejecutor.EjecutarPreCombate();

            return base.Atacar(mob);
        }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "Los usos deben ser mayores a 0");

            usos /= 5;
            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
