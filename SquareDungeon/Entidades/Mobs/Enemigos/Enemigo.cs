using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    abstract class Enemigo : Mob
    {
        protected Objeto drop;

        protected Enemigo(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax,
            Objeto drop) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax)
        {
            this.drop = drop;
        }

        public Objeto Drop() => drop;
    }
}
