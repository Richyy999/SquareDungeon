using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Modelo;
using SquareDungeon.Objetos;
using SquareDungeon.Salas;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreLLaveJefe : AbstractCofre
    {
        public CofreLLaveJefe() : base(NOMBRE_COFRE_LLAVE_JEFE, DESC_COFRE_LLAVE_JEFE, new LlaveJefe()) { }

        public override bool AbrirCofre(AbstractJugador jugador, AbstractSala sala, Partida partida)
        {
            jugador.SetLlaveJefe(getContenido());
            return true;
        }

        protected override LlaveJefe getContenido() => (LlaveJefe)contenido;
    }
}
