using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    abstract class AbstractEnemigo : AbstractMob
    {
        private const int TOTAL_STATS_ENEMIGO = 16;

        protected AbstractObjeto drop;

        private int dropExp;

        protected AbstractEnemigo(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, AbstractObjeto drop) :
            base(pv, fue, mag, agi, hab, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, habMax, defMax, resMax, probCritMax, danCritMax, nombre, descripcion, TOTAL_STATS_ENEMIGO)
        {
            this.dropExp = dropExp;

            this.drop = drop;
        }

        public virtual int Atacar(AbstractMob jugador)
        {
            int ata = (int)(fueCom * 1.2);

            int dano = ata - jugador.GetStatCombate(INDICE_DEFENSA);
            if (dano < 0)
                dano = 0;

            double crit = 1 + GetCritico();

            dano = (int)(dano * crit);

            if (dano <= 0)
                dano = 1;

            return dano;
        }

        protected override void subirNivel()
        {
            int diferencia = getDiferenciaStat(pv, pvMax);
            if (diferencia > 0)
                subirStat(INDICE_VIDA_TOTAL, diferencia, pvMax);

            diferencia = getDiferenciaStat(fue, fueMax);
            if (diferencia > 0)
                subirStat(INDICE_FUERZA, diferencia, fueMax);

            diferencia = getDiferenciaStat(mag, magMax);
            if (diferencia > 0)
                subirStat(INDICE_MAGIA, diferencia, magMax);

            diferencia = getDiferenciaStat(agi, agiMax);
            if (diferencia > 0)
                subirStat(INDICE_AGILIDAD, diferencia, agiMax);

            diferencia = getDiferenciaStat(def, defMax);
            if (diferencia > 0)
                subirStat(INDICE_DEFENSA, diferencia, defMax);

            diferencia = getDiferenciaStat(res, resMax);
            if (diferencia > 0)
                subirStat(INDICE_RESISTENCIA, diferencia, resMax);

            diferencia = getDiferenciaStat(probCrit, probCritMax);
            if (diferencia > 0)
                subirStat(INDICE_PROBABILIDAD_CRITICO, diferencia, probCritMax);

            diferencia = getDiferenciaStat(danCrit, danCritMax);
            if (diferencia > 0)
                subirStat(INDICE_DANO_CRITICO, diferencia, danCritMax);
        }

        private int getDiferenciaStat(int stat, int statMax)
        {
            int statSubido = (statMax * nivel) / NIVEL_MAX;

            return statSubido - stat;
        }

        public virtual AbstractObjeto Drop() => drop;

        public int GetExp() => dropExp;

        public void CurarCompleto()
        {
            this.pv = this.pvTotal;
        }
    }
}
