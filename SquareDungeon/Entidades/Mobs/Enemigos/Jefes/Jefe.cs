using SquareDungeon.Habilidades;
using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    abstract class Jefe : Enemigo
    {
        protected Habilidad habilidad;

        protected Jefe(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax,
            Objeto drop, Habilidad habilidad) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax,
                drop)
        {
            this.habilidad = habilidad;
        }
    }
}
