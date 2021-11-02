namespace SquareDungeon.Entidad.Mob
{
    abstract class Mob : Entidad
    {
        public const int EXP_MAX = 100;
        public const int NIVEL_MAX = 50;

        protected int pv;
        protected int fue;
        protected int mag;
        protected int agi;
        protected int def;
        protected int res;
        protected int probCrit;
        protected int danCrit;

        protected int nivel;
        protected int exp;

        protected Mob(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit)
        {
            this.pv = pv;
            this.fue = fue;
            this.mag = mag;
            this.agi = agi;
            this.def = def;
            this.res = res;
            this.probCrit = probCrit;
            this.danCrit = danCrit;

            nivel = 1;
            exp = 0;
        }

        protected virtual void SubirNivel() { }

        public virtual void SubirNivel(int exp) { }
    }
}
