using System;
using System.Collections.Generic;

using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Objetos;
using SquareDungeon.Armas;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    abstract class AbstractJugador : AbstractMob
    {
        private const int TOTAL_STATS_JUGADOR = 20;

        private int pvAnt;
        private int fueAnt;
        private int magAnt;
        private int agiAnt;
        private int habAnt;
        private int defAnt;
        private int resAnt;
        private int probCritAnt;
        private int danCritAnt;
        private int nivelAnt;
        private int expAnt;

        private byte pvCrec;
        private byte fueCrec;
        private byte magCrec;
        private byte agiCrec;
        private byte habCrec;
        private byte defCrec;
        private byte resCrec;
        private byte probCritCrec;
        private byte danCritCrec;

        protected AbstractArma armaCombate;

        protected AbstractArma[] armas;

        protected AbstractObjeto[] objetos;

        private LlaveJefe llaveJefe;

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

        public int[,] GetStatsNuevos() => new int[,]
        { { pvAnt, pvTotal}, { fueAnt, fue}, { magAnt, mag}, { agiAnt, agi}, {habAnt, hab}, {defAnt, def }, {resAnt, res },
            {probCritAnt, probCrit }, {danCritAnt, danCrit }, {expAnt, exp }, {nivelAnt, nivel } };

        public abstract bool EquiparArma(AbstractArma arma);

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

        public abstract AbstractArma[] GetArmas();

        public void SetArmaCombate(AbstractArma arma)
        {
            armaCombate = arma;
        }

        public abstract AbstractArma GetArmaCombate();

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

        public LlaveJefe GetLlaveJefe() => llaveJefe;

        public void SetLlaveJefe(LlaveJefe llaveJefe)
        {
            if (this.llaveJefe != null)
                throw new ArgumentException("Ya posees la llave del jefe");

            this.llaveJefe = llaveJefe;
        }

        public void EliminarLlaveJefe()
        {
            llaveJefe = null;
        }

        public AbstractObjeto[] GetObjetos() => objetos;

        public Type GetTipoArmas() => armas[0].GetType();
    }
}
