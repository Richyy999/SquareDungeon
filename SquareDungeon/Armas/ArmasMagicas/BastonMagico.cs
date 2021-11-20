using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Habilidades.Armas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasMagicas
{
    class BastonMagico : ArmaMagica
    {
        private const int USOS_MAX = 20;
        private const int DANO = 5;

        public BastonMagico() :
            base(DANO, USOS_MAX, NOMBRE_BASTON_MAGICO, DESC_BASTON_MAGICO, new Sanacion())
        { }

        public override bool Atacar(Mob mob)
        {
            try
            {
                Jugador jugador = (Jugador)portador;
                if (habilidad.Ejecutar())
                {
                    int res = habilidad.RealizarAccion(jugador, (Enemigo)mob);
                    if (res == Habilidad.RESULTADO_ACTIVADA)
                        EntradaSalida.MostrarHabilidad(jugador, habilidad);
                }
            }
            catch (InvalidCastException)
            {

            }

            return base.Atacar(mob);
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
            usos /= 4;
            if (this.usos + usos >= USOS_MAX)
                this.usos = USOS_MAX;
            else
                this.usos += usos;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
