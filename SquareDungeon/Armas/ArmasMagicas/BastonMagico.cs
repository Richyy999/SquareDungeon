using SquareDungeon.Modelo;
using SquareDungeon.Habilidades.Cura;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class BastonMagico : AbstractArmaMagica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 5;

        public BastonMagico() :
            base(DANO, USOS_MAX, NOMBRE_BASTON_MAGICO, DESC_BASTON_MAGICO, new Sanacion())
        { }

        public override int Atacar(AbstractMob mob)
        {

            EjecutorHabilidades ejecutor = new EjecutorHabilidades(this.portador, mob, this.habilidad);
            ejecutor.EjecutarAtaque();
            return base.Atacar(mob);
        }

        public override void RepararArma(int usos)
        {
            usos /= 4;
            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
