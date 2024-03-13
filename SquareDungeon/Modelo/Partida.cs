using System;
using System.Threading;

using SquareDungeon.Salas;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Modelo
{
    /// <summary>
    /// Clase con los datos y la lógica de la partida
    /// </summary>
    class Partida
    {
        public const int RESULTADO_JUGADOR_GANA = 0;
        public const int RESULTADO_ENEMIGO_GANA = 1;
        public const int RESULTADO_JEFE_ELIMINADO = 2;
        public const int RESULTADO_EN_JUEGO = 3;
        public const int RESULTADO_HUIR = 4;

        public const int MOVER_ARRIBA = 10;
        public const int MOVER_ABAJO = 11;
        public const int MOVER_DERECHA = 12;
        public const int MOVER_IZQUIERDA = 13;
        public const int ABRIR_MENU = 14;

        private const int DIFERENCIA_NIVEL = 10;

        private static Partida partida;

        /// <summary>
        /// Tablero de juego
        /// </summary>
        private AbstractSala[,] tablero;

        /// <summary>
        /// Personaje del jugador
        /// </summary>
        private AbstractJugador jugador;

        /// <summary>
        /// Fábrica de objetos
        /// </summary>
        private Fabrica fabrica;

        /// <summary>
        /// Coordenada X del jugador
        /// </summary>
        private int jugadorX;
        /// <summary>
        /// Coordenada Y del jugador
        /// </summary>
        private int jugadorY;
        /// <summary>
        /// Resultado de la partida
        /// </summary>
        private int resultado;
        /// <summary>
        /// Piso en el que se encuentra el jugador.
        /// </summary>
        private int nivelActual;
        /// <summary>
        /// Número de pisos necesarios para ganar la partida
        /// </summary>
        private int nivelMax;

        /// <summary>
        /// Devuelve la instancia de la partida. Si no existe, genera una nueva instancia
        /// </summary>
        /// <returns>Instancia de la clase</returns>
        public static Partida GetInstance()
        {
            if (partida == null)
                partida = new Partida();

            return partida;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        private Partida()
        {
            nivelActual = 0;

            nivelMax = EntradaSalida.PreguntarNiveles();
            EntradaSalida.Clear();
            EntradaSalida.Esperar("Avanza con w, a, s, d\nPulsa Enter para continuar\n...");

            fabrica = new Fabrica();
            jugador = fabrica.GetJugador();
        }

        /// <summary>
        /// Inicia la partida desde el nivel actual hasta el nivel máximo
        /// </summary>
        /// <returns><see cref="resultado"/> de la partida</returns>
        public int JugarNiveles()
        {
            while (nivelActual < nivelMax)
            {
                iniciarNivel();
                jugar();
                if (resultado == RESULTADO_ENEMIGO_GANA)
                    return resultado;

                nivelActual++;
            }

            return resultado;
        }

        /// <summary>
        /// Contiene toda la lógica de un nivel del juego
        /// </summary>
        private void jugar()
        {
            do
            {
                EntradaSalida.MostrarTablero(tablero, jugadorX, jugadorY);
                int eleccion = EntradaSalida.MenuAcciones();
                switch (eleccion)
                {
                    case MOVER_ARRIBA:
                        if (jugadorX - 1 >= 0)
                        {
                            AbstractSala sala = tablero[jugadorX - 1, jugadorY];
                            entrarEnSala(sala, jugador);
                        }
                        break;

                    case MOVER_ABAJO:
                        if (jugadorX + 1 < 8)
                        {
                            AbstractSala sala = tablero[jugadorX + 1, jugadorY];
                            entrarEnSala(sala, jugador);
                        }
                        break;

                    case MOVER_DERECHA:
                        if (jugadorY + 1 < 8)
                        {
                            AbstractSala sala = tablero[jugadorX, jugadorY + 1];
                            entrarEnSala(sala, jugador);
                        }
                        break;

                    case MOVER_IZQUIERDA:
                        if (jugadorY - 1 >= 0)
                        {
                            AbstractSala sala = tablero[jugadorX, jugadorY - 1];
                            entrarEnSala(sala, jugador);
                        }
                        break;

                    case ABRIR_MENU:
                        EntradaSalida.MostrarTablero(tablero, jugadorX, jugadorY);
                        string menu = EntradaSalida.Elegir("", true, EntradaSalida.MENU_STATS, EntradaSalida.MENU_ARMAS, EntradaSalida.MENU_OBJETOS, EntradaSalida.MENU_HABILIDADES);
                        switch (menu)
                        {
                            case EntradaSalida.MENU_STATS:
                                EntradaSalida.MostrarStats(jugador);
                                break;

                            case EntradaSalida.MENU_ARMAS:
                                AbstractArma arma = EntradaSalida.ElegirArma(jugador.GetArmas());
                                if (arma != null)
                                    EntradaSalida.MostrarArma(arma);

                                break;

                            case EntradaSalida.MENU_HABILIDADES:
                                AbstractHabilidad habilidad = EntradaSalida.ElegirHabilidad(jugador.GetHabilidades());
                                if (habilidad != null)
                                    EntradaSalida.MostrarHabilidad(habilidad);

                                break;

                            case EntradaSalida.MENU_OBJETOS:
                                AbstractObjeto objeto = EntradaSalida.ElegirObjeto(jugador.GetObjetos(true));
                                if (objeto != null)
                                    EntradaSalida.MostrarUsarObjeto(objeto, jugador, null, null);

                                break;
                        }
                        break;
                }
            } while (resultado == RESULTADO_EN_JUEGO);
        }

        /// <summary>
        /// Establece el resultado de la partida
        /// </summary>
        /// <param name="resultado"><see cref="resultado"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si se establece un resultado erróneo</exception>
        public void SetResultado(int resultado)
        {
            if (resultado < 0 || resultado > 3)
                throw new ArgumentOutOfRangeException("resultado",
                    "EL resultado es incorrecto, utiliza las constantes de clase");

            this.resultado = resultado;
        }

        /// <summary>
        /// Devuelve el nivel mínimo de los enemigos en función del piso en el que se encuentren
        /// </summary>
        /// <returns>nivel mínimo de los enemigos</returns>
        public int GetNivelPiso() => nivelActual * DIFERENCIA_NIVEL;

        /// <summary>
        /// Establece la posición del jugador en el tablero
        /// </summary>
        /// <param name="x">Coordenada X del jugador</param>
        /// <param name="y">Coordenada Y del jugador</param>
        public void SetPosicionJugador(int x, int y)
        {
            jugadorX = x;
            jugadorY = y;
        }

        /// <summary>
        /// Contiene toda la lógica del combate
        /// </summary>
        /// <param name="jugador">Personaje del jugador</param>
        /// <param name="enemigo"><see cref="AbstractEnemigo">Enemigo</see> al que se enfrenta el jugador</param>
        /// <param name="sala"><see cref="SalaEnemigo">Sala</see> en la que se libra el combate</param>
        /// <returns>resultado del combate</returns>
        public int Combatir(AbstractJugador jugador, AbstractEnemigo enemigo, AbstractSala sala)
        {
            int resultado;

            EjecutorHabilidades ejecutorJugador = new EjecutorHabilidades(jugador, enemigo, sala, jugador.GetHabilidades());
            EjecutorHabilidades ejecutorEnemigo = new EjecutorHabilidades(enemigo, jugador, sala, enemigo.GetHabilidades());

            EntradaSalida.Clear();
            // Se ejecutan todas las habilidades pre combate
            bool ejecutaJugador = ejecutorJugador.EjecutarPreCombate();
            Thread.Sleep(500);

            EntradaSalida.NuevaLinea();
            bool ejecutaEnemigo = ejecutorEnemigo.EjecutarPreCombate();
            Thread.Sleep(500);

            if (ejecutaJugador || ejecutaEnemigo)
                EntradaSalida.Esperar();

            // Se obtienen los PV iniciales para dibujar las barras de vida
            int pvInicialesEnemigo = enemigo.GetStat(AbstractMob.INDICE_VIDA_TOTAL);
            int pvInicialesJugador = jugador.GetStat(AbstractMob.INDICE_VIDA_TOTAL);

            do
            {
                // Se muestran las barras de viada del enemigo y del jugador
                int pvActualesEnemigo = enemigo.GetStat(AbstractMob.INDICE_VIDA);
                int pvActualesJugador = jugador.GetStat(AbstractMob.INDICE_VIDA);
                EntradaSalida.MostrarBarrasPV(enemigo, pvInicialesEnemigo, pvActualesEnemigo, jugador, pvInicialesJugador, pvActualesJugador);

                // Se elige entre utilizar un objeto, atacar con un arma o huir del combate
                string eleccion = EntradaSalida.Elegir("", false, EntradaSalida.ELEGIR_ARMA, EntradaSalida.ELEGIR_OBJETO, EntradaSalida.ELEGIR_HUIR);

                if (EntradaSalida.ELEGIR_HUIR.Equals(eleccion))
                {
                    resultado = RESULTADO_HUIR;
                    break;
                }

                EntradaSalida.MostrarMigas();

                if (EntradaSalida.ELEGIR_OBJETO.Equals(eleccion))
                {
                    AbstractObjeto objeto = EntradaSalida.ElegirObjeto(jugador.GetObjetos(false));
                    if (objeto == null)
                        eleccion = EntradaSalida.ELEGIR_ARMA;
                    else
                        EntradaSalida.MostrarUsarObjeto(objeto, jugador, enemigo, sala);
                }

                if (EntradaSalida.ELEGIR_ARMA.Equals(eleccion))
                {
                    // Se elige el arma con la que se va a atacar
                    AbstractArma arma = EntradaSalida.ElegirArma(jugador.GetArmas());
                    if (arma == null)
                        continue;

                    jugador.SetArmaCombate(arma);

                    ejecutorJugador.EjecutarPreAtaque();
                    if (atacar(jugador, enemigo, ejecutorJugador, ejecutorEnemigo))
                    {
                        resultado = RESULTADO_JUGADOR_GANA;
                        break;
                    }
                    ejecutorJugador.EjecutarPostAtaque();
                }

                // Ataca el enemigo
                ejecutorEnemigo.EjecutarPreAtaque();
                if (atacar(enemigo, jugador, ejecutorEnemigo, ejecutorJugador))
                {
                    resultado = RESULTADO_ENEMIGO_GANA;
                    break;
                }

                ejecutorEnemigo.EjecutarPostAtaque();

                int velJugador = jugador.GetStatCombate(AbstractMob.INDICE_AGILIDAD);
                int velEnemigo = enemigo.GetStatCombate(AbstractMob.INDICE_AGILIDAD);

                // Ataque doble del jugador
                if (velJugador - velEnemigo > 4 && eleccion == EntradaSalida.ELEGIR_ARMA)
                {
                    Thread.Sleep(500);
                    ejecutorJugador.EjecutarPreAtaque();
                    if (atacar(jugador, enemigo, ejecutorJugador, ejecutorEnemigo))
                    {
                        resultado = RESULTADO_JUGADOR_GANA;
                        break;
                    }
                    ejecutorJugador.EjecutarPostAtaque();
                }

                // Ataque doble del enemigo
                if (velEnemigo - velJugador > 4)
                {
                    Thread.Sleep(500);
                    ejecutorEnemigo.EjecutarPreAtaque();
                    if (atacar(enemigo, jugador, ejecutorEnemigo, ejecutorJugador))
                    {
                        resultado = RESULTADO_ENEMIGO_GANA;
                        break;
                    }
                    ejecutorEnemigo.EjecutarPostAtaque();
                }
                jugador.AplicarEfectos();
                enemigo.AplicarEfectos();
                EntradaSalida.Esperar();
            } while (true);

            jugador.ReiniciarStatsCombate();
            ejecutorJugador.EjecutarPostCombate();
            ejecutorEnemigo.EjecutarPostCombate();

            ejecutorJugador.ResetearHabilidades();
            ejecutorEnemigo.ResetearHabilidades();

            enemigo.EliminarEfectos();
            jugador.EliminarEfectos();

            jugador.ResetearHabilidadesArmas();

            return resultado;
        }

        /// <summary>
        /// Contiene toda la lógica empleada en el ataque de un <see cref="AbstractMob">atacante</see> a una <see cref="AbstractMob">víctima</see>/>
        /// </summary>
        /// <param name="atacante"><see cref="AbstractMob">Mob</see> que realiza el ataque</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque</param>
        /// <param name="ejecutorAtacante"><see cref="EjecutorHabilidades">Ejecutor de habilidades</see> del atacante</param>
        /// <param name="ejecutorVictima"><see cref="EjecutorHabilidades">Ejecutor de habilidades</see> de la víctima</param>
        /// <returns>true si el jugador ha sido derrotado, false en caso contrario</returns>
        private bool atacar(AbstractMob atacante, AbstractMob victima, EjecutorHabilidades ejecutorAtacante, EjecutorHabilidades ejecutorVictima)
        {
            int dano = ejecutorAtacante.EjecutarAtaque();
            if (dano == AbstractHabilidad.RESULTADO_SIN_ACTIVAR)
            {
                dano = getDano(atacante, victima);
            }
            dano = ejecutorVictima.EjecutarAtaqueRival(dano);

            if (victima.Esquivar(atacante))
            {
                dano = 0;
                EntradaSalida.MostrarEsquivar(atacante, victima);
            }
            else
            {
                EntradaSalida.MostrarDano(atacante, victima, dano);
            }

            return victima.Danar(dano);
        }

        /// <summary>
        /// Ejecuta un ataque de un mob a otro
        /// </summary>
        /// <param name="atacante"><see cref="AbstractMob">Mob</see> atacante</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque</param>
        /// <returns>Daño que inflige el atacante a la víctima</returns>
        private int getDano(AbstractMob atacante, AbstractMob victima)
        {
            int dano = 0;

            if (atacante is AbstractJugador)
            {
                AbstractJugador jugador = (AbstractJugador)atacante;
                AbstractEnemigo enemigo = (AbstractEnemigo)victima;

                AbstractArma armaCombate = jugador.GetArmaCombate();
                EntradaSalida.MostrarAtaque(jugador, armaCombate);
                dano = jugador.Atacar(enemigo);

            }
            else if (atacante is AbstractEnemigo)
            {
                AbstractJugador jugador = (AbstractJugador)victima;
                AbstractEnemigo enemigo = (AbstractEnemigo)atacante;

                EntradaSalida.MostrarAtaque(enemigo);
                dano = enemigo.Atacar(jugador);
            }

            return dano;
        }

        /// <summary>
        /// Realiza la lógica necesaria cuando un jugador entra en una sala
        /// </summary>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que entra el jugador</param>
        /// <param name="jugador"><see cref="jugador"/></param>
        private void entrarEnSala(AbstractSala sala, AbstractJugador jugador)
        {
            if (sala.AbrirSala(jugador))
                sala.Entrar(jugador);
        }

        /// <summary>
        /// Establece los valores iniciales al acceder a un nivel
        /// </summary>
        private void iniciarNivel()
        {
            tablero = fabrica.GenerarTablero();
            jugadorX = 7;
            jugadorY = 0;
            resultado = RESULTADO_EN_JUEGO;
        }
    }
}
