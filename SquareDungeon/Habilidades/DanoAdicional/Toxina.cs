using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.DanoAdicional
{
    /// <summary>
    /// Envenena al emnemigo y le causa daño en cada turno
    /// </summary>
    class Toxina : AbstractHabilidad
    {

        private const int DANO_BASE = 5;

        private bool aplicarToxina;

        private int dano;

        public Toxina() : base(5, PRIORIDAD_MEDIA, NOMBRE_TOXINA, DESC_TOXINA, Categorias.DANO_ADICIONAL)
        { }

        public override bool EjecutarPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return aplicarToxina;
        }

        public override void RealizarAccionPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            victima.DanarSinMatar(dano);
            dano += DANO_BASE * (victima.GetNivel() / 2);

            EntradaSalida.MostrarDano(ejecutor, victima, dano);
        }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return Util.Probabilidad(activacion) && !aplicarToxina;
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            aplicarToxina = true;
            EntradaSalida.MostrarMensaje($"{ejecutor.GetNombre()} envenenó a {victima.GetNombre()}");
            return Util.GetDano(ejecutor, victima);
        }

        public override void ResetearHabilidad()
        {
            base.ResetearHabilidad();
            aplicarToxina = false;
            dano = DANO_BASE;
        }
    }
}
