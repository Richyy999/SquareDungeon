using System;
using System.Collections.Generic;

using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Objetos;
using SquareDungeon.Armas;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    /// <summary>
    /// Clase básica que define a los jugadores. Todos los jugadores deben heredar de esta clase
    /// </summary>
    abstract class AbstractJugador : AbstractMob
    {
        private const int TOTAL_STATS_JUGADOR = 20;

        /// <summary>
        /// Vida del jugador antes de subir de nivel
        /// </summary>
        private int pvAnt;
        /// <summary>
        /// Fuerza del jugador antes de subir de nivel
        /// </summary>
        private int fueAnt;
        /// <summary>
        /// Magia del jugador antes de subir de nivel
        /// </summary>
        private int magAnt;
        /// <summary>
        /// Agilidad del jugador antes de subir de nivel
        /// </summary>
        private int agiAnt;
        /// <summary>
        /// Habilidad del jugador antes de subir de nivel
        /// </summary>
        private int habAnt;
        /// <summary>
        /// Defensa del jugador antes de subir de nivel
        /// </summary>
        private int defAnt;
        /// <summary>
        /// Resistencia mágica del jugador antes de subir de nivel
        /// </summary>
        private int resAnt;
        /// <summary>
        /// Probabilidad de crítico del jugador antes de subir de nivel
        /// </summary>
        private int probCritAnt;
        /// <summary>
        /// Daño crítico del jugador antes de subir de nivel
        /// </summary>
        private int danCritAnt;
        /// <summary>
        /// Nivel del jugador antes de subir de nivel
        /// </summary>
        private int nivelAnt;
        /// <summary>
        /// Experiencia del jugador antes de subir de nivel
        /// </summary>
        private int expAnt;

        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de vida
        /// </summary>
        private byte pvCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de fuerza
        /// </summary>
        private byte fueCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de magia
        /// </summary>
        private byte magCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de agilidad
        /// </summary>
        private byte agiCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de habilidad
        /// </summary>
        private byte habCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de defensa
        /// </summary>
        private byte defCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de resistencia mágica
        /// </summary>
        private byte resCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de probabilidad de crítico
        /// </summary>
        private byte probCritCrec;
        /// <summary>
        /// Probabilidades de que el jugador aumente el stat de daño crítico
        /// </summary>
        private byte danCritCrec;

        /// <summary>
        /// <see cref="AbstractArma">Arma</see> que usa el jugador en combate para atacar al <see cref="AbstractEnemigo">enemigo</see>
        /// </summary>
        protected AbstractArma armaCombate;

        /// <summary>
        /// Array de <see cref="AbstractArma">armas</see> que posee el jugador
        /// </summary>
        protected AbstractArma[] armas;

        /// <summary>
        /// Array de <see cref="AbstractObjeto">objetos</see> que posee el jugador
        /// </summary>
        protected AbstractObjeto[] objetos;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="pv">Vida inicial del jugador</param>
        /// <param name="fue">Fuerza inicial del jugador</param>
        /// <param name="mag">Magia inicial del jugador</param>
        /// <param name="agi">Agilidad inicial del jugador</param>
        /// <param name="hab">Habilidad inicial del jugador</param>
        /// <param name="def">Defensa inicial del jugador</param>
        /// <param name="res">Resistencia mágica inicial del jugador</param>
        /// <param name="pvCrec">Stat de crecimiento de la vida del jugador</param>
        /// <param name="fueCrec">Stat de crecimiento de la fuerza del jugador</param>
        /// <param name="magCrec">Stat de crecimiento de la magia del jugador</param>
        /// <param name="agiCrec">Stat de crecimiento de la agilidad del jugador</param>
        /// <param name="habCrec">Stat de crecimiento de la habilidad del jugador</param>
        /// <param name="defCrec">Stat de crecimiento de la defensa del jugador</param>
        /// <param name="resCrec">Stat de crecimiento de la resistencia mágica del jugador</param>
        /// <param name="pvMax">Vida máxima del jugador</param>
        /// <param name="fueMax">Fuerza máxima del jugador</param>
        /// <param name="magMax">Magia máxima del jugador</param>
        /// <param name="agiMax">Agilidad máxima del jugador</param>
        /// <param name="habMax">habilidad máxima del jugador</param>
        /// <param name="defMax">Defensa máxima del jugador</param>
        /// <param name="resMax">Resistencia mágica máxima del jugador</param>
        /// <param name="nombre">Nombre del jugador</param>
        /// <param name="descripcion">Descripción del jugador</param>
        /// <param name="habilidad">Habilidad del jugador</param>
        protected AbstractJugador(int pv, int fue, int mag, int agi, int hab, int def, int res,
            byte pvCrec, byte fueCrec, byte magCrec, byte agiCrec, byte habCrec, byte defCrec, byte resCrec,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax,
            string nombre, string descripcion, AbstractHabilidad habilidad) :
            base(pv, fue, mag, agi, hab, def, res, 5, 50,
                pvMax, fueMax, magMax, agiMax, habMax, defMax, resMax, 100, 200, nombre, descripcion, TOTAL_STATS_JUGADOR)
        {
            this.pvCrec = pvCrec;
            this.fueCrec = fueCrec;
            this.magCrec = magCrec;
            this.agiCrec = agiCrec;
            this.habCrec = habCrec;
            this.defCrec = defCrec;
            this.resCrec = resCrec;
            this.probCritCrec = 30;
            this.danCritCrec = 70;

            pvAnt = pv;
            fueAnt = fue;
            magAnt = mag;
            agiAnt = agi;
            habAnt = hab;
            defAnt = def;
            resAnt = res;
            probCritAnt = probCrit;
            danCritAnt = danCrit;
            nivelAnt = 1;
            expAnt = 0;

            armas = new AbstractArma[4];

            habilidades = new List<AbstractHabilidad>();
            habilidades.Add(habilidad);

            objetos = new AbstractObjeto[15];
        }

        /// <summary>
        /// Aumenta los stats del jugador al subir de nivel.
        /// </summary>
        /// <seealso cref="SubirNivel(int)"/>
        protected override void subirNivel()
        {
            Random random = new Random();
            if (puedeSubirStat(pvCrec))
            {
                pvAnt = pvTotal;
                subirStat(INDICE_VIDA_TOTAL, random.Next(1, 3), pvMax);
            }
            if (puedeSubirStat(fueCrec))
            {
                fueAnt = fue;
                subirStat(INDICE_FUERZA, random.Next(1, 3), fueMax);
            }
            if (puedeSubirStat(magCrec))
            {
                magAnt = mag;
                subirStat(INDICE_MAGIA, random.Next(1, 3), magMax);
            }
            if (puedeSubirStat(agiCrec))
            {
                agiAnt = agi;
                subirStat(INDICE_AGILIDAD, random.Next(1, 3), agiMax);
            }
            if (puedeSubirStat(habCrec))
            {
                habAnt = hab;
                subirStat(INDICE_HABILIDAD, random.Next(1, 3), habMax);
            }
            if (puedeSubirStat(defCrec))
            {
                defAnt = def;
                subirStat(INDICE_DEFENSA, random.Next(1, 3), defMax);
            }
            if (puedeSubirStat(resCrec))
            {
                resAnt = res;
                subirStat(INDICE_RESISTENCIA, random.Next(1, 3), resMax);
            }
            if (puedeSubirStat(probCritCrec))
            {
                probCritAnt = probCrit;
                subirStat(INDICE_PROBABILIDAD_CRITICO, random.Next(1, 3), probCritMax);
            }
            if (puedeSubirStat(danCritCrec))
            {
                danCritAnt = danCrit;
                subirStat(INDICE_DANO_CRITICO, random.Next(2, 5), danCritMax);
            }
        }

        /// <summary>
        /// Ataca al enemigo con el <see cref="armaCombate">arma de combate</see>
        /// </summary>
        /// <param name="enemigo"><see cref="AbstractEnemigo">enemigo</see> al que ataca el jugador</param>
        /// <returns>Daño que inflinge al enemigo</returns>
        public virtual int Atacar(AbstractEnemigo enemigo)
        {
            AbstractArma armaCombate = GetArmaCombate();
            return armaCombate.Atacar(enemigo);
        }

        public override bool SubirNivel(int exp)
        {
            expAnt = this.exp;
            return base.SubirNivel(exp);
        }

        /// <summary>
        /// Devuelve un array con los stats que tiene el jugador antes y desués de subir de nivel
        /// </summary>
        /// <returns></returns>
        public int[,] GetStatsNuevos() => new int[,]
        { { pvAnt, pvTotal}, { fueAnt, fue}, { magAnt, mag}, { agiAnt, agi}, {habAnt, hab}, {defAnt, def }, {resAnt, res },
            {probCritAnt, probCrit }, {danCritAnt, danCrit }, {expAnt, exp }, {nivelAnt, nivel } };

        /// <summary>
        /// Añade un arma al inventario del jugador
        /// </summary>
        /// <param name="arma"><see cref="AbstractArma">Arma</see> a añadir</param>
        /// <returns></returns>
        public abstract bool EquiparArma(AbstractArma arma);

        /// <summary>
        /// Elimina un arma del inventario del jugador
        /// </summary>
        /// <param name="arma"><see cref="AbstractArma">Arma</see> a eliminar</param>
        public void EliminarArma(AbstractArma arma)
        {
            AbstractArma[] armas = new AbstractArma[this.armas.Length];
            int indice = 0;
            for (int i = 0; i < this.armas.Length; i++)
            {
                if (this.armas[i] == null)
                    continue;

                if (this.armas[i].GetNombre() != arma.GetNombre())
                {
                    armas[indice] = this.armas[i];
                    indice++;
                }
            }

            this.armas = armas;
        }

        /// <summary>
        /// Devuelve un array con las armas del jugador
        /// </summary>
        /// <returns>Array con las armas del jugador</returns>
        public abstract AbstractArma[] GetArmas();

        /// <summary>
        /// Establece un arma como arma de combate
        /// </summary>
        /// <param name="arma"><see cref="AbstractArma">Arma</see> que se establecerá como arma de combate</param>
        public void SetArmaCombate(AbstractArma arma)
        {
            armaCombate = arma;
        }

        /// <summary>
        /// Devuelve el <see cref="armaCombate">arma de combate</see>
        /// </summary>
        /// <returns><see cref="armaCombate">Arma de combate</see></returns>
        public abstract AbstractArma GetArmaCombate();

        /// <summary>
        /// Añade una habilidad al jugador si el jugador no la posee
        /// </summary>
        /// <param name="habilidad"><see cref="AbstractHabilidad">Habilidad</see> a añadir</param>
        /// <returns>true si la habilidad se ha añadido, false en caso de que ya posea la habilidad</returns>
        public bool AnadirHabilidad(AbstractHabilidad habilidad)
        {
            bool contiene = false;
            foreach (AbstractHabilidad hab in habilidades)
            {
                if (hab.GetNombre().Equals(habilidad.GetNombre()))
                    contiene = true;
            }

            if (!contiene)
                habilidades.Add(habilidad);

            return !contiene;
        }

        /// <summary>
        /// Añade un objeto al inventario del jugador.<br/>Si el jugador ya posee el objeto, aumentan los usos de dicho objeto
        /// </summary>
        /// <param name="objeto"><see cref="AbstractObjeto">Objeto</see> a añadir</param>
        /// <returns>true si se ha añadido el objeto, false en caso contrario</returns>
        public bool AnadirObjeto(AbstractObjeto objeto)
        {
            if (objeto == null)
                return true;

            for (int i = 0; i < objetos.Length; i++)
            {
                if (objetos[i] == null)
                {
                    objetos[i] = objeto;
                    return true;
                }

                if (objetos[i].GetNombre().Equals(objeto.GetNombre()))
                {
                    objetos[i].AnadirCantidad(objeto.GetCantidad());
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Elimina un objeto del inventario del jugador
        /// </summary>
        /// <param name="objeto"><see cref="AbstractObjeto">Objeto</see> a eliminar</param>
        public void EliminarObjeto(AbstractObjeto objeto)
        {
            AbstractObjeto[] objetos = new AbstractObjeto[this.objetos.Length];
            int indice = 0;
            for (int i = 0; i < this.objetos.Length; i++)
            {
                if (this.objetos[i] == null)
                    continue;

                if (this.objetos[i].GetNombre() != objeto.GetNombre())
                {
                    objetos[indice] = this.objetos[i];
                    indice++;
                }
            }

            this.objetos = objetos;
        }

        /// <summary>
        /// Devuelve un array con los objetos del inventario del jugador
        /// </summary>
        /// <returns>Array con los objetos del inventario del jugador</returns>
        public AbstractObjeto[] GetObjetos() => objetos;

        /// <summary>
        /// Devuelve el tipo de arma que lleva el jugador
        /// </summary>
        /// <returns>Tipo de arma que lleva el jugador</returns>
        public Type GetTipoArmas() => armas[0].GetType();
    }
}
