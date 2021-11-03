using System;

namespace SquareDungeon.Entidades.Mobs
{
    abstract class Mob : Entidad
    {
        public const int EXP_MAX = 100;
        public const int NIVEL_MAX = 50;

        public const int INDICE_FUERZA = 0;
        public const int INDICE_MAGIA = 1;
        public const int INDICE_AGILIDAD = 2;
        public const int INDICE_DEFENSA = 3;
        public const int INDICE_RESISTENCIA = 4;
        public const int INDICE_PROBABILIDAD_CRITICO = 5;
        public const int INDICE_DANO_CRITICO = 6;
        public const int INDICE_VIDA = 7;

        protected readonly int pvMax;
        protected readonly int fueMax;
        protected readonly int magMax;
        protected readonly int agiMax;
        protected readonly int defMax;
        protected readonly int resMax;
        protected readonly int probCritMax;
        protected readonly int danCritMax;

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

        protected int fueCom;
        protected int magCom;
        protected int agiCom;
        protected int defCom;
        protected int resCom;
        protected int probCritCom;
        protected int danCritCom;

        protected Mob(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax)
        {
            this.pv = pv;
            this.fue = fue;
            this.mag = mag;
            this.agi = agi;
            this.def = def;
            this.res = res;
            this.probCrit = probCrit;
            this.danCrit = danCrit;

            this.pvMax = pvMax;
            this.fueMax = fueMax;
            this.magMax = magMax;
            this.agiMax = agiMax;
            this.defMax = defMax;
            this.resMax = resMax;
            this.probCritMax = probCritMax;
            this.danCritMax = danCritMax;

            fueCom = fue;
            magCom = mag;
            agiCom = agi;
            defCom = def;
            resCom = res;
            probCritCom = probCrit;
            danCritCom = danCrit;

            nivel = 1;
            exp = 0;
        }

        protected abstract void subirNivel();

        public virtual void SubirNivel(int exp)
        {
            if (exp < 0)
                throw new ArgumentOutOfRangeException("exp", "No se puede recibir una cantidad negativa de experiencia");

            if (nivel < NIVEL_MAX)
            {
                if (this.exp + exp >= Mob.EXP_MAX)
                {
                    subirNivel();
                    this.exp = 0;
                    SubirNivel((this.exp + exp) - Mob.EXP_MAX);
                }
                else
                    this.exp += exp;
            }
        }

        protected bool puedeSubirStat(byte statCrec)
        {
            Random random = new Random();
            int num = random.Next(0, 101);

            return num <= statCrec;
        }

        protected void subirStat(int indice, int valor, int max)
        {
            if (valor <= 0)
                throw new ArgumentException("valor", "El valor para subir un stat debe ser mayor que 0");

            int stat = GetStat(indice);

            if (stat + valor <= max)
                stat += valor;
            else
                stat = max;
        }

        public int GetStat(int indice)
        {
            switch (indice)
            {
                case INDICE_VIDA:
                    return pv;

                case INDICE_FUERZA:
                    return fue;

                case INDICE_MAGIA:
                    return mag;

                case INDICE_AGILIDAD:
                    return agi;

                case INDICE_DEFENSA:
                    return def;

                case INDICE_RESISTENCIA:
                    return res;

                case INDICE_PROBABILIDAD_CRITICO:
                    return probCrit;

                case INDICE_DANO_CRITICO:
                    return danCrit;

                default:
                    throw new ArgumentException("indice",
                    $"Se ha recibido un índice incorrecto. Utiliza las constantes de clase");
            }
        }

        public int GetStatCombate(int indice)
        {
            switch (indice)
            {
                case INDICE_FUERZA:
                    return fueCom;

                case INDICE_MAGIA:
                    return magCom;

                case INDICE_AGILIDAD:
                    return agiCom;

                case INDICE_DEFENSA:
                    return defCom;

                case INDICE_RESISTENCIA:
                    return resCom;

                case INDICE_PROBABILIDAD_CRITICO:
                    return probCritCom;

                case INDICE_DANO_CRITICO:
                    return danCritCom;

                default:
                    throw new ArgumentException("indice",
                    $"Se ha recibido un índice incorrecto. Utiliza las constantes de clase");
            }
        }

        public virtual int[] GetStats() => new int[] { pv, fue, mag, agi, def, res, probCrit, danCrit, exp, nivel };

        public void SetStatCombate(int indice, int valor)
        {
            int stat = GetStatCombate(indice);
            stat += valor;
        }

        public void ReiniciarStatsCombate()
        {
            fueCom = fue;
            magCom = mag;
            agiCom = agi;
            defCom = def;
            resCom = res;
            probCritCom = probCrit;
            danCritCom = danCrit;
        }

        public bool Danar(int dano)
        {
            pv -= dano;

            return pv <= 0;
        }
    }
}
