using System;

using SquareDungeon.Habilidades.Ataque;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("¡Bienvenido!\nEscribe tu nombre:");
            string nombre = Console.ReadLine();
            
            Guerrero guerrero = new Guerrero(nombre, new AntiSlime());
            ViolaSlimes violaSlimes = new ViolaSlimes();
            violaSlimes.SetPortador(guerrero);
            guerrero.EquiparArma(violaSlimes);

            Partida partida = new Partida(guerrero);
            partida.Jugar();

            Console.WriteLine("¡Derrotaste al jefe!");
            Console.WriteLine("Fin del juego");
        }
    }
}
