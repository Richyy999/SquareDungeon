using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Salas
{
    class SalaVacia : Sala
    {
        public SalaVacia(int x, int y) : base(x, y) { }

        public override void Entrar(Partida partida, Jugador jugador)
        {
            partida.SetPosicionJugador(x, y);
            SetEstado(ESTADO_VISITADO);
        }
    }
}
