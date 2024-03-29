﻿using System;

using SquareDungeon.Armas;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Modelo;
using SquareDungeon.Salas;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Cofre que contiene un <see cref="AbstractArma">arma</see>
    /// </summary>
    class CofreArma : AbstractCofre
    {
        public CofreArma(AbstractArma arma) : base(NOMBRE_COFRE_ARMA, DESC_COFRE_ARMA, arma) { }

        public override bool AbrirCofre(AbstractJugador jugador, AbstractSala sala)
        {
            try
            {
                bool armaEquipada = jugador.EquiparArma(getContenido());
                if (!armaEquipada)
                {
                    AbstractArma armaSeleccionada = EntradaSalida.Elegir("No puedes llevar más armas, elimina una para tener espacio", true, jugador.GetArmas());
                    if (armaSeleccionada != null)
                    {
                        jugador.EliminarArma(armaSeleccionada);
                        EntradaSalida.MostrarArmaConseguida(getContenido());
                        return true;
                    } else
                    {
                        return false;
                    }
                }
                else
                {
                    EntradaSalida.MostrarArmaConseguida(getContenido());
                    return true;
                }
            }
            catch (ArgumentException)
            {
                EntradaSalida.MostrarNoEquiparArma();
                return true;
            }
            finally
            {
                EntradaSalida.Esperar();
            }
        }

        protected override AbstractArma getContenido() => (AbstractArma)contenido;
    }
}
