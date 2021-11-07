using System;
using System.Text;

using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;

namespace SquareDungeon
{
    static class EntradaSalida
    {
        public const int ELEGIR_ARMA = 0;
        public const int ELEGIR_OBJETO = 1;

        public static void MostrarPV(string nombre, int pvIniciales, int pvActuales)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine(nombre);
            Console.WriteLine(calcularBarraPV(pvIniciales, pvActuales));
            Console.WriteLine();
        }

        private static String calcularBarraPV(int pvIniciales, int pvActuales)
        {
            int numPuntos = (20 * pvActuales) / pvIniciales;
            string puntos = "";

            for (int i = 0; i <= numPuntos; i++)
            {
                puntos += "*";
            }

            while (puntos.Length < 20)
            {
                puntos += '·';
            }

            return puntos;
        }

        public static Arma ElegirArma(Arma[] armas)
        {
            string textoArmas = "Elige un arma:";
            int numArmas = 0;

            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] == null)
                    break;

                textoArmas += $"\n{(i + 1)}) {armas[i].GetNombre()}";
                numArmas++;
            }
            int armaElegida = 1;
            bool incorrecto;
            do
            {
                Console.WriteLine(textoArmas);
                string input = Console.ReadLine();
                try
                {
                    armaElegida = int.Parse(input);
                    incorrecto = (armaElegida < 1 || armaElegida > numArmas);
                    if (incorrecto)
                        Console.WriteLine("Elige un arma dentro del rango de armas disponibles");
                }
                catch (FormatException)
                {
                    incorrecto = true;
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }

            } while (incorrecto);
            return armas[armaElegida - 1];
        }

        public static void MostrarHabilidad(Mob mob, Habilidad habilidad)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ha utilizado {habilidad.GetNombre()}!");
        }

        public static void MostrarAtaque(Mob mob, Arma arma)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ataca con {arma.GetNombre()}!");
        }

        public static void MostrarAtaque(Enemigo enemigo)
        {
            Console.WriteLine($"¡El {enemigo.GetNombre()} enemigo te ataca!");
        }

        public static int ElegirArmaObjeto()
        {
            do
            {
                Console.WriteLine("1) Atacar\n2) Utilizar objeto");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);

                    if (eleccion == 1)
                        return ELEGIR_ARMA;

                    if (eleccion == 2)
                        return ELEGIR_OBJETO;

                    Console.WriteLine("Opción incorrecta, inténtalo otra vez");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static Objeto ElegirObjeto(Objeto[] objetos)
        {
            if (objetos[0] == null)
            {
                Console.WriteLine("Tu inventario está vacío");
                return null;
            }
            string textoObjetos = "Elige un objeto:";
            int numObjetos = 0;

            for (int i = 0; i < objetos.Length; i++)
            {
                Objeto objeto = objetos[i];
                if (objeto == null)
                    break;

                textoObjetos += $"\n{i + 1}) {objeto.GetNombre()}x{objeto.GetCantidad()}";
                numObjetos++;
            }

            do
            {
                Console.WriteLine(textoObjetos);
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);
                    if (eleccion < 0 || eleccion > numObjetos)
                        Console.WriteLine("Elige un objeto dentro del rango de onjetos disponibles.");
                    else
                        return objetos[eleccion - 1];
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }
    }
}
