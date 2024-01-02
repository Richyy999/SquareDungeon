using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon.Salas
{
    /// <summary>
    /// Sala que contiene un enemigo que el jugador deberá enfrentar
    /// </summary>
    class SalaEnemigo : AbstractSala
    {
        /// <summary>
        /// Enemigo al que se debe enfrentar el jugador
        /// </summary>
        private AbstractEnemigo enemigo;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="x">Coordenada X de la sala en el tablero</param>
        /// <param name="y">Coordenada Y de la sala en el tablero</param>
        /// <param name="enemigo">Enemigo de la sala</param>
        public SalaEnemigo(int x, int y, AbstractEnemigo enemigo) : base(x, y)
        {
            this.enemigo = enemigo;
        }

        /// <summary>
        /// Sube el nivel del <see cref="enemigo"/> al nivel mínimo del nivel o al nivel del usuario variando entre 0 y 3 niveles
        /// </summary>
        /// <param name="partida">Instancia de la partida</param>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que enfrenta el enemigo</param>
        protected virtual void subirNivelEnemigo(Partida partida, AbstractJugador jugador)
        {
            int nivelJugador = jugador.GetNivel();
            int nivelPiso = partida.GetNivelPiso();

            int nivelBase = nivelJugador > nivelPiso ? nivelJugador : nivelPiso;

            Random random = new Random();
            int diferencia = random.Next(4);
            int nivelEnemigo = nivelBase + diferencia;

            if (nivelEnemigo >= 0)
                enemigo.SubirNivel(nivelEnemigo * AbstractMob.EXP_MAX);

            enemigo.CurarCompleto();
        }

        /// <summary>
        /// Contiene toda la lógica del combate entre el <see cref="enemigo"/> y el <see cref="AbstractJugador">jugador</see>
        /// </summary>
        /// <param name="partida">Instancia de la partida</param>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que enfrenta al enemigo</param>
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

        public override void Entrar(AbstractJugador jugador)
        {
            Partida partida = Partida.GetInstance();
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
