using System;

using SquareDungeon.Modelo;
using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

namespace SquareDungeon.Salas
{
    class SalaJefe : SalaEnemigo
    {
        private AbstractJefe jefe;

        private bool subirNivel;
        private bool abierta;

        public SalaJefe(int x, int y, AbstractJefe jefe) : base(x, y, jefe)
        {
            this.jefe = jefe;
            subirNivel = true;
            abierta = false;
        }

        protected override void combatir(Partida partida, AbstractJugador jugador)
        {
            int res = partida.Combatir(jugador, jefe, this);
            if (res == Partida.RESULTADO_JUGADOR_GANA)
            {
                EntradaSalida.MostrarVictoria(jugador, jefe);
                partida.SetResultado(Partida.RESULTADO_JEFE_ELIMINADO);
            }

            if (res == Partida.RESULTADO_ENEMIGO_GANA)
                partida.SetResultado(Partida.RESULTADO_ENEMIGO_GANA);
        }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (subirNivel)
            {
                subirNivelEnemigo(partida, jugador);
                subirNivel = false;
            }
            combatir(partida, jugador);
        }

        public override bool AbrirSala(AbstractJugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR || GetEstado() == ESTADO_SALA_JEFE_SIN_ABRIR)
            {
                EntradaSalida.DescubrirJefe();
                LlaveJefe llave = EntradaSalida.GetLlaveJefe(jugador.GetObjetos());
                SetEstado(ESTADO_SALA_JEFE_SIN_ABRIR);
                if (llave == null)
                    return false;

                llave.RealizarAccion(jugador, null, this, null);
                SetEstado(ESTADO_SALA_JEFE_ABIERTA);
                abierta = true;
                return abierta;
            }

            return abierta;
        }
    }
}
