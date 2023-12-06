using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

using static SquareDungeon.Resources.Resource;
using SquareDungeon.Salas;

namespace SquareDungeon.Habilidades.DanoAdicional
{
    class HabilidadTrol : AbstractHabilidad
    {
        public HabilidadTrol() : base(15, PRIORIDAD_MAXIMA, NOMBRE_HABILIDAD_TROL, DESC_HABILIDAD_TROL, Categorias.DANO_ADICIONAL) { }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            return ejecutor is Trol && Util.Probabilidad(this.activacion);
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            int dano = Util.GetDano(ejecutor, victima);

            return dano * 2;
        }
    }
}
