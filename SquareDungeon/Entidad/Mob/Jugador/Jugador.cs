namespace SquareDungeon.Entidad.Mob.Jugador
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

        private Arma.Arma[] armas;

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

            armas = new Arma.Arma[4];
        }

        public virtual bool EquiparArma(Arma.Arma arma) => false;

        public void EliminarArma(Arma.Arma arma)
        {
            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] == arma)
                    armas[i] = null;
            }
        }

        public Arma.Arma[] GetArmas() => armas;
    }
}
