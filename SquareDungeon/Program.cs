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
            Console.WriteLine("Bienvenido. Introduce tu nombre:");
            string nombre = Console.ReadLine();

            Guerrero guerrero = new Guerrero(nombre, new AntiSlime());
            EspadaHierro espadaHierro = new EspadaHierro(guerrero, NOMBRE_ESPADA_HIERRO, DESC_ESPADA_HIERRO);
            guerrero.EquiparArma(espadaHierro);

            Slime slime = new Slime(null);

            guerrero.SubirNivel(10 * 100);
            slime.SubirNivel(10 * 100);

            int res = Partida.Combatir(guerrero, slime, null);
            if (res == Partida.RESULTADO_JUGADOR_GANA)
            {
                guerrero.ReiniciarStatsCombate();
                Console.WriteLine("¡Victoria!");
                Console.WriteLine($"¡Obtienes {slime.GetExp()} puntos de experiencia!");
                guerrero.SubirNivel(slime.GetExp());
            }
            else
                Console.WriteLine("Derrota");
        }
    }
}
