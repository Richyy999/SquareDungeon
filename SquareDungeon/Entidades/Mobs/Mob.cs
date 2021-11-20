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
        public const int INDICE_VIDA_TOTAL = 8;

        protected readonly int pvMax;
        protected readonly int fueMax;
        protected readonly int magMax;
        protected readonly int agiMax;
        protected readonly int defMax;
        protected readonly int resMax;
        protected readonly int probCritMax;
        protected readonly int danCritMax;

        protected int pv;
        protected int pvTotal;
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
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion) : base(nombre, descripcion)
        {
            this.pv = pv;
            pvTotal = pv;
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

        public virtual bool SubirNivel(int exp)
        {
            bool suboNivel = false;

            if (exp < 0)
                throw new ArgumentOutOfRangeException("exp", "No se puede recibir una cantidad negativa de experiencia");

            if (nivel < NIVEL_MAX)
            {
                if (this.exp + exp >= EXP_MAX)
                {
                    suboNivel = true;
                    nivel++;
                    subirNivel();
                    int expActual = this.exp;
                    this.exp = 0;
                    SubirNivel((expActual + exp) - EXP_MAX);
                }
                else
                    this.exp += exp;
            }

            return suboNivel;
        }

        protected bool puedeSubirStat(byte statCrec)
        {
            Random random = new Random();
            int num = random.Next(101);

            return num <= statCrec;
        }

        public void SubirStat(int indice, int valor)
        {
            if (valor <= 0)
                throw new ArgumentException("valor", "El valor para subir un stat debe ser mayor que 0");

            switch (indice)
            {
                case INDICE_VIDA:
                    if (pv + valor <= pvTotal)
                        pv += valor;
                    else
                        pv = pvTotal;
                    break;

                case INDICE_VIDA_TOTAL:
                    if (pvTotal + valor <= pvMax)
                        pvTotal += valor;
                    else
                        pvTotal = pvMax;
                    break;

                case INDICE_FUERZA:
                    if (fue + valor <= fueMax)
                        fue += valor;
                    else
                        fue = fueMax;
                    break;

                case INDICE_MAGIA:
                    if (mag + valor <= magMax)
                        mag += valor;
                    else
                        mag = magMax;
                    break;

                case INDICE_AGILIDAD:
                    if (agi + valor <= agiMax)
                        agi += valor;
                    else
                        agi = agiMax;
                    break;

                case INDICE_DEFENSA:
                    if (def + valor <= defMax)
                        def += valor;
                    else
                        def = defMax;
                    break;

                case INDICE_RESISTENCIA:
                    if (res + valor <= resMax)
                        res += valor;
                    else
                        res = resMax;
                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    if (probCrit + valor <= probCritMax)
                        probCrit += valor;
                    else
                        probCrit = probCritMax;
                    break;

                case INDICE_DANO_CRITICO:
                    if (danCrit + valor <= danCritMax)
                        danCrit += valor;
                    else
                        danCrit = danCritMax;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("indice",
                        $"Se ha recibido el índice {indice}, debe estar entre 0 y 7. Utiliza las constantes de clase");
            }
        }

        protected void subirStat(int indice, int valor, int max)
        {
            if (valor <= 0)
                throw new ArgumentException("valor", "El valor para subir un stat debe ser mayor que 0");

            switch (indice)
            {
                case INDICE_VIDA_TOTAL:
                    if (pv + valor <= max)
                        pvTotal += valor;
                    else
                        pvTotal = max;
                    break;

                case INDICE_FUERZA:
                    if (fue + valor <= max)
                        fue += valor;
                    else
                        fue = max;
                    break;

                case INDICE_MAGIA:
                    if (mag + valor <= max)
                        mag += valor;
                    else
                        mag = max;
                    break;

                case INDICE_AGILIDAD:
                    if (agi + valor <= max)
                        agi += valor;
                    else
                        agi = max;
                    break;

                case INDICE_DEFENSA:
                    if (def + valor <= max)
                        def += valor;
                    else
                        def = max;
                    break;

                case INDICE_RESISTENCIA:
                    if (res + valor <= max)
                        res += valor;
                    else
                        res = max;
                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    if (probCrit + valor <= max)
                        probCrit += valor;
                    else
                        probCrit = max;
                    break;

                case INDICE_DANO_CRITICO:
                    if (danCrit + valor <= max)
                        danCrit += valor;
                    else
                        danCrit = max;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("indice",
                        $"Se ha recibido el índice {indice}, utiliza las constante sde clase");
            }
        }

        public int GetStat(int indice)
        {
            switch (indice)
            {
                case INDICE_VIDA:
                    return pv;

                case INDICE_VIDA_TOTAL:
                    return pvTotal;

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

        public virtual int[] GetStats() => new int[] { pvTotal, pv, fue, mag, agi, def, res, probCrit, danCrit, exp, nivel };

        protected int[] getStatsMax() =>
            new int[] { pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax };

        public void AlterarStatCombate(int indice, int valor)
        {
            switch (indice)
            {
                case INDICE_FUERZA:
                    fueCom += valor;
                    if (fueCom <= 0)
                        fueCom = 0;

                    break;

                case INDICE_MAGIA:
                    magCom += valor;
                    if (magCom <= 0)
                        magCom = 0;

                    break;

                case INDICE_AGILIDAD:
                    agiCom += valor;
                    if (agiCom <= 0)
                        agiCom = 0;

                    break;

                case INDICE_DEFENSA:
                    defCom += valor;
                    if (defCom <= 0)
                        defCom = 0;

                    break;

                case INDICE_RESISTENCIA:
                    resCom += valor;
                    if (resCom <= 0)
                        resCom = 0;

                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    probCritCom += valor;
                    if (probCritCom <= 0)
                        probCritCom = 0;

                    break;

                case INDICE_DANO_CRITICO:
                    danCritCom += valor;
                    if (danCritCom <= 0)
                        danCritCom = 0;

                    break;

                default:
                    throw new ArgumentException("indice",
                    $"Se ha recibido un índice incorrecto. Utiliza las constantes de clase");
            }
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

        public int GetCritico()
        {
            Random random = new Random();
            if (random.Next(101) <= probCritCom)
                return danCritCom / 100;
            else
                return 0;
        }

        public bool Danar(int dano)
        {
            if (dano < 0)
                throw new ArgumentException("dano", "EL daño no puede ser inferior a 0");

            pv -= dano;

            return pv <= 0;
        }

        public int GetNivel() => nivel;
    }
}
