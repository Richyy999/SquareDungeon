using System;

using SquareDungeon.Modelo;
using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

namespace SquareDungeon.Salas
{
    /// <summary>
    /// Sala del jefe del piso. Debe ser derrotado para avanzar al siguiente piso
    /// </summary>
    class SalaJefe : SalaEnemigo
    {
        /// <summary>
        /// Jefe del nivel
        /// </summary>
        private AbstractJefe jefe;

        /// <summary>
        /// Indicador para subir o no el nivel del jefe
        /// </summary>
        /// <seealso cref="SalaEnemigo.subirNivelEnemigo(Partida, AbstractJugador)"/>
        private bool subirNivel;
        /// <summary>
        /// Indicador de que la sala ha sido abierta
        /// </summary>
        private bool abierta;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="x">Coordenada X de la sala en el tablero</param>
        /// <param name="y">Coordenada Y de la sala en el tablero</param>
        /// <param name="jefe">Jefe de la sala</param>
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

        public override void Entrar(AbstractJugador jugador)
        {
            Partida partida = Partida.GetInstance();
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
                LlaveJefe llave = null;
                EntradaSalida.DescubrirJefe();
                SetEstado(ESTADO_SALA_JEFE_SIN_ABRIR);
                llave = jugador.GetLlaveJefe();
                if (llave == null)
                    return false;

                if (!EntradaSalida.PreguntarSiNo("¿Quieres abrir esta sala?"))
                    return false;

                llave.RealizarAccion(jugador, null, this);
                SetEstado(ESTADO_SALA_JEFE_ABIERTA);
                abierta = true;
                return abierta;
            }

            return abierta;
        }
    }
}
