using SquareDungeon.Salas;
using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    /// <summary>
    /// Llave que abre la sala del jefe
    /// </summary>
    class LlaveJefe : AbstractObjeto
    {
        public LlaveJefe() : base(1, NOMBRE_LLAVE_JEFE, DESC_LLAVE_JEFE) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            if (sala is SalaJefe)
                jugador.EliminarLlaveJefe();
        }
    }
}
