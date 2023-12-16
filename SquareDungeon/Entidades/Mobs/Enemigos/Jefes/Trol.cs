using SquareDungeon.Habilidades.DanoAdicional;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    class Trol : AbstractJefe
    {
        public Trol() :
            base(250, 6, 1, 2, 2, 3, 2, 10, 40, 1000, 70, 10, 25, 25, 40, 25, 50, 100,
                NOMBRE_TROL, DESC_TROL, 120, null, new HabilidadTrol())
        { }
    }
}
