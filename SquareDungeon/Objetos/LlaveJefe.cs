using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Objetos
{
    class LlaveJefe : AbstractObjeto
    {
        public LlaveJefe() : base(1, NOMBRE_LLAVE_JEFE, DESC_LLAVE_JEFE) { }

        public override void RealizarAccion(AbstractJugador jugador, AbstractEnemigo enemigo, Sala sala)
        {
            if (sala.GetType() == typeof(SalaJefe))
                jugador.EliminarObjeto(this);
        }
    }
}
