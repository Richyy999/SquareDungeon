using System;
using System.Threading;
using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Modelo.Partida;
using static SquareDungeon.Salas.Sala;

namespace SquareDungeon.Modelo
{
    static class EntradaSalida
    {
        public const int VOLVER = -1;

        public const int ELEGIR_ARMA = 0;
        public const int ELEGIR_OBJETO = 1;
        public const int ELEGIR_HUIR = 2;

        public const int MENU_STATS = 10;
        public const int MENU_ARMAS = 11;
        public const int MENU_OBJETOS = 12;
        public const int MENU_HABILIDADES = 13;

        public const int ELECCION_GUERRERO = 100;
        public const int ELECCION_MAGO = 101;

        private const string CASILLA_JUGADOR = " o ";
        private const string CASILLA_COFRE = " + ";
        private const string CASILLA_JEFE = " X ";
        private const string CASILLA_ENEMIGO = " x ";
        private const string CASILLA_JEFE_SIN_ABRIR = " * ";
        private const string CASILLA_VACIA = " \\ ";
        private const string CASILLA_SIN_VISITAR = "   ";

        public static void MostrarPV(AbstractMob mob, int pvIniciales, int pvActuales)
        {
            string nombre = mob.GetNombre();
            string nivel = "Nv " + mob.GetNivel().ToString();
            while (nombre.Length + nivel.Length < 26)
            {
                nivel = " " + nivel;
            }
            Console.WriteLine(nombre + nivel);
            Console.WriteLine(calcularBarraPV(pvIniciales, pvActuales));
            if (mob.GetType().IsSubclassOf(typeof(AbstractJugador)))
            {
                string pv = mob.GetStat(AbstractMob.INDICE_VIDA).ToString();
                string pvTotal = mob.GetStat(AbstractMob.INDICE_VIDA_TOTAL).ToString();
                Console.WriteLine(pv + "/" + pvTotal);
            }
            Console.WriteLine();
        }

        private static string calcularBarraPV(int pvIniciales, int pvActuales)
        {
            int numPuntos = (25 * pvActuales) / pvIniciales;
            string puntos = "";

            for (int i = 0; i <= numPuntos; i++)
            {
                puntos += "*";
            }

            while (puntos.Length < 26)
            {
                puntos += '·';
            }

            return puntos;
        }

        public static AbstractArma ElegirArma(AbstractArma[] armas)
        {
            string textoArmas = "Elige un arma:";
            int numArmas = 0;

            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] == null)
                    break;

                textoArmas += $"\n{(i + 1)}) {armas[i].GetNombre()} {armas[i].GetUsos()}/{armas[i].GetUsosMaximos()}";
                numArmas++;
            }
            numArmas++;
            textoArmas += $"\n{numArmas}) Cancelar";
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
                    if (armaElegida == numArmas)
                        return null;

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

        public static void MostrarHabilidad(AbstractMob mob, AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ha utilizado {habilidad.GetNombre()}!");
        }

        public static void MostrarHabilidad(AbstractArma arma, AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡{arma.GetNombre()} ha utilizado {habilidad.GetNombre()}!");
        }

        public static void MostrarAtaque(AbstractMob mob, AbstractArma arma)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ataca con {arma.GetNombre()}!");
        }

        public static void MostrarAtaque(AbstractEnemigo enemigo)
        {
            Console.WriteLine($"¡El {enemigo.GetNombre()} enemigo te ataca!");
        }

        public static int ElegirArmaObjeto()
        {
            do
            {
                Console.WriteLine("1) Atacar\n2) Utilizar objeto\n3) Hiur");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);

                    if (eleccion == 1)
                        return ELEGIR_ARMA;

                    if (eleccion == 2)
                        return ELEGIR_OBJETO;

                    if (eleccion == 3)
                        return ELEGIR_HUIR;

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

                textoObjetos += $"\n{i + 1}) {objeto.GetNombre()} x{objeto.GetCantidad()}";
                numObjetos++;
            }
            numObjetos++;
            textoObjetos += $"\n{numObjetos}) Cancelar";

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
                        if (eleccion == numObjetos)
                        return null;

                    return objetos[eleccion - 1];
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static LlaveJefe GetLlaveJefe(Objeto[] objetos)
        {
            if (objetos[0] == null)
                return null;

            LlaveJefe llave = null;
            foreach (Objeto objeto in objetos)
            {
                if (objeto != null)
                    if (objeto.GetType() == typeof(LlaveJefe))
                        llave = (LlaveJefe)objeto;
            }

            Console.WriteLine("¿Quieres abrir esta sala?");

            if (llave == null)
                return llave;

            do
            {
                Console.WriteLine("1)Abrir sala\n2)No abrir la sala");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);

                    if (eleccion == 1)
                        return llave;

                    if (eleccion == 2)
                        return null;

                    Console.WriteLine("Opción no válida");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static bool PreguntarAbrirCofre()
        {
            do
            {
                Console.WriteLine("Esta sala contiene un cofre.\n¿Quieres abrirlo?\n1) Sí\n2) No");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);

                    if (eleccion == 1)
                        return true;

                    if (eleccion == 2)
                        return false;

                    Console.WriteLine("Opción no válida");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }

            } while (true);
        }

        public static void MostrarTablero(Sala[,] tablero, int jugadorX, int jugadorY)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("---------------------------------");
            for (int i = 0; i < 8; i++)
            {
                string linea = "|";
                for (int j = 0; j < 8; j++)
                {
                    if (i == jugadorX && j == jugadorY)
                    {
                        linea += CASILLA_JUGADOR + "|";
                        continue;
                    }

                    Sala casilla = tablero[i, j];
                    switch (casilla.GetEstado())
                    {
                        case ESTADO_SIN_VISITAR:
                            linea += CASILLA_SIN_VISITAR + "|";
                            break;

                        case ESTADO_VISITADO:
                            linea += CASILLA_VACIA + "|";
                            break;

                        case ESTADO_COFRE_SIN_ABRIR:
                            linea += CASILLA_COFRE + "|";
                            break;

                        case ESTADO_SALA_JEFE_SIN_ABRIR:
                            linea += CASILLA_JEFE_SIN_ABRIR + "|";
                            break;

                        case ESTADO_SALA_JEFE_ABIERTA:
                            linea += CASILLA_JEFE + "|";
                            break;

                        case ESTADO_SALA_ENEMIGO_ABIERTA:
                            linea += CASILLA_ENEMIGO + "|";
                            break;
                    }
                }
                Console.WriteLine(linea);
                if (i < 7)
                    Console.WriteLine("|---+---+---+---+---+---+---+---|");
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }

        public static int MenuAcciones()
        {
            do
            {
                Console.WriteLine("1) Mover arriba\n2) Mover abajo\n3) Mover a la derecha\n4) Mover a la izquierda" +
                    "\n5) Menu");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);
                    switch (eleccion)
                    {
                        case 1:
                            return MOVER_ARRIBA;

                        case 2:
                            return MOVER_ABAJO;

                        case 3:
                            return MOVER_DERECHA;

                        case 4:
                            return MOVER_IZQUIERDA;

                        case 5:
                            return ABRIR_MENU;

                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static void MostrarVictoria(AbstractJugador jugador, AbstractEnemigo enemigo)
        {
            Console.WriteLine("¡Victoria!");
            Objeto drop = enemigo.Drop();
            if (drop != null)
                Console.WriteLine($"¡Obtuviste {drop.GetNombre()}!");

            if (!jugador.AnadirObjeto(drop))
            {
                Console.WriteLine("Tu inventario está lleno, elimina un objeto para ganar espacio");
                Objeto objeto = ElegirObjeto(jugador.GetObjetos());
                jugador.EliminarObjeto(objeto);
            }
            Console.WriteLine($"¡Obtuviste {enemigo.GetExp()} puntos de experiencia!");
            if (jugador.SubirNivel(enemigo.GetExp()))
            {
                Console.WriteLine("\nPulsa Enter para continuar");
                Console.ReadLine();
                mostrarNivelSubido(jugador);
            }
            else
            {
                Console.WriteLine("\nPulsa Enter para continuar");
                Console.ReadLine();
            }
        }

        private static void mostrarNivelSubido(AbstractJugador jugador)
        {
            Console.Clear();
            Console.WriteLine($"¡Has subido al nivel {jugador.GetNivel()}!");
            int[,] stats = jugador.GetStatsNuevos();
            string texto = "";
            for (int i = 0; i < 10; i++)
            {
                string linea = "";
                switch (i)
                {
                    case 0:
                        linea += "PV        ";
                        break;

                    case 1:
                        linea += "Fue       ";
                        break;

                    case 2:
                        linea += "Mag       ";
                        break;

                    case 3:
                        linea += "Agi       ";
                        break;

                    case 4:
                        linea += "Def       ";
                        break;

                    case 5:
                        linea += "Res       ";
                        break;

                    case 6:
                        linea += "% Crítico ";
                        break;

                    case 7:
                        linea += "Daño Crit ";
                        break;

                    case 8:
                        linea += "Exp       ";
                        break;

                    case 9:
                        linea += "Nivel     ";
                        break;
                }

                string stat1 = stats[i, 0].ToString();
                string stat2 = stats[i, 1].ToString();

                while (stat1.Length < 3)
                {
                    stat1 = " " + stat1;
                }

                while (stat2.Length < 3)
                {
                    stat2 = " " + stat2;
                }

                texto = linea + stat1 + " -> " + stat2;
                Console.WriteLine(texto);
            }
            Console.WriteLine("\nPulsa Enter para seguir");
            Console.ReadLine();
        }

        public static void MostrarObjetoConseguido(Objeto objeto)
        {
            Console.WriteLine($"¡Obtuviste {objeto.GetNombre()}!");
            Console.WriteLine("\nPulsa Enter para continuar");
            Console.ReadLine();
        }

        public static void MostrarHabilidadObtenida(AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡Obtuviste {habilidad.GetNombre()}!");
            Console.WriteLine("\nPulsa Enter para continuar");
            Console.ReadLine();
        }

        public static void MostrarArmaConseguida(AbstractArma arma)
        {
            Console.WriteLine($"¡Obtuviste {arma.GetNombre()}!");
            Console.WriteLine("\nPulsa Enter para continuar");
            Console.ReadLine();
        }

        public static void MostrarNoEquiparArma()
        {
            Console.WriteLine("Tu personaje no puede usar esta arma");
            Thread.Sleep(1000);
        }

        public static void MostrarHabilidadEquipada(AbstractHabilidad habilidad)
        {
            Console.WriteLine($"Ya posees la habilidad {habilidad.GetNombre()}");
            Thread.Sleep(1000);
        }

        public static int MostrarMenu(AbstractJugador jugador)
        {
            Console.Clear();
            do
            {
                Console.WriteLine("1) Ver stats\n2) Ver armas\n3) Usar objeto\n4) Ver habilidades\n5) Volver");
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);
                    switch (eleccion)
                    {
                        case 1:
                            return MENU_STATS;

                        case 2:
                            return MENU_ARMAS;

                        case 3:
                            return MENU_OBJETOS;

                        case 4:
                            return MENU_HABILIDADES;

                        case 5:
                            return VOLVER;

                        default:
                            Console.WriteLine("Opción no válida");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static void MostrarStats(AbstractJugador jugador)
        {
            Console.Clear();
            int[] stats = jugador.GetStats();
            string texto = "\nPV";
            string pv = jugador.GetStat(AbstractMob.INDICE_VIDA).ToString();
            string pvTotal = jugador.GetStat(AbstractMob.INDICE_VIDA_TOTAL).ToString();
            string lineaPv = pv + "/" + pvTotal;
            while (lineaPv.Length < 11)
            {
                lineaPv = " " + lineaPv;
            }
            texto += lineaPv;
            for (int i = 2; i < stats.Length; i++)
            {
                string linea = "";
                switch (i)
                {
                    case 2:
                        linea += "Fue       ";
                        break;

                    case 3:
                        linea += "Mag       ";
                        break;

                    case 4:
                        linea += "Agi       ";
                        break;

                    case 5:
                        linea += "Def       ";
                        break;

                    case 6:
                        linea += "Res       ";
                        break;

                    case 7:
                        linea += "% Crítico ";
                        break;

                    case 8:
                        linea += "Daño Crit ";
                        break;

                    case 9:
                        linea += "Exp       ";
                        break;

                    case 10:
                        linea += "Nivel     ";
                        break;
                }

                string stat = stats[i].ToString();
                while (stat.Length < 3)
                {
                    stat = " " + stat;
                }

                texto += "\n" + linea + stat;
            }

            Console.WriteLine(texto);
            Console.WriteLine("\nPulsa Enter para volver");
            Console.ReadLine();
        }

        public static void MostrarArma(AbstractArma arma)
        {
            Console.Clear();
            Console.WriteLine($"{arma.GetNombre()}:");
            Console.WriteLine(arma.GetDescripcion());
            Console.WriteLine($"\nDaño: {arma.GetDano()}");
            Console.WriteLine($"Usos: {arma.GetUsos()}/{arma.GetUsosMaximos()}");

            AbstractHabilidad habilidad = arma.GetHabilidad();
            Console.WriteLine($"\nHabilidad: {habilidad.GetNombre()}");
            Console.WriteLine(habilidad.GetDescripcion());
            Console.WriteLine("\nPulsa Enter para continuar\n");
            Console.ReadLine();
        }

        public static AbstractHabilidad ElegirHabilidad(List<AbstractHabilidad> habilidades)
        {
            if (habilidades.Count == 0)
            {
                Console.WriteLine("No tienes ninguna habilidad");
                return null;
            }

            string texto = "Elige una habilidad:";
            int numHabilidades = 0;
            for (int i = 0; i < habilidades.Count; i++)
            {
                texto += $"\n{i + 1}) {habilidades[i].GetNombre()}";
                numHabilidades++;
            }
            numHabilidades++;
            texto += $"\n{numHabilidades}) Cancelar";


            do
            {
                Console.WriteLine(texto);
                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);
                    if (eleccion < 0 || eleccion > numHabilidades)
                        Console.WriteLine("Elige un objeto dentro del rango de onjetos disponibles.");
                    else if (eleccion == numHabilidades)
                        return null;
                    else
                        return habilidades[eleccion - 1];
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, que gracioso...");
                }
            } while (true);
        }

        public static void MostrarHabilidad(AbstractHabilidad habilidad)
        {
            Console.Clear();
            Console.WriteLine(habilidad.GetNombre());
            Console.WriteLine(habilidad.GetDescripcion());
            Console.WriteLine("\nPulsa Enter para continuar");
            Console.ReadLine();
        }

        public static void MostrarUsarObjeto(Objeto objeto, AbstractJugador jugador, AbstractEnemigo enemigo, Sala sala)
        {
            Console.Clear();
            Console.WriteLine(objeto.GetNombre() + " x " + objeto.GetCantidad());
            Console.WriteLine(objeto.GetDescripcion());
            Console.WriteLine("\n1) Usar\n2) Volver");
            int eleccion = 0;
            do
            {
                try
                {
                    string input = Console.ReadLine();
                    eleccion = int.Parse(input);
                    if (eleccion != 1 && eleccion != 2)
                        Console.WriteLine("Opción no válida");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (eleccion != 1 && eleccion != 2);
            if (eleccion == 1)
            {
                try
                {
                    objeto.RealizarAccion(jugador, enemigo, sala);
                    if (objeto.GetCantidad() == 0)
                        Console.WriteLine($"Te quedaste sin {objeto.GetNombre()}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("No puede usar este objeto aquí");
                }
            }
            Console.WriteLine("\nPulsa Enter para continuar");
            Console.ReadLine();
        }

        public static void DescubrirJefe()
        {
            Console.WriteLine("Has encontrado la sala del Jefe. Se necesita la Llave del Jefe para abrirla");
            Thread.Sleep(1500);
        }

        public static string PedirNombre()
        {
            Console.Clear();
            Console.WriteLine("¡Bienvenido!\nEscribe tu nombre:");
            string nombre = "";
            do
            {
                nombre = Console.ReadLine();
            } while (nombre.Trim().Equals(""));
            return nombre.Trim();
        }

        public static int PedirPersonaje()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Elige a tu personaje:");
                Console.WriteLine("1) Guerrero");
                Console.WriteLine("2) Mago");

                try
                {
                    string input = Console.ReadLine();
                    int eleccion = int.Parse(input);
                    switch (eleccion)
                    {
                        case 1:
                            return ELECCION_GUERRERO;

                        case 2:
                            return ELECCION_MAGO;

                        default:
                            Console.WriteLine("Opción no válida. Elige uno de los personajes disponibles");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ja ja ja, muy gracioso...");
                }
            } while (true);
        }

        public static void NuevaLinea()
        {
            Console.WriteLine();
        }

        public static void Clear()
        {
            Console.Clear();
        }
    }
}
