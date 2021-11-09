using System;

using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

using static SquareDungeon.EntradaSalida;

namespace SquareDungeon.Salas
{
    class SalaJefe : Sala
    {
        private Jefe jefe;

        private bool abierta;

        public SalaJefe(int x, int y, Jefe jefe) : base(x, y)
        {
            this.jefe = jefe;
            abierta = false;
        }

        public override void Entrar(Partida partida, Jugador jugador)
        {
            if (abierta)
            {
                Random random = new Random();
                int diferencia = random.Next(-1, 2);
                int nivelJugador = jugador.GetNivel() + diferencia;

                jefe.SubirNivel(nivelJugador * Mob.EXP_MAX);

                partida.SetPosicionJugador(x, y);

                int res = partida.Combatir(jugador, jefe, this);
                if (res == Partida.RESULTADO_JUGADOR_GANA)
                {
                    MostrarVictoria(jugador, jefe);
                    partida.SetResultado(Partida.RESULTADO_JEFE_ELIMINADO);
                }

                if (res == Partida.RESULTADO_ENEMIGO_GANA)
                    partida.SetResultado(Partida.RESULTADO_ENEMIGO_GANA);
            }
        }

        public bool AbrirSala(Jugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR || GetEstado() == ESTADO_SALA_JEFE_SIN_ABRIR)
            {
                DescubrirJefe();
                LlaveJefe llave = GetLlaveJefe(jugador.GetObjetos());
                SetEstado(ESTADO_SALA_JEFE_SIN_ABRIR);
                if (llave == null)
                    return false;

                llave.RealizarAccion(jugador, null, this);
                SetEstado(ESTADO_SALA_JEFE_ABIERTA);
                abierta = true;
                return true;
            }

            return abierta;
        }
    }
}
