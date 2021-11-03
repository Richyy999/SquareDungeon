using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Armas.ArmasMagicas;

namespace SquareDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Guerrero guerrero = new Guerrero();

            GrimorioBasico grimorio = new GrimorioBasico(guerrero);

            guerrero.EquiparArma(grimorio);

            int[] stats = guerrero.GetStats();

            foreach (int stat in stats)
            {
                System.Console.WriteLine(stat);
            }

            System.Console.WriteLine();

            guerrero.SubirNivel(157);

            int[,] statsNuevos = guerrero.GetStatsNuevos();

            System.Console.WriteLine($"PV         {statsNuevos[0, 0]}  ->  {statsNuevos[0, 1]}");
            System.Console.WriteLine($"Fue        {statsNuevos[1, 0]}  ->  {statsNuevos[1, 1]}");
            System.Console.WriteLine($"Mag        {statsNuevos[2, 0]}  ->  {statsNuevos[2, 1]}");
            System.Console.WriteLine($"Agi        {statsNuevos[3, 0]}  ->  {statsNuevos[3, 1]}");
            System.Console.WriteLine($"Def        {statsNuevos[4, 0]}  ->  {statsNuevos[4, 1]}");
            System.Console.WriteLine($"Res        {statsNuevos[5, 0]}  ->  {statsNuevos[5, 1]}");
            System.Console.WriteLine($"ProbCrit   {statsNuevos[6, 0]}  ->  {statsNuevos[6, 1]}");
            System.Console.WriteLine($"DañoCrit   {statsNuevos[7, 0]}  ->  {statsNuevos[7, 1]}");
            System.Console.WriteLine($"EXP        {statsNuevos[8, 0]}  ->  {statsNuevos[8, 1]}");
            System.Console.WriteLine($"Nivel      {statsNuevos[9, 0]}  ->  {statsNuevos[9, 1]}");
        }
    }
}
