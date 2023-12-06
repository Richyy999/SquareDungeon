using SquareDungeon.Salas;
using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.DanoAdicional.TipoEnemigo
{
    class AntiSlime : AbstractHabilidad
    {
        public AntiSlime() : base(30, PRIORIDAD_MEDIA, NOMBRE_ANTI_SLIME, DESC_ANTI_SLIME, Categorias.DANO_ADICIONAL_TIPO_ENEMIGO) { }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            return victima is Slime;
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, Sala sala)
        {
            int dano = Util.GetDano(ejecutor, victima);
            return dano * 3;
        }
    }
}
