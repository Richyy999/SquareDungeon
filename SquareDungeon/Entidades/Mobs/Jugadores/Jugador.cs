using System;
using System.Collections.Generic;

using SquareDungeon.Objetos;
using SquareDungeon.Armas;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    abstract class Jugador : Mob
    {
        private int pvAnt;
        private int fueAnt;
        private int magAnt;
        private int agiAnt;
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
        private byte defCrec;
        private byte resCrec;
        private byte probCritCrec;
        private byte danCritCrec;

        protected Arma armaCombate;

        protected Arma[] armas;

        protected List<Habilidad> habilidades;

        protected Objeto[] objetos;

        protected Jugador(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            byte pvCrec, byte fueCrec, byte magCrec, byte agiCrec, byte defCrec, byte resCrec,
            byte probCritCrec, byte danCritCrec,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax,
            int probCritMax, int danCritMax,
            string nombre, string descripcion, Habilidad habilidad) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax, nombre, descripcion)
        {
            this.pvCrec = pvCrec;
            this.fueCrec = fueCrec;
            this.magCrec = magCrec;
            this.agiCrec = agiCrec;
            this.defCrec = defCrec;
            this.resCrec = resCrec;
            this.probCritCrec = probCritCrec;
            this.danCritCrec = danCritCrec;

            pvAnt = pv;
            fueAnt = fue;
            magAnt = mag;
            agiAnt = agi;
            defAnt = def;
            resAnt = res;
            probCritAnt = probCrit;
            danCritAnt = danCrit;
            nivelAnt = 1;
            expAnt = 0;

            armas = new Arma[4];

            habilidades = new List<Habilidad>();
            habilidades.Add(habilidad);

            objetos = new Objeto[15];
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

        public override bool SubirNivel(int exp)
        {
            expAnt = this.exp;
            return base.SubirNivel(exp);
        }

        public int[,] GetStatsNuevos() => new int[,]
        { { pvAnt, pvTotal}, { fueAnt, fue}, { magAnt, mag}, { agiAnt, agi}, {defAnt, def }, {resAnt, res },
            {probCritAnt, probCrit }, {danCritAnt, danCrit }, {expAnt, exp }, {nivelAnt, nivel } };

        public abstract bool EquiparArma(Arma arma);

        public void EliminarArma(Arma arma)
        {
            Arma[] armas = new Arma[this.armas.Length];
            for (int i = 0; i < this.armas.Length; i++)
            {
                if (this.armas[i] == null)
                    continue;

                if (this.armas[i] != arma)
                    armas[i] = this.armas[i];
            }

            this.armas = armas;
        }

        public abstract Arma[] GetArmas();

        public void SetArmaCombate(Arma arma)
        {
            armaCombate = arma;
        }

        public abstract Arma GetArmaCombate();

        public List<Habilidad> GetHabilidades() => habilidades;

        public List<Habilidad> GetHabilidadesPorTipo(int tipo)
        {
            List<Habilidad> habilidades = new List<Habilidad>();

            foreach (Habilidad habilidad in this.habilidades)
            {
                if (habilidad.GetTipoHabilidad() == tipo)
                    habilidades.Add(habilidad);
            }

            return habilidades;
        }

        public bool AnadirHabilidad(Habilidad habilidad)
        {
            bool contiene = false;
            foreach (Habilidad hab in habilidades)
            {
                if (hab.GetNombre().Equals(habilidad.GetNombre()))
                    contiene = true;
            }

            if (!contiene)
                habilidades.Add(habilidad);

            return !contiene;
        }

        public bool AnadirObjeto(Objeto objeto)
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

        public void EliminarObjeto(Objeto objeto)
        {
            Objeto[] objetos = new Objeto[this.objetos.Length];
            for (int i = 0; i < this.objetos.Length; i++)
            {
                if (this.objetos[i] == null)
                    continue;

                if (this.objetos[i].GetNombre() != objeto.GetNombre())
                    objetos[i] = this.objetos[i];
            }

            this.objetos = objetos;
        }

        public Objeto[] GetObjetos() => objetos;
    }
}
