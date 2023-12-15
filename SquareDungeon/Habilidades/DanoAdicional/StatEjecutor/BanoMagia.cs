using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.DanoAdicional.StatEjecutor
{
    class BanoMagia : AbstractHabilidad
    {
        public BanoMagia() : base(40, PRIORIDAD_MEDIA, NOMBRE_BANO_MAGIA, DESC_BANO_MAGIA, Categorias.DANO_ADICOINAL_STAT_EJECUTOR)
        { }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return Util.Probabilidad(this.activacion) && ejecutor is AbstractJugador;
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int dano = Util.GetDano(ejecutor, victima);
            return dano + ejecutor.GetStatCombate(AbstractMob.INDICE_MAGIA);
        }
    }
}
