using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.ReducirDano
{
    class Rencor : AbstractHabilidad
    {
        private int danoReducido;

        private bool reducido;

        public Rencor() : base(10, PRIORIDAD_MAXIMA, NOMBRE_RENCOR, DESC_RENCOR, Categorias.CONTRA_ATAQUE) { }

        public override void ResetearHabilidad()
        {
            base.ResetearHabilidad();
            init();
        }

        public override bool EjecutarAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return Util.Probabilidad(this.activacion);
        }

        public override int RealizarAccionAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala, int danoRecibido)
        {
            int dano = (int)(danoRecibido * 0.7);
            danoReducido = danoRecibido - dano;
            reducido = true;

            return dano;
        }

        public override bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            return reducido;
        }

        public override int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala)
        {
            int dano = Util.GetDano(ejecutor, victima);

            dano += danoReducido;

            init();

            return dano;
        }

        private void init()
        {
            danoReducido = 0;
            reducido = false;
        }
    }
}
