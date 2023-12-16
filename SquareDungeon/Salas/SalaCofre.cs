using System;

using SquareDungeon.Modelo;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Cofres;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Modelo.EntradaSalida;

namespace SquareDungeon.Salas
{
    class SalaCofre : AbstractSala
    {
        private AbstractCofre cofre;

        public SalaCofre(int x, int y, AbstractCofre cofre) : base(x, y)
        {
            this.cofre = cofre;
        }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR || GetEstado() == ESTADO_COFRE_SIN_ABRIR)
            {
                SetEstado(ESTADO_COFRE_SIN_ABRIR);
                if (PreguntarAbrirCofre())
                {
                    bool cofreAbierto = cofre.AbrirCofre(jugador, this, partida);
                    if (cofreAbierto)
                        SetEstado(ESTADO_VISITADO);
                }
            }

            if (GetEstado() == ESTADO_VISITADO)
                partida.SetPosicionJugador(x, y);
        }
    }
}
