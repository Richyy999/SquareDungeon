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
using static SquareDungeon.Salas.AbstractSala;

namespace SquareDungeon.Modelo
{
    /// <summary>
    /// Clase encargada de mostrar y capturar la información por consola
    /// </summary>
    static class EntradaSalida
    {
        public const int VOLVER = -1;

        public const string UN_NIVEL = "1 Nivel";
        public const string DOS_NIVELES = "2 Niveles";
        public const string TRES_NIVELES = "3 Niveles";
        public const string CUATRO_NIVELES = "4 Niveles";
        public const string CINCO_NIVELES = "5 Niveles";

        public const string ELEGIR_ARMA = "Atacar";
        public const string ELEGIR_OBJETO = "Usar objeto";
        public const string ELEGIR_HUIR = "Huir";

        public const string MENU_STATS = "Ver Stats";
        public const string MENU_ARMAS = "Ver armas";
        public const string MENU_OBJETOS = "Ver objetos";
        public const string MENU_HABILIDADES = "Ver habilidades";

        public const string SI = "Sí";
        public const string NO = "No";

        public const string GUERRERO = "Guerrero";
        public const string MAGO = "Mago";

        private const string CASILLA_JUGADOR = " o ";
        private const string CASILLA_COFRE = " ? ";
        private const string CASILLA_JEFE = " * ";
        private const string CASILLA_ENEMIGO = " + ";
        private const string CASILLA_JEFE_SIN_ABRIR = " x ";
        private const string CASILLA_VACIA = " \\ ";
        private const string CASILLA_SIN_VISITAR = "   ";

        /// <summary>
        /// Almacena el estado actualizado del juego. Ya sea la pantalla de combate o el tablero
        /// </summary>
        private static string migas = "";

        /// <summary>
        /// Muestra un mensaje en la consola
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        public static void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        /// <summary>
        /// Muestra las barras de vida del enemigo y el jugador
        /// </summary>
        /// <param name="enemigo">Enemigo</param>
        /// <param name="pvInicialesEnemigo">Vida total del enemigo</param>
        /// <param name="pvActualesEnemigo">Vida actual del enemigo</param>
        /// <param name="jugador">Jugador</param>
        /// <param name="pvInicialesJugador">Vida total del jugador</param>
        /// <param name="pvActualesJugador">Vida actual del jugador</param>
        public static void MostrarBarrasPV(AbstractMob enemigo, int pvInicialesEnemigo, int pvActualesEnemigo, AbstractMob jugador, int pvInicialesJugador, int pvActualesJugador)
        {
            iniciarMigas(MostrarPV(enemigo, pvInicialesEnemigo, pvActualesEnemigo), false);
            anadirMiga(MostrarPV(jugador, pvInicialesJugador, pvActualesJugador), true);
        }

        /// <summary>
        /// Muestra la barra de vida de un mob. Si el mob es un <see cref="AbstractJugador">jugador</see> muestra la cantidad de PV que posee
        /// </summary>
        /// <param name="mob">Mob cuya vida se mostrará</param>
        /// <param name="pvIniciales">PV totales del mob</param>
        /// <param name="pvActuales">PV actuales del mob</param>
        public static string MostrarPV(AbstractMob mob, int pvIniciales, int pvActuales)
        {
            string output;
            string nombre = mob.GetNombre();
            string nivel = "Nv " + mob.GetNivel().ToString();
            while (nombre.Length + nivel.Length < 26)
            {
                nivel = " " + nivel;
            }
            output = nombre + nivel + "\n";
            output += calcularBarraPV(pvIniciales, pvActuales) + "\n";
            if (mob is AbstractJugador)
            {
                string pv = mob.GetStat(AbstractMob.INDICE_VIDA).ToString();
                string pvTotal = mob.GetStat(AbstractMob.INDICE_VIDA_TOTAL).ToString();
                output += pv + "/" + pvTotal + "\n";
            }

            return output;
        }

        /// <summary>
        /// Genera la barra de vida en función de los PV totales y actuales de un mob
        /// </summary>
        /// <param name="pvIniciales">PV totales del mob</param>
        /// <param name="pvActuales">PV actuales del mob</param>
        /// <returns>String con la barra de vida</returns>
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

        /// <summary>
        /// Da a elegir uno de los elementos de la lista y devuelve la selección
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos a elegir</typeparam>
        /// <param name="mensaje">Mensaje para mostrar en la selección</param>
        /// <param name="nullable">true si el resultado puede ser nulo, false en caso contrario</param>
        /// <param name="lista">Lista de elementos a mostrar</param>
        /// <returns>Elección del jugador</returns>
        public static T Elegir<T>(string mensaje, bool nullable, List<T> lista)
        {
            return Elegir(mensaje, nullable, lista.ToArray());
        }

        /// <summary>
        /// Da a elegir uno de los elementos de la lista y devuelve la selección
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos a elegir</typeparam>
        /// <param name="mensaje">Mensaje para mostrar en la selección</param>
        /// <param name="nullable">true si el resultado puede ser nulo, false en caso contrario</param>
        /// <param name="elementos">Lista de elementos a mostrar</param>
        /// <returns>Elección del jugador</returns>
        public static T Elegir<T>(string mensaje, bool nullable, params T[] elementos)
        {
            var res = paginar(elementos, mensaje, nullable, 0);
            if (!EqualityComparer<T>.Default.Equals(res, default(T)) || nullable)
                return res;

            return Elegir(mensaje, nullable, elementos);
        }

        /// <summary>
        /// Muestra la lista de elementos a seleccionar. Si la lista supera los 9 elementos, se podrá paginar la lista
        /// </summary>
        /// <typeparam name="T">Tipo de los elementos a elegir</typeparam>
        /// <param name="array">Lista de elementos a mostrar</param>
        /// <param name="mensaje">Mensaje para mostrar en la selección</param>
        /// <param name="nullable">true si se puede cancelar la elección, false en caso contrario</param>
        /// <param name="primeraPosicion">Índice del array en la primera posición de la paginacióon</param>
        /// <returns>Elemento del array seleccionado</returns>
        private static T paginar<T>(T[] array, string mensaje, bool nullable, int primeraPosicion)
        {
            do
            {
                Clear();
                if (!string.IsNullOrEmpty(migas))
                    MostrarMigas();

                if (!string.IsNullOrEmpty(mensaje))
                    Console.WriteLine(mensaje);

                bool subir = primeraPosicion > 0;
                bool bajar = array.Length - primeraPosicion > 9;

                if (array.Length > 9)
                    Console.WriteLine("Usa las flechas para paginar");

                int numEleccion = 1;
                for (int i = primeraPosicion; i < array.Length && numEleccion < 10; i++)
                {
                    var elemento = array[i];
                    Console.WriteLine(numEleccion + ") " + elemento.ToString());
                    numEleccion++;
                }

                if (nullable)
                    Console.WriteLine("\nEsc) Cancelar");

                int indice = -1;
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        indice = 0 + primeraPosicion;
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        indice = 1 + primeraPosicion;
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        indice = 2 + primeraPosicion;
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        indice = 3 + primeraPosicion;
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        indice = 4 + primeraPosicion;
                        break;

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        indice = 5 + primeraPosicion;
                        break;

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        indice = 6 + primeraPosicion;
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        indice = 7 + primeraPosicion;
                        break;

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        indice = 8 + primeraPosicion;
                        break;

                    case ConsoleKey.UpArrow:
                        if (subir)
                            return paginar(array, mensaje, nullable, primeraPosicion - 1);
                        break;

                    case ConsoleKey.DownArrow:
                        if (bajar)
                            return paginar(array, mensaje, nullable, primeraPosicion + 1);
                        break;

                    case ConsoleKey.Escape:
                        return default(T);
                }

                if (indice >= 0 && indice < array.Length)
                    return array[indice];
            }
            while (true);
        }

        /// <summary>
        /// Hace una pregunta de sí o no al jugador
        /// </summary>
        /// <param name="mensaje">Pregunta a realizar</param>
        /// <returns>true si el jugador ha elegido Sí, false en caso contrario</returns>
        /// <seealso cref="Elegir{T}(string, bool, List{T})"/>
        /// <seealso cref="Elegir{T}(string, bool, T[])"/>
        public static bool PreguntarSiNo(string mensaje)
        {
            string res = Elegir(mensaje, false, SI, NO);
            return SI.Equals(res);
        }

        /// <summary>
        /// Muestra todas las armas del jugador y le solicita que elija una
        /// </summary>
        /// <param name="armas">Armas del jugador</param>
        /// <returns></returns>
        public static AbstractArma ElegirArma(AbstractArma[] armas)
        {
            return Elegir("Elige un arma:", true, armas);
        }

        /// <summary>
        /// Muestra por consola que un mob ha usado una habilidad
        /// </summary>
        /// <param name="mob">Mob que usa la habilidad</param>
        /// <param name="habilidad">Habilidad que ha usado el mob</param>
        public static void MostrarHabilidad(AbstractMob mob, AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ha utilizado {habilidad.GetNombre()}!");
        }

        /// <summary>
        /// Muestra por consola que un arma ha usado una habilidad
        /// </summary>
        /// <param name="arma">Arma que usa la habilidad</param>
        /// <param name="habilidad">Habilidad que ha usado el arma</param>
        public static void MostrarHabilidad(AbstractArma arma, AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡{arma.GetNombre()} ha utilizado {habilidad.GetNombre()}!");
        }

        /// <summary>
        /// Muestra un mensaje indicando que un mob ha atacado con un arma
        /// </summary>
        /// <param name="mob"></param>
        /// <param name="arma"></param>
        public static void MostrarAtaque(AbstractMob mob, AbstractArma arma)
        {
            Console.WriteLine($"¡{mob.GetNombre()} ataca con {arma.GetNombre()}!");
        }

        /// <summary>
        /// Muestra por consola que un <see cref="AbstractEnemigo">enemigo</see> ha atacado
        /// </summary>
        /// <param name="enemigo"></param>
        public static void MostrarAtaque(AbstractEnemigo enemigo)
        {
            Console.WriteLine($"¡El {enemigo.GetNombre()} enemigo te ataca!");
        }

        /// <summary>
        /// Muestra por consola el daño que un mob ejerce a otro
        /// </summary>
        /// <param name="atacante">Mob que ataca</param>
        /// <param name="victima">Mob que recibe el ataque</param>
        /// <param name="dano">Daño realizado</param>
        public static void MostrarDano(AbstractMob atacante, AbstractMob victima, int dano)
        {
            Console.WriteLine($"{atacante.GetNombre()} le inlfigió {dano} puntos de daño a {victima.GetNombre()}");
        }

        /// <summary>
        /// Da a elegir un objeto del inventario
        /// </summary>
        /// <param name="objetos">Objetos del inventario</param>
        /// <returns>Objeto seleccionado</returns>
        public static AbstractObjeto ElegirObjeto(AbstractObjeto[] objetos)
        {
            if (objetos[0] == null)
            {
                MostrarMensaje("Tu inventario está vacío");
                return null;
            }

            return Elegir("Elige un objeto:", true, objetos);
        }

        /// <summary>
        /// Muestra el tablero del juego actualizado
        /// </summary>
        /// <param name="tablero">Tablero</param>
        /// <param name="jugadorX">Coordenada X del jugador</param>
        /// <param name="jugadorY">Coordenada Y del jugador</param>
        public static void MostrarTablero(AbstractSala[,] tablero, int jugadorX, int jugadorY)
        {
            Clear();
            iniciarMigas("\n---------------------------------", false);
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

                    AbstractSala casilla = tablero[i, j];
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
                anadirMiga(linea, false);
                if (i < 7)
                    anadirMiga("|---+---+---+---+---+---+---+---|", false);
            }

            anadirMiga("---------------------------------", true);
        }

        /// <summary>
        /// Muestra por pantalla que un <see cref="AbstractMob">mob</see> ha esquivado el ataque de otro
        /// </summary>
        /// <param name="atacante"><see cref="AbstractMob">Mob</see> que realiza el ataque</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> esquiva el ataque</param>
        public static void MostrarEsquivar(AbstractMob atacante, AbstractMob victima)
        {
            Console.WriteLine($"{victima.GetNombre()} esquivó el ataque de {atacante.GetNombre()}");
        }

        /// <summary>
        /// Muestra un mensaje al jugador y espera a que pulse Enter para continuar con la ejecución
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar</param>
        public static void Esperar(string mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje))
                Console.WriteLine(mensaje);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Hace esperar al jugador hasta que pulse enter
        /// </summary>
        public static void Esperar()
        {
            Esperar("...");
        }

        /// <summary>
        /// Muestra el menú de acciones del tablero
        /// </summary>
        /// <returns></returns>
        public static int MenuAcciones()
        {
            do
            {
                Clear();
                Console.WriteLine(migas);
                Console.WriteLine("Esc) Menú");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.W:
                        return MOVER_ARRIBA;

                    case ConsoleKey.S:
                        return MOVER_ABAJO;

                    case ConsoleKey.D:
                        return MOVER_DERECHA;

                    case ConsoleKey.A:
                        return MOVER_IZQUIERDA;

                    case ConsoleKey.Escape:
                        return ABRIR_MENU;
                }
            } while (true);
        }

        /// <summary>
        /// Inicializa las migas y las muestra
        /// </summary>
        /// <param name="miga">miga nueva</param>
        /// <param name="mostrar">true para mostrarlas</param>
        private static void iniciarMigas(string miga, bool mostrar)
        {
            migas = miga + "\n";

            if (mostrar)
                MostrarMigas();
        }

        /// <summary>
        /// Añade una miga y las muestra
        /// </summary>
        /// <param name="miga">miga a anadir</param>
        /// <param name="mostrar">true para mostrar las migas</param>
        private static void anadirMiga(string miga, bool mostrar)
        {
            migas += miga + "\n";
            if (mostrar)
                MostrarMigas();
        }

        /// <summary>
        /// Muestra las <see cref="migas"/>
        /// </summary>
        public static void MostrarMigas()
        {
            Clear();
            Console.WriteLine(migas);
        }

        /// <summary>
        /// Muestra la pantalla de victoria cuando el jugador derrota al enemigo
        /// </summary>
        /// <param name="jugador"></param>
        /// <param name="enemigo"></param>
        public static void MostrarVictoria(AbstractJugador jugador, AbstractEnemigo enemigo)
        {
            Esperar();
            anadirMiga("\n¡Victoria!", false);
            AbstractObjeto drop = enemigo.Drop();
            if (drop != null)
                anadirMiga($"¡Obtuviste {drop.GetNombre()} x {drop.GetCantidad()}!\n", true);

            if (!jugador.AnadirObjeto(drop))
            {
                anadirMiga("Tu inventario está lleno, elimina un objeto para ganar espacio", true);
                AbstractObjeto objeto = ElegirObjeto(jugador.GetObjetos(false));
                if (objeto != null)
                {
                    jugador.EliminarObjeto(objeto);
                    jugador.AnadirObjeto(drop);
                }
            }
            Console.WriteLine($"¡Obtuviste {enemigo.GetExp()} puntos de experiencia!");
            Esperar("\nPulsa Enter para continuar");
            if (jugador.SubirNivel(enemigo.GetExp()))
                mostrarNivelSubido(jugador);
        }

        /// <summary>
        /// Indica al jugador que ha subido de nivel y muestra la subida de stats
        /// </summary>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que ha subido de nivel</param>
        private static void mostrarNivelSubido(AbstractJugador jugador)
        {
            Clear();
            Console.WriteLine($"¡Has subido al nivel {jugador.GetNivel()}!");
            int[,] stats = jugador.GetStatsNuevos();
            string texto = "";
            for (int i = 0; i < 11; i++)
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
                        linea += "Hab       ";
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
            Esperar("\nPulsa Enter para seguir");
        }

        /// <summary>
        /// Indica al jugador que ha obtenido un <see cref="AbstractObjeto">objeto</see>
        /// </summary>
        /// <param name="objeto"><see cref="AbstractObjeto">Objeto</see> obtenido</param>
        public static void MostrarObjetoConseguido(AbstractObjeto objeto)
        {
            Console.WriteLine($"¡Obtuviste {objeto.GetNombre()}!");
            Esperar("\nPulsa Enter para continuar");
        }

        /// <summary>
        /// Indica al jugador que ha obtenido una <see cref="AbstractHabilidad">habilidad</see>
        /// </summary>
        /// <param name="habilidad"><see cref="AbstractHabilidad">Habilidad</see> obtenida</param>
        public static void MostrarHabilidadObtenida(AbstractHabilidad habilidad)
        {
            Console.WriteLine($"¡Obtuviste {habilidad.GetNombre()}!");
            Esperar("\nPulsa Enter para continuar");
        }

        /// <summary>
        /// Indica al jugador que ha obtenido un <see cref="AbstractArma">arma</see>
        /// </summary>
        /// <param name="arma"><see cref="AbstractArma">Arma</see> obtenida</param>
        public static void MostrarArmaConseguida(AbstractArma arma)
        {
            Console.WriteLine($"¡Obtuviste {arma.GetNombre()}!");
            Esperar("\nPulsa Enter para continuar");
        }

        /// <summary>
        /// Indica al jugador que su personaje no puede usar un arma
        /// </summary>
        public static void MostrarNoEquiparArma()
        {
            Console.WriteLine("Tu personaje no puede usar esta arma");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Indica al jugador que ya posee una <see cref="AbstractHabilidad">habilidad</see>
        /// </summary>
        /// <param name="habilidad"><see cref="AbstractHabilidad">Habilidad</see> que ya posee</param>
        public static void MostrarHabilidadEquipada(AbstractHabilidad habilidad)
        {
            Console.WriteLine($"Ya posees la habilidad {habilidad.GetNombre()}");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// uestra los stats de un <see cref="AbstractJugador">jugador</see>
        /// </summary>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> cuyos stats se mostrarán</param>
        public static void MostrarStats(AbstractJugador jugador)
        {
            Clear();
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
                        linea += "Hab       ";
                        break;

                    case 6:
                        linea += "Def       ";
                        break;

                    case 7:
                        linea += "Res       ";
                        break;

                    case 8:
                        linea += "% Crítico ";
                        break;

                    case 9:
                        linea += "Daño Crit ";
                        break;

                    case 10:
                        linea += "Exp       ";
                        break;

                    case 11:
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
            Esperar("\nPulsa Enter para volver");
        }

        /// <summary>
        /// Muestra los detalles de un <see cref="AbstractArma">arma</see>
        /// </summary>
        /// <param name="arma"><see cref="AbstractArma">Arma</see> a mostrar</param>
        public static void MostrarArma(AbstractArma arma)
        {
            MostrarMigas();
            Console.WriteLine($"{arma.GetNombre()}:");
            Console.WriteLine(arma.GetDescripcion());
            Console.WriteLine($"\nDaño: {arma.GetDano()}");
            Console.WriteLine($"Usos: {arma.GetUsos()}/{arma.GetUsosMaximos()}");

            AbstractHabilidad habilidad = arma.GetHabilidad();
            Console.WriteLine($"\nHabilidad: {habilidad.GetNombre()}");
            Console.WriteLine(habilidad.GetDescripcion());
            Esperar("\nPulsa Enter para continuar\n");
        }

        /// <summary>
        /// Pide al jugador que eliga unas de sus habilidades.
        /// <br/>
        /// Si el jugador no posee ninguna habilidad, se informará de ello
        /// </summary>
        /// <param name="habilidades">Lista de las <see cref="AbstractHabilidad">habilidades</see> del jugador</param>
        /// <returns>habilidad seleccionada, null si no elige ninguna o si no posee ninguna</returns>
        public static AbstractHabilidad ElegirHabilidad(List<AbstractHabilidad> habilidades)
        {
            if (habilidades.Count == 0)
            {
                Console.WriteLine("No tienes ninguna habilidad");
                return null;
            }

            return Elegir("Elige una habilidad:", true, habilidades);
        }

        /// <summary>
        /// Muestra los detalles de una <see cref="AbstractHabilidad">habilidad</see>
        /// </summary>
        /// <param name="habilidad"><see cref="AbstractHabilidad">Habilidad</see> a mostrar</param>
        public static void MostrarHabilidad(AbstractHabilidad habilidad)
        {
            MostrarMigas();
            Console.WriteLine(habilidad.GetNombre());
            Console.WriteLine(habilidad.GetDescripcion());
            Esperar("\nPulsa Enter para continuar");
        }

        /// <summary>
        /// Muestra los detalles de un objeto y le pregunta si lo quiere usar
        /// </summary>
        /// <param name="objeto"><see cref="AbstractObjeto">Objeto</see> a usar</param>
        /// <param name="jugador"><see cref="AbstractJugador">Jugador</see> que usará el objeto</param>
        /// <param name="enemigo"><see cref="AbstractEnemigo">Enemigo</see> que sufrirá la acción del objeto</param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el jugador</param>
        public static void MostrarUsarObjeto(AbstractObjeto objeto, AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            anadirMiga("\n" + objeto.ToString() + "\n" + objeto.GetDescripcion(), false);

            if (PreguntarSiNo("¿Quieres usar este objeto?"))
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
            Esperar("\nPulsa Enter para continuar");
        }

        /// <summary>
        /// Informa al jugador de que ha encontrado la sala del jefe
        /// </summary>
        public static void DescubrirJefe()
        {
            anadirMiga("Has encontrado la sala del Jefe. Se necesita la Llave del Jefe para abrirla", true);
            Thread.Sleep(100);
        }

        /// <summary>
        /// Pregunta al jugador el nombre de su personaje.
        /// <br/>
        /// El nombre no puede estar vacío ni superar los 20 caracteres
        /// </summary>
        /// <returns>Nombre del personaje</returns>
        public static string PedirNombre()
        {
            Clear();
            Console.WriteLine("¡Bienvenido!\nEscribe tu nombre:");
            string nombre = "";
            do
            {
                nombre = Console.ReadLine().Trim();
                if (nombre.Length > 20)
                    Console.WriteLine("El nombre no puede ser superior a 20 caracteres");
            } while (string.IsNullOrEmpty(nombre) || nombre.Length > 20);
            return nombre.Trim();
        }

        /// <summary>
        /// Pinta una línea vacía
        /// </summary>
        public static void NuevaLinea()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Limpia la consola
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Pregunta al jugador cuántos niveles desea enfrentar
        /// </summary>
        /// <returns>Número de niveles que el jugador deberá enfrentar</returns>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si se elige un nivel que no existe</exception>
        public static int PreguntarNiveles()
        {
            string nivel = Elegir("Selecciona el número de pisos que deseas enfrentar", false, UN_NIVEL, DOS_NIVELES, TRES_NIVELES, CUATRO_NIVELES, CINCO_NIVELES);
            switch (nivel)
            {
                case UN_NIVEL:
                    return 1;

                case DOS_NIVELES:
                    return 2;

                case TRES_NIVELES:
                    return 3;

                case CUATRO_NIVELES:
                    return 4;

                case CINCO_NIVELES:
                    return 5;

                default:
                    throw new ArgumentOutOfRangeException("Se ha elegido un nivel que no existe");
            }
        }
    }
}
