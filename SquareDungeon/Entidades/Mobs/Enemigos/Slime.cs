using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    class Slime : Enemigo
    {
        public Slime(Objeto drop) : base(50, 1, 2, 0, 0, 3, 5, 20,
            300, 15, 25, 10, 10, 30, 20, 50, NOMBRE_SLIME, DESC_SLIME, 65, drop)
        { }

        public override bool Atacar(Jugador jugador)
        {
            int ata = (int)(mag * 1.2);

            int dano = ata - jugador.GetStat(INDICE_RESISTENCIA);

            return jugador.Danar(dano);
        }
    }
}
