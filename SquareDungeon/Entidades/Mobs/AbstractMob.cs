using System;
using System.Collections.Generic;

using SquareDungeon.Habilidades;

namespace SquareDungeon.Entidades.Mobs
{
    abstract class AbstractMob : AbstractEntidad
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
        public const int INDICE_HABILIDAD = 9;

        protected readonly int pvMax;
        protected readonly int fueMax;
        protected readonly int magMax;
        protected readonly int agiMax;
        protected readonly int habMax;
        protected readonly int defMax;
        protected readonly int resMax;
        protected readonly int probCritMax;
        protected readonly int danCritMax;

        protected int pv;
        protected int pvTotal;
        protected int fue;
        protected int mag;
        protected int agi;
        protected int hab;
        protected int def;
        protected int res;
        protected int probCrit;
        protected int danCrit;

        protected int nivel;
        protected int exp;

        protected int fueCom;
        protected int magCom;
        protected int agiCom;
        protected int habCom;
        protected int defCom;
        protected int resCom;
        protected int probCritCom;
        protected int danCritCom;

        protected List<AbstractHabilidad> habilidades;

        protected AbstractMob(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion) : base(nombre, descripcion)
        {
            this.pv = pv;
            this.pvTotal = pv;
            this.fue = fue;
            this.mag = mag;
            this.agi = agi;
            this.hab = hab;
            this.def = def;
            this.res = res;
            this.probCrit = probCrit;
            this.danCrit = danCrit;

            this.pvMax = pvMax;
            this.fueMax = fueMax;
            this.magMax = magMax;
            this.agiMax = agiMax;
            this.habMax = habMax;
            this.defMax = defMax;
            this.resMax = resMax;
            this.probCritMax = probCritMax;
            this.danCritMax = danCritMax;

            this.fueCom = fue;
            this.magCom = mag;
            this.agiCom = agi;
            this.habCom = hab;
            this.defCom = def;
            this.resCom = res;
            this.probCritCom = probCrit;
            this.danCritCom = danCrit;

            this.nivel = 1;
            this.exp = 0;

            this.habilidades = new List<AbstractHabilidad>();
        }

        public virtual int[] GetStats() => new int[] { pvTotal, pv, fue, mag, agi, hab, def, res, probCrit, danCrit, exp, nivel };

        public virtual bool SubirNivel(int exp)
        {
            bool suboNivel = false;

            if (exp < 0)
                throw new ArgumentOutOfRangeException("exp",
                    "No se puede recibir una cantidad negativa de experiencia");

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

        protected abstract void subirNivel();

        public void SubirStat(int indice, int valor)
        {
            if (valor <= 0)
                throw new ArgumentException("valor", "El valor para subir un stat debe ser mayor que 0");

            switch (indice)
            {
                case INDICE_VIDA:
                    if (this.pv + valor <= this.pvTotal)
                        this.pv += valor;
                    else
                        this.pv = this.pvTotal;
                    break;

                case INDICE_VIDA_TOTAL:
                    if (this.pvTotal + valor <= this.pvMax)
                        this.pvTotal += valor;
                    else
                        this.pvTotal = this.pvMax;
                    break;

                case INDICE_FUERZA:
                    if (this.fue + valor <= this.fueMax)
                        this.fue += valor;
                    else
                        this.fue = this.fueMax;
                    break;

                case INDICE_MAGIA:
                    if (this.mag + valor <= this.magMax)
                        this.mag += valor;
                    else
                        this.mag = this.magMax;
                    break;

                case INDICE_AGILIDAD:
                    if (this.agi + valor <= this.agiMax)
                        this.agi += valor;
                    else
                        this.agi = this.agiMax;
                    break;

                case INDICE_HABILIDAD:
                    if (this.hab + valor <= this.habMax)
                        this.hab += valor;
                    else
                        this.hab = this.habMax;
                    break;

                case INDICE_DEFENSA:
                    if (this.def + valor <= this.defMax)
                        this.def += valor;
                    else
                        this.def = this.defMax;
                    break;

                case INDICE_RESISTENCIA:
                    if (this.res + valor <= this.resMax)
                        this.res += valor;
                    else
                        this.res = this.resMax;
                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    if (this.probCrit + valor <= this.probCritMax)
                        this.probCrit += valor;
                    else
                        this.probCrit = this.probCritMax;
                    break;

                case INDICE_DANO_CRITICO:
                    if (this.danCrit + valor <= this.danCritMax)
                        this.danCrit += valor;
                    else
                        this.danCrit = this.danCritMax;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("indice",
                        $"Se ha recibido el índice {indice}, debe estar entre 0 y 7. Utiliza las constantes de clase");
            }
        }

        public int GetStat(int indice)
        {
            switch (indice)
            {
                case INDICE_VIDA:
                    return this.pv;

                case INDICE_VIDA_TOTAL:
                    return this.pvTotal;

                case INDICE_FUERZA:
                    return this.fue;

                case INDICE_MAGIA:
                    return this.mag;

                case INDICE_AGILIDAD:
                    return this.agi;

                case INDICE_HABILIDAD:
                    return this.hab;

                case INDICE_DEFENSA:
                    return this.def;

                case INDICE_RESISTENCIA:
                    return this.res;

                case INDICE_PROBABILIDAD_CRITICO:
                    return this.probCrit;

                case INDICE_DANO_CRITICO:
                    return this.danCrit;

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
                    return this.fueCom;

                case INDICE_MAGIA:
                    return this.magCom;

                case INDICE_AGILIDAD:
                    return this.agiCom;

                case INDICE_HABILIDAD:
                    return this.habCom;

                case INDICE_DEFENSA:
                    return this.defCom;

                case INDICE_RESISTENCIA:
                    return this.resCom;

                case INDICE_PROBABILIDAD_CRITICO:
                    return this.probCritCom;

                case INDICE_DANO_CRITICO:
                    return this.danCritCom;

                default:
                    throw new ArgumentException("indice",
                    $"Se ha recibido un índice incorrecto. Utiliza las constantes de clase");
            }
        }

        public void AlterarStatCombate(int indice, int valor)
        {
            switch (indice)
            {
                case INDICE_FUERZA:
                    this.fueCom += valor;
                    if (this.fueCom <= 0)
                        this.fueCom = 0;

                    break;

                case INDICE_MAGIA:
                    this.magCom += valor;
                    if (this.magCom <= 0)
                        this.magCom = 0;

                    break;

                case INDICE_AGILIDAD:
                    this.agiCom += valor;
                    if (this.agiCom <= 0)
                        this.agiCom = 0;

                    break;

                case INDICE_HABILIDAD:
                    this.habCom += valor;
                    if (this.habCom <= 0)
                        this.habCom = 0;

                    break;

                case INDICE_DEFENSA:
                    this.defCom += valor;
                    if (this.defCom <= 0)
                        this.defCom = 0;

                    break;

                case INDICE_RESISTENCIA:
                    this.resCom += valor;
                    if (this.resCom <= 0)
                        this.resCom = 0;

                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    this.probCritCom += valor;
                    if (this.probCritCom <= 0)
                        this.probCritCom = 0;

                    break;

                case INDICE_DANO_CRITICO:
                    this.danCritCom += valor;
                    if (this.danCritCom <= 0)
                        this.danCritCom = 0;

                    break;

                default:
                    throw new ArgumentException("indice",
                    $"Se ha recibido un índice incorrecto. Utiliza las constantes de clase");
            }
        }

        public void ReiniciarStatsCombate()
        {
            this.fueCom = this.fue;
            this.magCom = this.mag;
            this.agiCom = this.agi;
            this.habCom = this.hab;
            this.defCom = this.def;
            this.resCom = this.res;
            this.probCritCom = this.probCrit;
            this.danCritCom = this.danCrit;
        }

        public int GetCritico()
        {
            Random random = new Random();
            if (random.Next(101) <= this.probCritCom)
                return this.danCritCom / 100;
            else
                return 0;
        }

        public bool Danar(int dano)
        {
            if (dano < 0)
                throw new ArgumentException("dano", "EL daño no puede ser inferior a 0");

            this.pv -= dano;

            return this.pv <= 0;
        }

        public int GetNivel() => nivel;

        public List<AbstractHabilidad> GetHabilidades() => habilidades;

        protected void subirStat(int indice, int valor, int max)
        {
            if (valor <= 0)
                throw new ArgumentException("valor", "El valor para subir un stat debe ser mayor que 0");

            switch (indice)
            {
                case INDICE_VIDA_TOTAL:
                    if (this.pv + valor <= max)
                        this.pvTotal += valor;
                    else
                        this.pvTotal = max;
                    break;

                case INDICE_FUERZA:
                    if (this.fue + valor <= max)
                        this.fue += valor;
                    else
                        this.fue = max;
                    break;

                case INDICE_MAGIA:
                    if (this.mag + valor <= max)
                        this.mag += valor;
                    else
                        this.mag = max;
                    break;

                case INDICE_AGILIDAD:
                    if (this.agi + valor <= max)
                        this.agi += valor;
                    else
                        this.agi = max;
                    break;

                case INDICE_HABILIDAD:
                    if (this.hab + valor <= max)
                        this.hab += valor;
                    else
                        this.hab = max;

                    break;

                case INDICE_DEFENSA:
                    if (this.def + valor <= max)
                        this.def += valor;
                    else
                        this.def = max;
                    break;

                case INDICE_RESISTENCIA:
                    if (this.res + valor <= max)
                        this.res += valor;
                    else
                        this.res = max;
                    break;

                case INDICE_PROBABILIDAD_CRITICO:
                    if (this.probCrit + valor <= max)
                        this.probCrit += valor;
                    else
                        this.probCrit = max;
                    break;

                case INDICE_DANO_CRITICO:
                    if (this.danCrit + valor <= max)
                        this.danCrit += valor;
                    else
                        this.danCrit = max;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("indice",
                        $"Se ha recibido el índice {indice}, utiliza las constante sde clase");
            }
        }

        protected bool puedeSubirStat(byte statCrec)
        {
            Random random = new Random();
            int num = random.Next(101);

            return num <= statCrec;
        }
    }
}
