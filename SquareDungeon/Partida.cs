using System;
using System.Threading;
using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.EntradaSalida;

namespace SquareDungeon
{
    class Partida
    {
        public const int RESULTADO_JUGADOR_GANA = 0;
        public const int RESULTADO_ENEMIGO_GANA = 1;
        public const int RESULTADO_JEFE_ELIMINADO = 2;
        public const int RESULTADO_EN_JUEGO = 3;

        public const int MOVER_ARRIBA = 10;
        public const int MOVER_ABAJO = 11;
        public const int MOVER_DERECHA = 12;
        public const int MOVER_IZQUIERDA = 13;
        public const int ABRIR_MENU = 14;

        private Sala[,] tablero;

        private Jugador jugador;

        private int jugadorX;
        private int jugadorY;
        private int resultado;

        public Partida()
        {
            jugadorX = 7;
            jugadorY = 0;
            resultado = RESULTADO_EN_JUEGO;

            Fabrica fabrica = new Fabrica();
            jugador = fabrica.GetJugador();
            tablero = fabrica.GenerarTablero();
        }

        public void Jugar()
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
                            jugadorX--;
                            Sala sala = tablero[jugadorX, jugadorY];
                            if (sala.GetType() == typeof(SalaJefe))
                                if (!((SalaJefe)sala).AbrirSala(jugador))
                                    jugadorX++;

                            sala.Entrar(this, jugador);
                        }
                        break;

                    case MOVER_ABAJO:
                        if (jugadorX + 1 < 8)
                        {
                            jugadorX++;
                            Sala sala = tablero[jugadorX, jugadorY];
                            if (sala.GetType() == typeof(SalaJefe))
                                if (!((SalaJefe)sala).AbrirSala(jugador))
                                    jugadorX--;

                            sala.Entrar(this, jugador);
                        }
                        break;

                    case MOVER_DERECHA:
                        if (jugadorY + 1 < 8)
                        {
                            jugadorY++;
                            Sala sala = tablero[jugadorX, jugadorY];
                            if (sala.GetType() == typeof(SalaJefe))
                                if (!((SalaJefe)sala).AbrirSala(jugador))
                                    jugadorY--;

                            sala.Entrar(this, jugador);
                        }
                        break;

                    case MOVER_IZQUIERDA:
                        if (jugadorY - 1 >= 0)
                        {
                            jugadorY--;
                            Sala sala = tablero[jugadorX, jugadorY];
                            if (sala.GetType() == typeof(SalaJefe))
                                if (!((SalaJefe)sala).AbrirSala(jugador))
                                    jugadorY++;

                            sala.Entrar(this, jugador);
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
                                Arma arma = ElegirArma(jugador.GetArmas());
                                if (arma != null)
                                    MostrarArma(arma);

                                break;

                            case MENU_HABILIDADES:
                                Habilidad habilidad = ElegirHabilidad(jugador.GetHabilidades());
                                if (habilidad != null)
                                    MostrarHabilidad(habilidad);

                                break;

                            case MENU_OBJETOS:
                                Objeto objeto = ElegirObjeto(jugador.GetObjetos());
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

        public int GetResultado() => resultado;

        public void SetPosicionJugador(int x, int y)
        {
            jugadorX = x;
            jugadorY = y;
        }

        public int Combatir(Jugador jugador, Enemigo enemigo, Sala sala)
        {
            // Se ejecutan todas las habilidades pre combate
            List<Habilidad> habilidadesPreCombate = jugador.GetHabilidadesPorTipo(Habilidad.TIPO_PRE_COMBATE);
            foreach (Habilidad habilidad in habilidadesPreCombate)
            {
                if (habilidad.Ejecutar())
                {
                    MostrarHabilidad(jugador, habilidad);
                    habilidad.RealizarAccion(jugador, enemigo);
                }
            }

            Thread.Sleep(1000);

            // Se obtienen los PV iniciales para dibujar las barras de vida
            int pvInicialesEnemigo = enemigo.GetStat(Mob.INDICE_VIDA);
            int pvInicialesJugador = jugador.GetStat(Mob.INDICE_VIDA_TOTAL);

            List<Habilidad> habilidadesCombateEjecutadas = new List<Habilidad>();
            do
            {
                // Se muestran las barras de viada del enemigo y del jugador
                Console.Clear();
                MostrarPV(enemigo, pvInicialesEnemigo, enemigo.GetStat(Mob.INDICE_VIDA));
                MostrarPV(jugador, pvInicialesJugador, jugador.GetStat(Mob.INDICE_VIDA));

                // Se ejecutan todas las habilidades de combate que no se hayan ejecutado antes
                List<Habilidad> habilidadesCombate = jugador.GetHabilidadesPorTipo(Habilidad.TIPO_COMBATE);
                foreach (Habilidad hab in habilidadesCombate)
                {
                    if (!habilidadesCombateEjecutadas.Contains(hab))
                        if (hab.Ejecutar())
                            if (hab.RealizarAccion(jugador, enemigo) == Habilidad.RESULTADO_ACTIVADA)
                                habilidadesCombateEjecutadas.Add(hab);
                }

                //Se elige entre utilizar un objeto o atacar con un arma
                int eleccion = ElegirArmaObjeto();
                if (eleccion == ELEGIR_OBJETO)
                {
                    Objeto objeto = ElegirObjeto(jugador.GetObjetos());
                    if (objeto == null)
                        eleccion = ELEGIR_ARMA;
                    else
                        objeto.RealizarAccion(jugador, enemigo, sala);
                }

                if (eleccion == ELEGIR_ARMA)
                {
                    // Se elige el arma con la que se va a atacar
                    Arma arma = ElegirArma(jugador.GetArmas());
                    if (arma == null)
                        continue;

                    jugador.SetArmaCombate(arma);

                    int res = ataqueJugador(jugador, enemigo);
                    if (res == RESULTADO_JUGADOR_GANA)
                    {
                        jugador.ReiniciarStatsCombate();
                        return res;
                    }
                }

                // Ataca el enemigo
                MostrarAtaque(enemigo);
                if (enemigo.Atacar(jugador))
                    return RESULTADO_ENEMIGO_GANA;

                Thread.Sleep(1000);

                int velJugador = jugador.GetStatCombate(Mob.INDICE_AGILIDAD);
                int velEnemigo = enemigo.GetStatCombate(Mob.INDICE_AGILIDAD);
                if (velJugador - velEnemigo > 4 && eleccion == ELEGIR_ARMA)
                {
                    int res = ataqueJugador(jugador, enemigo);
                    if (res == RESULTADO_JUGADOR_GANA)
                    {
                        jugador.ReiniciarStatsCombate();
                        return res;
                    }
                }

                if (velEnemigo - velJugador > 4)
                {
                    MostrarAtaque(enemigo);
                    if (enemigo.Atacar(jugador))
                        return RESULTADO_ENEMIGO_GANA;

                    Thread.Sleep(1000);
                }

            } while (true);
        }

        private int ataqueJugador(Jugador jugador, Enemigo enemigo)
        {
            // Se selecciona una habilidad de ataque para que se ejecute o se ataque normalmente
            List<Habilidad> habilidadesAtaque = jugador.GetHabilidadesPorTipo(Habilidad.TIPO_ATAQUE);
            List<Habilidad> habilidadesEjecutadas = new List<Habilidad>();
            foreach (Habilidad habilidad1 in habilidadesAtaque)
            {
                if (habilidad1.Ejecutar())
                    habilidadesEjecutadas.Add(habilidad1);
            }

            int resultadoHabilidad = Habilidad.RESULTADO_SIN_ACTIVAR;

            if (habilidadesEjecutadas.Count > 0)
            {
                Habilidad habilidad = Habilidad.GetHabilidadPorPrioridad(habilidadesEjecutadas);
                resultadoHabilidad = habilidad.RealizarAccion(jugador, enemigo);

                if (resultadoHabilidad != Habilidad.RESULTADO_SIN_ACTIVAR)
                {
                    MostrarHabilidad(jugador, habilidad);
                    Thread.Sleep(1000);
                }
            }

            if (resultadoHabilidad == Habilidad.RESULTADO_MOB_DERROTADO)
                return RESULTADO_JUGADOR_GANA;

            if (resultadoHabilidad == Habilidad.RESULTADO_SIN_ACTIVAR)
            {
                Arma arma = jugador.GetArmaCombate();
                MostrarAtaque(jugador, arma);
                Thread.Sleep(1000);
                if (arma.Atacar(enemigo))
                    return RESULTADO_JUGADOR_GANA;
            }

            return RESULTADO_EN_JUEGO;
        }
    }
}
