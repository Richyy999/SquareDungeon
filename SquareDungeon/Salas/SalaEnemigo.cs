using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Salas
{
    class SalaEnemigo : Sala
    {
        private AbstractEnemigo enemigo;

        public SalaEnemigo(int x, int y, AbstractEnemigo enemigo) : base(x, y)
        {
            this.enemigo = enemigo;
        }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR)
            {
                Random random = new Random();
                int diferencia = random.Next(2);
                diferencia--;
                int nivelJugador = jugador.GetNivel() + diferencia;
                if (nivelJugador >= 0)
                    enemigo.SubirNivel(nivelJugador * AbstractMob.EXP_MAX);

                enemigo.CurarCompleto();

                SetEstado(ESTADO_SALA_ENEMIGO_ABIERTA);
            }

            if (GetEstado() == ESTADO_SALA_ENEMIGO_ABIERTA)
            {
                int res = partida.Combatir(jugador, enemigo, this);

                if (res == Partida.RESULTADO_HUIR)
                {
                    partida.SetResultado(Partida.RESULTADO_EN_JUEGO);
                    SetEstado(ESTADO_SALA_ENEMIGO_ABIERTA);
                    jugador.ReiniciarStatsCombate();
                }

                if (res == Partida.RESULTADO_JUGADOR_GANA)
                {
                    EntradaSalida.MostrarVictoria(jugador, enemigo);
                    jugador.ReiniciarStatsCombate();
                    SetEstado(ESTADO_VISITADO);
                }

                if (res == Partida.RESULTADO_ENEMIGO_GANA)
                {
                    partida.SetResultado(Partida.RESULTADO_ENEMIGO_GANA);
                }
            }
            if (GetEstado() == ESTADO_VISITADO)
                partida.SetPosicionJugador(x, y);
        }
    }
}
