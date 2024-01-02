using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Salas
{
    /// <summary>
    /// Clase básica que define las salas del juego. Todas las salas deben de heredar de esta clase
    /// </summary>
    abstract class AbstractSala
    {
        public const int ESTADO_SIN_VISITAR = 0;
        public const int ESTADO_VISITADO = 1;
        public const int ESTADO_COFRE_SIN_ABRIR = 2;
        public const int ESTADO_SALA_JEFE_SIN_ABRIR = 3;
        public const int ESTADO_SALA_JEFE_ABIERTA = 4;
        public const int ESTADO_SALA_ENEMIGO_ABIERTA = 5;

        /// <summary>
        /// Coordenadas X, Y de la sala en el tablero
        /// </summary>
        protected int x, y;

        /// <summary>
        /// Estado de la sala
        /// </summary>
        private int estado;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="x">Coordenada X de la sala en el tablero</param>
        /// <param name="y">Coordenada Y de la sala en el tablero</param>
        protected AbstractSala(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.estado = ESTADO_SIN_VISITAR;
        }

        /// <summary>
        /// Establece el estado de la sala
        /// </summary>
        /// <param name="estado">Estado a establecer</param>
        /// <exception cref="ArgumentException">Lanza una excepción si se establece un estado erróneo</exception>
        public void SetEstado(int estado)
        {
            if (estado < 0 || estado > 5)
                throw new ArgumentException("estado",
                    "Se ha recibido un estado inválido para la sala, utiliza las constantes de clase");

            this.estado = estado;
        }

        /// <summary>
        /// Realiza la lógica cuando el jugador entra en la sala
        /// </summary>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que entra en la sala</param>
        public abstract void Entrar(AbstractJugador jugador);

        /// <summary>
        /// Realiza la lógica para que el jugador entre en la sala
        /// </summary>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que entra en la sala</param>
        /// <returns>true si la sala se ha abierto, false en caso contrario</returns>
        public virtual bool AbrirSala(AbstractJugador jugador) => true;

        /// <summary>
        /// Devuelve el estado actual de la sala
        /// </summary>
        /// <returns>Estado de la sala</returns>
        public int GetEstado() => estado;

        /// <summary>
        /// Obtiene la coordenada X de la sala
        /// </summary>
        /// <returns>Coordenada X de la sala</returns>
        public int GetX() => x;

        /// <summary>
        /// Obtiene la coordenada Y de la sala
        /// </summary>
        /// <returns>Coordenada Y de la sala</returns>
        public int GetY() => y;
    }
}
