﻿using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Habilidades;
using SquareDungeon.Modelo;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreHabilidad : AbstractCofre
    {
        public CofreHabilidad(AbstractHabilidad habilidad) : base(NOMBRE_COFRE_HABILIDAD, DESC_COFRE_HABILIDAD, habilidad) { }

        public override bool AbrirCofre(AbstractJugador jugador, AbstractSala sala, Partida partida)
        {
            bool habilidadAnadida = jugador.AnadirHabilidad(getContenido());
            if (habilidadAnadida)
                EntradaSalida.MostrarHabilidadObtenida(getContenido());
            else
                EntradaSalida.MostrarHabilidadEquipada(getContenido());

            return habilidadAnadida;
        }

        protected override AbstractHabilidad getContenido() => (AbstractHabilidad)contenido;
    }
}
