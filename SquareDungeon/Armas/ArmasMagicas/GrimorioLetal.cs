using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Habilidades.PreCombate;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class GrimorioLetal : ArmaMagica
    {
        private const int USOS_MAX = 15;
        private const int DANO = 9;

        public GrimorioLetal() :
            base(DANO, USOS_MAX, NOMBRE_GRIMORIO_LETAL, DESC_GRIMORIO_LETAL, new Asesinato())
        { }

        public override bool Atacar(Mob mob)
        {
            int mag = portador.GetStatCombate(Mob.INDICE_MAGIA);
            int ata = mag + this.dano;
            int dano = ata - mob.GetStatCombate(Mob.INDICE_RESISTENCIA);

            if (habilidad.Ejecutar())
            {
                EntradaSalida.MostrarHabilidad(this, habilidad);
                try
                {
                    Jugador jugador = (Jugador)portador;
                    Enemigo enemigo = (Enemigo)mob;
                    habilidad.RealizarAccion(jugador, enemigo);
                }
                catch (InvalidCastException)
                {

                }
            }

            int crit = 1 + portador.GetCritico();
            dano *= crit;

            try
            {
                Jugador portador = (Jugador)this.portador;
                usos--;
                if (usos == SIN_USOS)
                    portador.EliminarArma(this);
            }
            catch (InvalidCastException)
            {

            }

            return mob.Danar(dano);
        }

        public override bool Atacar(Mob mob, int danoAdicional)
        {
            bool ataque = Atacar(mob);
            if (!ataque)
                return mob.Danar(danoAdicional);

            return ataque;
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
