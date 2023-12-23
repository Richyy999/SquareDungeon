using SquareDungeon.Modelo;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Salas
{
    /// <summary>
    /// Sala vacía
    /// </summary>
    class SalaVacia : AbstractSala
    {
        public SalaVacia(int x, int y) : base(x, y) { }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            partida.SetPosicionJugador(x, y);
            SetEstado(ESTADO_VISITADO);
        }
    }
}
