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

        public override void Entrar(AbstractJugador jugador)
        {
            Partida.GetInstance().SetPosicionJugador(x, y);
            SetEstado(ESTADO_VISITADO);
        }
    }
}
