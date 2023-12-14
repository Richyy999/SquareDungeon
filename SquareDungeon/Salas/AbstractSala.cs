using System;

using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Salas
{
    abstract class AbstractSala
    {
        public const int ESTADO_SIN_VISITAR = 0;
        public const int ESTADO_VISITADO = 1;
        public const int ESTADO_COFRE_SIN_ABRIR = 2;
        public const int ESTADO_SALA_JEFE_SIN_ABRIR = 3;
        public const int ESTADO_SALA_JEFE_ABIERTA = 4;
        public const int ESTADO_SALA_ENEMIGO_ABIERTA = 5;

        protected int x, y;

        private int estado;

        protected AbstractSala(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.estado = ESTADO_SIN_VISITAR;
        }

        public void SetEstado(int estado)
        {
            if (estado < 0 || estado > 5)
                throw new ArgumentException("estado",
                    "Se ha recibido un estado inválido para la sala, utiliza las constantes de clase");

            this.estado = estado;
        }

        public virtual bool AbrirSala(AbstractJugador jugador) => true;

        public int GetEstado() => estado;

        public int GetX() => x;

        public int GetY() => y;

        public abstract void Entrar(Partida partida, AbstractJugador jugador);
    }
}
