using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Salas
{
    class SalaEnemigo : AbstractSala
    {
        private AbstractEnemigo enemigo;

        public SalaEnemigo(int x, int y, AbstractEnemigo enemigo) : base(x, y)
        {
            this.enemigo = enemigo;
        }

        protected virtual void subirNivelEnemigo(Partida partida, AbstractJugador jugador)
        {
            int nivelJugador = jugador.GetNivel();
            int nivelPiso = partida.GetNivelPiso();

            int nivelBase = nivelJugador > nivelPiso ? nivelJugador : nivelPiso;

            Random random = new Random();
            int diferencia = random.Next(2);
            int nivelEnemigo = nivelBase + diferencia;

            if (nivelEnemigo >= 0)
                enemigo.SubirNivel(nivelEnemigo * AbstractMob.EXP_MAX);

            enemigo.CurarCompleto();
        }

        protected virtual void combatir(Partida partida, AbstractJugador jugador)
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

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR)
            {
                subirNivelEnemigo(partida, jugador);
                SetEstado(ESTADO_SALA_ENEMIGO_ABIERTA);
            }

            if (GetEstado() == ESTADO_SALA_ENEMIGO_ABIERTA)
            {
                combatir(partida, jugador);
            }

            if (GetEstado() == ESTADO_VISITADO)
                partida.SetPosicionJugador(x, y);
        }
    }
}
