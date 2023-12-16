using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    class Slime : AbstractEnemigo
    {
        public Slime(AbstractObjeto drop) : base(50, 1, 4, 0, 0, 0, 2, 5, 20,
            300, 15, 27, 10, 10, 10, 30, 20, 50, NOMBRE_SLIME, DESC_SLIME, 65, drop)
        { }

        public override int Atacar(AbstractMob jugador)
        {
            int ata = (int)(magCom * 1.2);

            int dano = ata - jugador.GetStatCombate(INDICE_RESISTENCIA);
            if (dano <= 0)
                dano = 1;

            return dano;
        }
    }
}
