using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos
{
    /// <summary>
    /// Clase básica que define a los enemigos. Todos los enemigos deben heredar de esta clase
    /// </summary>
    abstract class AbstractEnemigo : AbstractMob
    {
        private const int TOTAL_STATS_ENEMIGO = 16;

        /// <summary>
        /// Objeto que deja al jugador cuando el enemigo es derrotado
        /// </summary>
        protected AbstractObjeto drop;

        /// <summary>
        /// Experiencia que deja al jugador cuando el enemigo es derrotado
        /// </summary>
        private int dropExp;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="pv">Vida inicial del enemigo</param>
        /// <param name="fue">Fuerza inicial del enemigo</param>
        /// <param name="mag">Magia inicial del enemigo</param>
        /// <param name="agi">Agilidad inicial del enemigo</param>
        /// <param name="hab">Habilidad inicial del enemigo</param>
        /// <param name="def">Defensa inicial del enemigo</param>
        /// <param name="res">Resistencia mágica inicial del enemigo</param>
        /// <param name="probCrit">Probabilidad de crítico inicial del enemigo</param>
        /// <param name="danCrit">Daño crítico inicial del enemigo</param>
        /// <param name="pvMax">Vida máxima del enemigo</param>
        /// <param name="fueMax">Fuerza máxima del enemigo</param>
        /// <param name="magMax">Magia máxima del enemigo</param>
        /// <param name="agiMax">Agilidad máxima del enemigo</param>
        /// <param name="habMax">habilidad máxima del enemigo</param>
        /// <param name="defMax">Defensa máxima del enemigo</param>
        /// <param name="resMax">Resistencia mágica máxima del enemigo</param>
        /// <param name="probCritMax">Probabilidad de crítico máxima del enemigo</param>
        /// <param name="danCritMax">Daño crítico máximo del enemigo</param>
        /// <param name="nombre">Nombre del enemigo</param>
        /// <param name="descripcion">Descripción del enemigo</param>
        /// <param name="dropExp">Experiencia que deja al jugador cuando el enemigo es derrotado</param>
        /// <param name="drop">Objeto que deja al jugador cuando el enemigo es derrotado</param>
        protected AbstractEnemigo(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, AbstractObjeto drop) :
            base(pv, fue, mag, agi, hab, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, habMax, defMax, resMax, probCritMax, danCritMax, nombre, descripcion, TOTAL_STATS_ENEMIGO)
        {
            this.dropExp = dropExp;

            this.drop = drop;
        }

        /// <summary>
        /// Calcula el daño que hace al jugador al atacarlo
        /// </summary>
        /// <param name="jugador"><see cref="AbstractMob">Jugador</see> al que ataca</param>
        /// <returns>Daño realizado al jugador</returns>
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

        /// <summary>
        /// Calcula la cantidad que debe aumentar un stat para que cuando llegue al nivel máximo tenga la el valor máximo de dicho stat
        /// </summary>
        /// <param name="stat">Valor del stat a aumentar</param>
        /// <param name="statMax">Valor máximo del stat a aumentar</param>
        /// <returns>Cantidad que debe aumentar un stat para que cuando llegue al nivel máximo tenga la el valor máximo de dicho stat</returns>
        private int getDiferenciaStat(int stat, int statMax)
        {
            int statSubido = (statMax * nivel) / NIVEL_MAX;

            return statSubido - stat;
        }

        /// <summary>
        /// Devuelve el drop que posee al jugador
        /// </summary>
        /// <returns>Drop del enemigo</returns>
        public virtual AbstractObjeto Drop() => drop;

        /// <summary>
        /// Devuelve la experiencia del enemigo
        /// </summary>
        /// <returns>Experiencia del enemigo</returns>
        public int GetExp() => dropExp;

        /// <summary>
        /// Cura completamente al enemigo
        /// </summary>
        public void CurarCompleto()
        {
            this.pv = this.pvTotal;
        }
    }
}
