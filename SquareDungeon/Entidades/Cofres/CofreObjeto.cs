using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Modelo;
using SquareDungeon.Objetos;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreObjeto : AbstractCofre
    {
        public CofreObjeto(AbstractObjeto objeto) : base(NOMBRE_COFRE_OBJETO, DESC_COFRE_OBJETO, objeto) { }

        public override bool AbrirCofre(AbstractJugador jugador, AbstractSala sala, Partida partida)
        {
            bool objetoAnadido = jugador.AnadirObjeto(getContenido());
            if (!objetoAnadido)
            {
                EntradaSalida.MostrarMensaje("Tu inventario está lleno, elimina un objeto para obtener más espacio");
                AbstractObjeto objetoElegido = EntradaSalida.ElegirObjeto(jugador.GetObjetos());
                if (objetoElegido != null)
                {
                    jugador.EliminarObjeto(objetoElegido);
                    jugador.AnadirObjeto(getContenido());
                    EntradaSalida.MostrarObjetoConseguido(getContenido());
                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                EntradaSalida.MostrarObjetoConseguido(getContenido());
                return true;
            }
        }

        protected override AbstractObjeto getContenido() => (AbstractObjeto)contenido;
    }
}
