using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    abstract class Enemigo : Mob
    {
        protected Objeto drop;

        private int dropExp;

        protected Enemigo(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, Objeto drop) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax, nombre, descripcion)
        {
            this.dropExp = dropExp;

            this.drop = drop;
        }

        public virtual bool Atacar(Jugador jugador)
        {
            int ata = (int)(fue * 1.2);

            int dano = ata - jugador.GetStat(INDICE_DEFENSA);
            int crit = 1 + GetCritico();

            dano *= crit;
            return jugador.Danar(dano);
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

        public virtual Objeto Drop() => drop;

        public int GetExp() => dropExp;
    }
}
