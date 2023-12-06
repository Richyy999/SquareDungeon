using SquareDungeon.Habilidades;
using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    abstract class AbstractJefe : AbstractEnemigo
    {
        protected AbstractJefe(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, Objeto drop, AbstractHabilidad habilidad) :
            base(pv, fue, mag, agi, hab, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, habMax, defMax, resMax, probCritMax, danCritMax,
                nombre, descripcion, dropExp, drop)
        {
            habilidades.Add(habilidad);
        }
    }
}
