using System;
using System.Collections.Generic;

using SquareDungeon.Modelo;
using SquareDungeon.Habilidades;
using SquareDungeon.Efectos;

namespace SquareDungeon.Entidades.Mobs
{
    /// <summary>
    /// Mob que se encuentra en una sala.<br/>Puede ser un <see cref="Mobs.Jugadores.AbstractJugador">jugador</see> o un <see cref=" Mobs.Enemigos.AbstractEnemigo">enemigo</see>
    /// </summary>
    abstract class AbstractMob : AbstractEntidad
    {
        /// <summary>
        /// Cantidad de experiencia necesaria para que un mob suba de nivel
        /// </summary>
        public const int EXP_MAX = 100;
        /// <summary>
        /// Nivel máximo que puede alcanzar un mob
        /// </summary>
        public const int NIVEL_MAX = 50;

        /// <summary>
        /// Índice para obtener el stat de fuerza
        /// </summary>
        public const int INDICE_FUERZA = 0;
        /// <summary>
        /// Índice para obtener el stat de magia
        /// </summary>
        public const int INDICE_MAGIA = 1;
        /// <summary>
        /// Índice para obtener el stat de agilidad
        /// </summary>
        public const int INDICE_AGILIDAD = 2;
        /// <summary>
        /// Índice para obtener el stat de defensa
        /// </summary>
        public const int INDICE_DEFENSA = 3;
        /// <summary>
        /// Índice para obtener el stat de resistencia mágica
        /// </summary>
        public const int INDICE_RESISTENCIA = 4;
        /// <summary>
        /// Índice para obtener el stat de probabilidad de críitico
        /// </summary>
        public const int INDICE_PROBABILIDAD_CRITICO = 5;
        /// <summary>
        /// Índice para obtener el stat de daño crítico
        /// </summary>
        public const int INDICE_DANO_CRITICO = 6;
        /// <summary>
        /// Índice para obtener el stat de vida
        /// </summary>
        public const int INDICE_VIDA = 7;
        /// <summary>
        /// Índice para obtener el stat de total
        /// </summary>
        public const int INDICE_VIDA_TOTAL = 8;
        /// <summary>
        /// Índice para obtener el stat de habilidad
        /// </summary>
        public const int INDICE_HABILIDAD = 9;

        /// <summary>
        /// Vida máxima del mob
        /// </summary>
        protected readonly int pvMax;
        /// <summary>
        /// Fuerza máxima del mob
        /// </summary>
        protected readonly int fueMax;
        /// <summary>
        /// Magia máxima del mob
        /// </summary>
        protected readonly int magMax;
        /// <summary>
        /// Agilidad máxima del mob
        /// </summary>
        protected readonly int agiMax;
        /// <summary>
        /// Habilidad máxima del mob
        /// </summary>
        protected readonly int habMax;
        /// <summary>
        /// Defensa máxima del mob
        /// </summary>
        protected readonly int defMax;
        /// <summary>
        /// Resistencia mágica máxima del mob
        /// </summary>
        protected readonly int resMax;
        /// <summary>
        /// Probabilidad de crítico máxima del mob
        /// </summary>
        protected readonly int probCritMax;
        /// <summary>
        /// Daño crítico máximo del mob
        /// </summary>
        protected readonly int danCritMax;

        /// <summary>
        /// Vida actual del mob.<br/>Si llega a 0 el mob muere
        /// </summary>
        protected int pv;
        /// <summary>
        /// Vida total del mob
        /// </summary>
        protected int pvTotal;
        /// <summary>
        /// Fuerza actual del mob.<br/>Afecta al daño con ataques físicos
        /// </summary>
        protected int fue;
        /// <summary>
        /// Magia actual del mob.<br/>Afecta al daño con ataques mágicos
        /// </summary>
        protected int mag;
        /// <summary>
        /// Agilidad actual del mob.<br/>Si supera en 4 o más a la agilidad del enemigo el mob realiza un ataque doble
        /// </summary>
        protected int agi;
        /// <summary>
        /// Habilidad actual del mob
        /// </summary>
        protected int hab;
        /// <summary>
        /// Defensa actual del mob.<br/>Reduce el daño de ataques físicos
        /// </summary>
        protected int def;
        /// <summary>
        /// Resistencia mágica actual del mob.<br/>Reduce el daño de ataques mágicos
        /// </summary>
        protected int res;
        /// <summary>
        /// Probabilidad de crítico actual del mob.<br/>Indica las podsibilidades de ejecutar un golpe crítico
        /// </summary>
        protected int probCrit;
        /// <summary>
        /// Daño crítico actual del mob.<br/>Aumenta el daño de los ataques críticos
        /// </summary>
        protected int danCrit;

        /// <summary>
        /// Nivel actual del mob
        /// </summary>
        protected int nivel;
        /// <summary>
        /// Experiencia actual del mob
        /// </summary>
        protected int exp;

        /// <summary>
        /// Fuerza del mob en combate
        /// </summary>
        protected int fueCom;
        /// <summary>
        /// Magia del mob en combate
        /// </summary>
        protected int magCom;
        /// <summary>
        /// Agilidad del mob en combate
        /// </summary>
        protected int agiCom;
        /// <summary>
        /// Habilidad del mob en combate
        /// </summary>
        protected int habCom;
        /// <summary>
        /// Defensa del mob en combate
        /// </summary>
        protected int defCom;
        /// <summary>
        /// Resistencia mágica del mob en combate
        /// </summary>
        protected int resCom;
        /// <summary>
        /// Probabilidad de crítico del mob en combate
        /// </summary>
        protected int probCritCom;
        /// <summary>
        /// Daño crítico del mob en combate
        /// </summary>
        protected int danCritCom;

        /// <summary>
        /// habilidades que posee el mob
        /// </summary>
        protected List<AbstractHabilidad> habilidades;

        /// <summary>
        /// Lista con los efectos aplicados al mob
        /// </summary>
        private List<AbstractEfecto> efectos;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="pv">Vida inicial del mob</param>
        /// <param name="fue">Fuerza inicial del mob</param>
        /// <param name="mag">Magia inicial del mob</param>
        /// <param name="agi">Agilidad inicial del mob</param>
        /// <param name="hab">Habilidad inicial del mob</param>
        /// <param name="def">Defensa inicial del mob</param>
        /// <param name="res">Resistencia mágica inicial del mob</param>
        /// <param name="probCrit">Probabilidad de crítico inicial del mob</param>
        /// <param name="danCrit">Daño crítico inicial del mob</param>
        /// <param name="pvMax">Vida máxima del mob</param>
        /// <param name="fueMax">Fuerza máxima del mob</param>
        /// <param name="magMax">Magia máxima del mob</param>
        /// <param name="agiMax">Agilidad máxima del mob</param>
        /// <param name="habMax">habilidad máxima del mob</param>
        /// <param name="defMax">Defensa máxima del mob</param>
        /// <param name="resMax">Resistencia mágica máxima del mob</param>
        /// <param name="probCritMax">Probabilidad de crítico máxima del mob</param>
        /// <param name="danCritMax">Daño crítico máximo del mob</param>
        /// <param name="nombre">Nombre del mob</param>
        /// <param name="descripcion">Descripción del mob</param>
        /// <param name="statsMaximos">Suma de los stats iniciales máximos</param>
        protected AbstractMob(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int statsMaximos) : base(nombre, descripcion)
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

            this.efectos = new List<AbstractEfecto>();

            validarStats(statsMaximos);
        }

        /// <summary>
        /// Devuelve un array con los stats actuales del mob
        /// </summary>
        /// <returns></returns>
        public virtual int[] GetStats() => new int[] { pvTotal, pv, fue, mag, agi, hab, def, res, probCrit, danCrit, exp, nivel };

        /// <summary>
        /// Añade experiencia al mob y si supera la <see cref="EXP_MAX">experiencia máxima</see> del mob, sube de nivel
        /// </summary>
        /// <param name="exp">Experiencia a añadir</param>
        /// <returns>true si el mob ha subido de nivel, false en caso contrario</returns>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si la experiencia ganada es inferior a 0</exception>
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

        /// <summary>
        /// Aumenta los stats del mob al subir de nivel
        /// </summary>
        protected abstract void subirNivel();

        /// <summary>
        /// Aumenta un stat en la cantidad indicada sin superar el valor máximo de dicho stat del mob
        /// </summary>
        /// <param name="indice">Índice del stat a aumentar</param>
        /// <param name="valor">Cantidad en la que el stat aumentará</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el valor a aumentar es menor o igual a 0</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si el índice indicado no es correcto</exception>
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

        /// <summary>
        /// Devuelve el stat actual del mob
        /// </summary>
        /// <param name="indice">Índice del stat a obtener</param>
        /// <returns>Stat actual del mob correspondiente al índice indicado</returns>
        /// <exception cref="ArgumentException">lanza una excepción si el índice no es correcto</exception>
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

        /// <summary>
        /// Devuelve el stat en combate del mob
        /// </summary>
        /// <param name="indice">Índice del stat a obtener</param>
        /// <returns>Stat en combate del mob correspondiente al índice indicado</returns>
        /// <exception cref="ArgumentException">lanza una excepción si el índice no es correcto</exception>
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

        /// <summary>
        /// Altera un stat durante el combate
        /// </summary>
        /// <param name="indice">Índice del stat a alterar</param>
        /// <param name="valor">Valor a modificar</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el índice no es correcto</exception>
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

        /// <summary>
        /// Establece el valor de los stats de combate al valor de los stats actuales
        /// </summary>
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

        /// <summary>
        /// Obtiene el daño crítico en función de la probabilidad de crítico
        /// </summary>
        /// <returns>El valor del daño crítico, 0 si no se ha ejecutado ningún crítico</returns>
        public int GetCritico()
        {
            if (Util.Probabilidad(this.probCritCom))
                return this.danCritCom / 100;
            else
                return 0;
        }

        /// <summary>
        /// Daña al mob
        /// </summary>
        /// <param name="dano">Daño infligido</param>
        /// <returns>true si el mob ha muerto, false en caso contrario</returns>
        /// <exception cref="ArgumentException">Lanza una excepción si el daño es negativo</exception>
        public bool Danar(int dano)
        {
            if (dano < 0)
                throw new ArgumentException("El daño no puede ser inferior a 0");

            this.pv -= dano;

            bool muerto = this.pv <= 0;

            if (this.pv < 0)
                this.pv = 0;

            return muerto;
        }

        /// <summary>
        /// Daña al mob sin matarle, dejándole con 1 pv si el daño es letal
        /// </summary>
        /// <param name="dano">Daño a infligir</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el daño es negativo</exception>
        public void DanarSinMatar(int dano)
        {
            if (dano < 0)
                throw new ArgumentException("El daño no puede ser inferior a 0");

            this.pv -= dano;

            if (this.pv < 1)
                this.pv = 1;
        }

        /// <summary>
        /// Esquiva el ataque de otro mob
        /// </summary>
        /// <param name="atacante">Mob atacante</param>
        /// <returns>true si el mob ha esquivado el ataque, false en caso contrario</returns>
        public bool Esquivar(AbstractMob atacante)
        {
            int agi = this.agiCom;
            int hab = this.habCom;

            int agiAtacante = atacante.agiCom;
            int difAgi = agi - agiAtacante;

            if (difAgi < 0)
                difAgi = 0;

            int destreza = (int)((difAgi + hab) / 2);

            return Util.Probabilidad(destreza);
        }

        /// <summary>
        /// Añade un efecto al mob
        /// </summary>
        /// <param name="efecto">Efecto a añadir</param>
        public void AnadirEfecto(AbstractEfecto efecto)
        {
            this.efectos.Add(efecto);
        }

        /// <summary>
        /// Elimina todos los efectos del mob
        /// </summary>
        public void EliminarEfectos()
        {
            this.efectos.Clear();
        }

        /// <summary>
        /// Aplica los efectos al mob
        /// </summary>
        public void AplicarEfectos()
        {
            foreach (AbstractEfecto efecto in this.efectos)
            {
                if (efecto.EsAplicarEfecto())
                {
                    efecto.AplicarEfecto(this);
                    efecto.MostrarMensaje(this);
                }
            }
        }

        /// <summary>
        /// Devuelve el nivel actual del mob
        /// </summary>
        /// <returns>Nivel actual del mob</returns>
        public int GetNivel() => nivel;

        /// <summary>
        /// Devuelve la lista de habilidades del mob
        /// </summary>
        /// <returns>Lista de habilidades del mob</returns>
        public List<AbstractHabilidad> GetHabilidades() => habilidades;

        /// <summary>
        /// Devuelve la lista de efectos aplicados al mob
        /// </summary>
        /// <returns>Lista de efectos aplicados al mob</returns>
        public List<AbstractEfecto> GetEfectos() => efectos;

        /// <summary>
        /// Aumenta el valor de un stat del mob
        /// </summary>
        /// <param name="indice">Índice del stat a aumentar</param>
        /// <param name="valor">Valor que aumenta el stat</param>
        /// <param name="max">Valor máximo del stat</param>
        /// <exception cref="ArgumentException">Lanza una excepción si el valor a aumentar es menor o igual a 0</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lanza una excepción si el índice es incorrecto</exception>
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

        /// <summary>
        /// Decide si se debe aumentar un stat en función a su stat de crecimiento
        /// </summary>
        /// <param name="statCrec">stat de crecimiento</param>
        /// <returns>true si el stat debe subir, false en caso contrario</returns>
        protected bool puedeSubirStat(byte statCrec)
        {
            return Util.Probabilidad(statCrec);
        }

        /// <summary>
        /// Verifica que la suma de los stats iniciales del mob sea igual o inferior a la cantidad definida.
        /// </summary>
        /// <param name="statsMaximos">Cantidad máxima que no debe superar la suma de los stats</param>
        /// <exception cref="InvalidOperationException">Lanza una excepción si la suma de los stats supera el máximo permitido</exception>
        private void validarStats(int statsMaximos)
        {
            int total = Util.Sumar(fue, mag, agi, hab, def, res);

            if (total > statsMaximos)
                throw new InvalidOperationException($"El valor de los stats iniciales ({total}) son más altos de lo permitido ({statsMaximos})");
        }
    }
}
