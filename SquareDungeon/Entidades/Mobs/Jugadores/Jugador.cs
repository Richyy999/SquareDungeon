using System;
using SquareDungeon.Armas;

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

        private byte pvCrec;
        private byte fueCrec;
        private byte magCrec;
        private byte agiCrec;
        private byte defCrec;
        private byte resCrec;
        private byte probCritCrec;
        private byte danCritCrec;

        private int nivelAnt;
        private int expAnt;

        private Arma[] armas;

        public Jugador(byte pvCrec, byte fueCrec, byte magCrec, byte agiCrec, byte defCrec, byte resCrec,
            byte probCritCrec, byte danCritCerc,
            int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit)
        {
            this.pvCrec = pvCrec;
            this.fueCrec = fueCrec;
            this.magCrec = magCrec;
            this.agiCrec = agiCrec;
            this.defCrec = defCrec;
            this.resCrec = resCrec;
            this.probCritCrec = probCritCrec;
            this.danCritCrec = danCritCerc;

            pvAnt = 0;
            fueAnt = 0;
            magAnt = 0;
            agiAnt = 0;
            defAnt = 0;
            resAnt = 0;
            probCritAnt = 0;
            danCritAnt = 0;

            nivelAnt = 0;
            expAnt = 0;

            armas = new Arma[4];
        }

        protected override void subirNivel()
        {
            Random random = new Random();
            if (subirStat(pvCrec))
            {
                pvAnt = pv;
                pv += random.Next(1, 2);
            }
            if (subirStat(fueCrec))
            {
                fueAnt = fue;
                fue += random.Next(1, 2);
            }
            if (subirStat(magCrec))
            {
                magAnt = mag;
                mag += random.Next(1, 2);
            }
            if (subirStat(agiCrec))
            {
                agiAnt = agi;
                agi += random.Next(1, 2);
            }
            if (subirStat(defCrec))
            {
                defAnt = def;
                def += random.Next(1, 2);
            }
            if (subirStat(resCrec))
            {
                resAnt = res;
                res += random.Next(1, 2);
            }
            if (subirStat(probCritCrec))
            {
                probCritAnt = probCrit;
                probCrit += random.Next(1, 2);
            }
            if (subirStat(danCritCrec))
            {
                danCritAnt = danCrit;
                danCrit += random.Next(2, 4);
            }
        }

        public int[,] GetStats() => new int[,]
        { { pvAnt, pv}, { fueAnt, fue}, { magAnt, mag}, { agiAnt, agi}, {defAnt, def }, {resAnt, res },
            {probCritAnt, probCrit }, {danCritAnt, danCrit }, {expAnt, exp }, {nivelAnt, nivel } };

        public virtual bool EquiparArma(Arma arma) => false;

        public void EliminarArma(Arma arma)
        {
            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] == arma)
                    armas[i] = null;
            }
        }

        public Arma[] GetArmas() => armas;
    }
}
