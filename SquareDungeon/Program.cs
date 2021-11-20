using System;

using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Jugador jugador = Fabrica.GenerarJugador();

            Partida partida = new Partida(jugador);
            partida.Jugar();

            int res = partida.GetResultado();

            if (res == Partida.RESULTADO_JEFE_ELIMINADO)
            {
                Console.WriteLine("¡Derrotaste al jefe!");
                Console.WriteLine("Fin del juego");
            } else if (res == Partida.RESULTADO_ENEMIGO_GANA)
            {
                Console.WriteLine("Derrota...");
                Console.WriteLine("Fin del juego");
            }
        }
    }
}
