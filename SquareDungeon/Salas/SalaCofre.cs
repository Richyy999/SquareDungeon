﻿using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Cofres;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Salas
{
    /// <summary>
    /// Sala que contiene un cofre
    /// </summary>
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
                if (EntradaSalida.PreguntarAbrirCofre())
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
