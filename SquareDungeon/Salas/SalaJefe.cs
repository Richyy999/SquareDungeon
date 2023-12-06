using System;

using SquareDungeon.Modelo;
using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

using static SquareDungeon.Modelo.EntradaSalida;

namespace SquareDungeon.Salas
{
    class SalaJefe : Sala
    {
        private AbstractJefe jefe;

        private bool abierta;

        public SalaJefe(int x, int y, AbstractJefe jefe) : base(x, y)
        {
            this.jefe = jefe;
            abierta = false;
        }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (abierta)
            {
                Random random = new Random();
                int diferencia = random.Next(-1, 2);
                int nivelJugador = jugador.GetNivel() + diferencia;

                jefe.SubirNivel(nivelJugador * AbstractMob.EXP_MAX);
                jefe.CurarCompleto();

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

        public bool AbrirSala(AbstractJugador jugador)
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
