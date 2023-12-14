using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Cura
{
    class GolpeSanador : AbstractSanacion
    {
        public GolpeSanador() : base(15, PRIORIDAD_MEDIA, NOMBRE_GOLPE_SANADOR, DESC_GOLPE_SANADOR, Categorias.CURAR)
        {
        }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return Util.Probabilidad(this.activacion);
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int dano = Util.GetDano(ejecutor, victima);
            Sanar(ejecutor, dano / 2);

            return dano;
        }
    }
}
