using System;
using System.Threading;

using SquareDungeon.Salas;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Modelo.EntradaSalida;

namespace SquareDungeon.Modelo
{
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

        private AbstractSala[,] tablero;

        private AbstractJugador jugador;

        private Fabrica fabrica;

        private int jugadorX;
        private int jugadorY;
        private int resultado;
        private int nivelActual;
        private int nivelMax;

        public Partida()
        {
            nivelActual = 0;

            nivelMax = EntradaSalida.PreguntarNiveles();
            EntradaSalida.IndicarAvanzarDialogos();

            fabrica = new Fabrica();
            jugador = fabrica.GetJugador();
        }

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

        private void jugar()
        {
            do
            {
                MostrarTablero(tablero, jugadorX, jugadorY);
                int eleccion = MenuAcciones();
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
                        int menu = MostrarMenu(jugador);
                        switch (menu)
                        {
                            case MENU_STATS:
                                MostrarStats(jugador);
                                break;

                            case MENU_ARMAS:
                                AbstractArma arma = ElegirArma(jugador.GetArmas());
                                if (arma != null)
                                    MostrarArma(arma);

                                break;

                            case MENU_HABILIDADES:
                                AbstractHabilidad habilidad = ElegirHabilidad(jugador.GetHabilidades());
                                if (habilidad != null)
                                    MostrarHabilidad(habilidad);

                                break;

                            case MENU_OBJETOS:
                                AbstractObjeto objeto = ElegirObjeto(jugador.GetObjetos());
                                if (objeto != null)
                                    MostrarUsarObjeto(objeto, jugador, null, null);
                                if (jugador.GetObjetos()[0] == null)
                                    Thread.Sleep(1000);

                                break;
                        }
                        break;
                }
            } while (resultado == RESULTADO_EN_JUEGO);
        }

        public void SetResultado(int resultado)
        {
            if (resultado < 0 || resultado > 3)
                throw new ArgumentOutOfRangeException("resultado",
                    "EL resultado es incorrecto, utiliza las constantes de clase");

            this.resultado = resultado;
        }

        public int GetNivelPiso() => nivelActual * DIFERENCIA_NIVEL;

        public void SetPosicionJugador(int x, int y)
        {
            jugadorX = x;
            jugadorY = y;
        }

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
                EntradaSalida.Clear();
                MostrarPV(enemigo, pvInicialesEnemigo, enemigo.GetStat(AbstractMob.INDICE_VIDA));
                MostrarPV(jugador, pvInicialesJugador, jugador.GetStat(AbstractMob.INDICE_VIDA));

                // Se elige entre utilizar un objeto, atacar con un arma o huir del combate
                int eleccion = ElegirArmaObjeto();

                if (eleccion == ELEGIR_HUIR)
                {
                    resultado = RESULTADO_HUIR;
                    break;
                }

                if (eleccion == ELEGIR_OBJETO)
                {
                    AbstractObjeto objeto = ElegirObjeto(jugador.GetObjetos());
                    if (objeto == null)
                        eleccion = ELEGIR_ARMA;
                    else
                        objeto.RealizarAccion(jugador, enemigo, sala);
                }

                if (eleccion == ELEGIR_ARMA)
                {
                    // Se elige el arma con la que se va a atacar
                    AbstractArma arma = ElegirArma(jugador.GetArmas());
                    if (arma == null)
                        continue;

                    jugador.SetArmaCombate(arma);

                    ejecutorJugador.EjecutarPreAtaque();
                    int res = ataqueJugador(jugador, enemigo, ejecutorJugador, ejecutorEnemigo);
                    if (res == RESULTADO_JUGADOR_GANA)
                    {
                        resultado = res;
                        break;
                    }
                    ejecutorJugador.EjecutarPostAtaque();
                }

                // Ataca el enemigo
                ejecutorEnemigo.EjecutarPreAtaque();
                if (atacarEnemigo(enemigo, jugador, ejecutorEnemigo, ejecutorJugador))
                {
                    resultado = RESULTADO_ENEMIGO_GANA;
                    break;
                }

                ejecutorEnemigo.EjecutarPostAtaque();

                Thread.Sleep(1000);

                int velJugador = jugador.GetStatCombate(AbstractMob.INDICE_AGILIDAD);
                int velEnemigo = enemigo.GetStatCombate(AbstractMob.INDICE_AGILIDAD);

                // Ataque doble del jugador
                if (velJugador - velEnemigo > 4 && eleccion == ELEGIR_ARMA)
                {
                    ejecutorJugador.EjecutarPreAtaque();
                    int res = ataqueJugador(jugador, enemigo, ejecutorJugador, ejecutorEnemigo);
                    if (res == RESULTADO_JUGADOR_GANA)
                    {
                        resultado = res;
                        break;
                    }
                    ejecutorJugador.EjecutarPostAtaque();
                }

                // Ataque doble del enemigo
                if (velEnemigo - velJugador > 4)
                {
                    ejecutorEnemigo.EjecutarPreAtaque();
                    if (atacarEnemigo(enemigo, jugador, ejecutorEnemigo, ejecutorJugador))
                    {
                        resultado = RESULTADO_ENEMIGO_GANA;
                        break;
                    }

                    ejecutorEnemigo.EjecutarPostAtaque();
                }

                EntradaSalida.Esperar();
            } while (true);

            jugador.ReiniciarStatsCombate();
            ejecutorJugador.EjecutarPostCombate();
            ejecutorEnemigo.EjecutarPostCombate();

            ejecutorJugador.ResetearHabilidades();
            ejecutorEnemigo.ResetearHabilidades();

            return resultado;
        }

        private int ataqueJugador(AbstractJugador jugador, AbstractEnemigo enemigo, EjecutorHabilidades ejecutorJugador, EjecutorHabilidades ejecutorEnemigo)
        {
            int dano = ejecutorJugador.EjecutarAtaque();

            if (dano == AbstractHabilidad.RESULTADO_SIN_ACTIVAR)
            {
                AbstractArma armaCombate = jugador.GetArmaCombate();
                EntradaSalida.MostrarAtaque(jugador, armaCombate);
                dano = jugador.Atacar(enemigo);
            }

            dano = ejecutorEnemigo.EjecutarAtaqueRival(dano);

            if (enemigo.Esquivar(jugador))
            {
                dano = 0;
                EntradaSalida.MostrarEsquivar(enemigo, jugador);
            }
            else
            {
                EntradaSalida.MostrarDano(jugador, enemigo, dano);
            }

            return enemigo.Danar(dano) ? RESULTADO_JUGADOR_GANA : RESULTADO_EN_JUEGO;
        }

        private bool atacarEnemigo(AbstractEnemigo enemigo, AbstractJugador jugador, EjecutorHabilidades ejecutorEnemigo, EjecutorHabilidades ejecutorJugador)
        {
            int dano = ejecutorEnemigo.EjecutarAtaque();
            if (dano == AbstractHabilidad.RESULTADO_SIN_ACTIVAR)
            {
                EntradaSalida.MostrarAtaque(enemigo);
                dano = enemigo.Atacar(jugador);
            }
            dano = ejecutorJugador.EjecutarAtaqueRival(dano);

            if (jugador.Esquivar(enemigo))
            {
                dano = 0;
                EntradaSalida.MostrarEsquivar(enemigo, jugador);
            }
            else
            {
                EntradaSalida.MostrarDano(enemigo, jugador, dano);
            }

            return jugador.Danar(dano);
        }

        private void entrarEnSala(AbstractSala sala, AbstractJugador jugador)
        {
            if (sala.AbrirSala(jugador))
                sala.Entrar(this, jugador);
        }

        private void iniciarNivel()
        {
            tablero = fabrica.GenerarTablero();
            jugadorX = 7;
            jugadorY = 0;
            resultado = RESULTADO_EN_JUEGO;
        }
    }
}
