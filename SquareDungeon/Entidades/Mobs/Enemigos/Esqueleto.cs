using System;
using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    class Esqueleto : Enemigo
    {
        public Esqueleto(Objeto drop) : base(50, 4, 0, 1, 3, 0, 5, 15,
            500, 60, 12, 20, 30, 10, 30, 60, NOMBRE_ESQUELETO, DESC_ESQUELETO, 80, drop)
        { }
    }
}
