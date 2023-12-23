﻿using System;

using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Entidad contenida en las salas de cofre.
    /// <seealso cref="Salas.AbstractSala"/>
    /// </summary>
    abstract class AbstractCofre : AbstractEntidad
    {
        /// <summary>
        /// Contenido del cofre
        /// </summary>
        protected object contenido;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="nombre">Nombre del cofre</param>
        /// <param name="descripcion">Descripción del cofre</param>
        /// <param name="contenido">Contenido del cofre</param>
        protected AbstractCofre(string nombre, string descripcion, object contenido) : base(nombre, descripcion)
        {
            this.contenido = contenido;
        }

        /// <summary>
        /// Realiza la lógica que abre el cofre
        /// </summary>
        /// <returns>true si el cofre se ha abuerto, false en caso contrario</returns>
        public abstract bool AbrirCofre(AbstractJugador jugador, AbstractSala sala, Partida partida);

        /// <summary>
        /// Devuelve el contenido del cofre
        /// </summary>
        /// <returns>Contenido del cofre</returns>
        protected abstract object getContenido();
    }
}