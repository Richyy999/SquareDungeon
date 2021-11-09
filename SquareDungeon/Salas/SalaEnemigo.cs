using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Salas
{
    class SalaEnemigo : Sala
    {
        private Enemigo enemigo;

        public SalaEnemigo(int x, int y, Enemigo enemigo) : base(x, y)
        {
            this.enemigo = enemigo;
        }

        public override void Entrar(Partida partida, Jugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR)
            {
                Random random = new Random();
                int diferencia = random.Next(-1, 2);
                int nivelJugador = jugador.GetNivel() + diferencia;

                enemigo.SubirNivel(nivelJugador * Mob.EXP_MAX);

                partida.SetPosicionJugador(x, y);

                int res = partida.Combatir(jugador, enemigo, this);

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
        }
    }
}
