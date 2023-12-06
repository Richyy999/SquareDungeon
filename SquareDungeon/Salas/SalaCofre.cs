using System;

using SquareDungeon.Modelo;
using SquareDungeon.Armas;
using SquareDungeon.Objetos;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Cofres;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Modelo.EntradaSalida;

namespace SquareDungeon.Salas
{
    class SalaCofre : Sala
    {
        private Cofre cofre;

        public SalaCofre(int x, int y, Cofre cofre) : base(x, y)
        {
            this.cofre = cofre;
        }

        public override void Entrar(Partida partida, AbstractJugador jugador)
        {
            if (GetEstado() == ESTADO_SIN_VISITAR || GetEstado() == ESTADO_COFRE_SIN_ABRIR)
            {
                SetEstado(ESTADO_COFRE_SIN_ABRIR);
                if (PreguntarAbrirCofre())
                {
                    SetEstado(ESTADO_VISITADO);
                    if (cofre.GetType() == typeof(CofreObjeto))
                    {
                        CofreObjeto cofreObjeto = (CofreObjeto)cofre;
                        bool objetoAnadido;

                        do
                        {
                            objetoAnadido = jugador.AnadirObjeto(cofreObjeto.AbrirCofre());

                            if (!objetoAnadido)
                            {
                                Console.WriteLine("Tu inventario está lleno, elimina un objeto para obtener más espacio");
                                Objeto objeto = ElegirObjeto(jugador.GetObjetos());
                                if (objeto != null)
                                    jugador.EliminarObjeto(objeto);
                                else
                                {
                                    objetoAnadido = true;
                                    SetEstado(ESTADO_COFRE_SIN_ABRIR);
                                }
                            }
                            else
                                MostrarObjetoConseguido(cofreObjeto.AbrirCofre());
                        } while (!objetoAnadido);
                    }
                    else if (cofre.GetType() == typeof(CofreLLaveJefe))
                    {
                        CofreLLaveJefe cofreObjeto = (CofreLLaveJefe)cofre;
                        bool objetoAnadido;

                        do
                        {
                            objetoAnadido = jugador.AnadirObjeto(cofreObjeto.AbrirCofre());

                            if (!objetoAnadido)
                            {
                                Console.WriteLine("Tu inventario está lleno, elimina un objeto para obtener más espacio");
                                Objeto objeto = ElegirObjeto(jugador.GetObjetos());
                                if (objeto != null)
                                    jugador.EliminarObjeto(objeto);
                                else
                                {
                                    objetoAnadido = true;
                                    SetEstado(ESTADO_COFRE_SIN_ABRIR);
                                }
                            }
                            else
                                MostrarObjetoConseguido(cofreObjeto.AbrirCofre());
                        } while (!objetoAnadido);
                    }
                    else if (cofre.GetType() == typeof(CofreHabilidad))
                    {
                        CofreHabilidad cofreHabilidad = (CofreHabilidad)cofre;
                        AbstractHabilidad habilidad = cofreHabilidad.AbrirCofre();

                        if (jugador.AnadirHabilidad(habilidad))
                            MostrarHabilidadObtenida(habilidad);
                        else
                            MostrarHabilidadEquipada(habilidad);
                    }
                    else if (cofre.GetType() == typeof(CofreArma))
                    {
                        CofreArma cofreArma = (CofreArma)cofre;
                        AbstractArma arma = cofreArma.AbrirCofre();

                        bool armaAnadida;
                        do
                        {
                            try
                            {
                                arma.SetPortador(jugador);
                                armaAnadida = jugador.EquiparArma(arma);
                                if (!armaAnadida)
                                {
                                    Console.WriteLine("No puedes llevar más armas, elimina una para tner espacio");
                                    AbstractArma armaEliminar = ElegirArma(jugador.GetArmas());
                                    if (armaEliminar != null)
                                        jugador.EliminarArma(armaEliminar);
                                    else
                                    {
                                        armaAnadida = true;
                                        SetEstado(ESTADO_COFRE_SIN_ABRIR);
                                    }
                                }
                                else
                                    MostrarArmaConseguida(arma);
                            }
                            catch (ArgumentException)
                            {
                                armaAnadida = true;
                                MostrarNoEquiparArma();
                            }
                        } while (!armaAnadida);
                    }
                }
            }

            if (GetEstado() == ESTADO_VISITADO)
                partida.SetPosicionJugador(x, y);
        }
    }
}
