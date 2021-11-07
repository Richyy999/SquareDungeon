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

        public static int Combatir(Jugador jugador, Enemigo enemigo, Sala sala)
        {
            // Se ejecutan todas las habilidades pre combate
            List<Habilidad> habilidadesPreCombate = jugador.GetHabilidadesPorTipo(Habilidad.TIPO_PRE_COMBATE);
            foreach (Habilidad habilidad in habilidadesPreCombate)
            {
                habilidad.RealizarAccion(jugador, enemigo);
            }

            // Se obtienen los PV iniciales para dibujar las barras de vida
            int pvInicialesEnemigo = enemigo.GetStat(Mob.INDICE_VIDA);
            int pvInicialesJugador = jugador.GetStat(Mob.INDICE_VIDA);

            List<Habilidad> habilidadesCombateEjecutadas = new List<Habilidad>();
            do
            {
                // Se muestran las barras de viada del enemigo y del jugador
                Console.Clear();
                MostrarPV(enemigo.GetNombre(), pvInicialesEnemigo, enemigo.GetStat(Mob.INDICE_VIDA));
                MostrarPV(jugador.GetNombre(), pvInicialesJugador, jugador.GetStat(Mob.INDICE_VIDA));

                // Se ejecutan todas las habilidades de combate que no se hayan ejecutado antes
                List<Habilidad> habilidadesCombate = jugador.GetHabilidadesPorTipo(Habilidad.TIPO_COMBATE);
                foreach (Habilidad hab in habilidadesCombate)
                {
                    if (!habilidadesCombateEjecutadas.Contains(hab))
                        if (hab.Ejecutar())
                            if (hab.RealizarAccion(jugador, enemigo) == Habilidad.RESULTADO_ACTIVADA)
                                habilidadesCombateEjecutadas.Add(hab);
                }

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
                    jugador.SetArmaCombate(arma);

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
                        MostrarAtaque(jugador, arma);
                        Thread.Sleep(1000);
                        if (arma.Atacar(enemigo))
                            return RESULTADO_JUGADOR_GANA;
                    }
                }
                // Ataca el enemigo
                MostrarAtaque(enemigo);
                if (enemigo.Atacar(jugador))
                    return RESULTADO_ENEMIGO_GANA;

                Thread.Sleep(1000);

            } while (true);
        }
    }
}
