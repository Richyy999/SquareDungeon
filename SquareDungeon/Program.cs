using System;

using SquareDungeon.Modelo;

namespace SquareDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Partida partida = new Partida();
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
