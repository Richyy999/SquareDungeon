using System;

using SquareDungeon.Modelo;

namespace SquareDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Partida partida = Partida.GetInstance();
            int res = partida.JugarNiveles();

            if (res == Partida.RESULTADO_JEFE_ELIMINADO)
                EntradaSalida.MostrarMensaje("¡Derrotaste al jefe!");

            else if (res == Partida.RESULTADO_ENEMIGO_GANA)
                EntradaSalida.MostrarMensaje("Derrota...");

            EntradaSalida.MostrarMensaje("Fin del juego");
        }
    }
}
