using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas
{
    /// <summary>
    /// Arma con la cual el jugador ataca a los enemigos.
    /// </summary>
    abstract class AbstractArma
    {
        public const int SIN_USOS = 0;

        /// <summary>
        /// Daño base del arma
        /// </summary>
        protected int dano;
        /// <summary>
        /// Usos del arma
        /// </summary>
        protected int usos;

        /// <summary>
        /// Habilidad que posee el arma
        /// </summary>
        protected AbstractHabilidad habilidad;

        /// <summary>
        /// <see cref="AbstractJugador">Jugador</see> que porta el arma
        /// </summary>
        protected AbstractJugador portador;

        /// <summary>
        /// Nombre del arma
        /// </summary>
        private string nombre;
        /// <summary>
        /// Descripción del arma
        /// </summary>
        private string descripcion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="dano">Daño básico del arma</param>
        /// <param name="usos">Usos máximos del arma</param>
        /// <param name="nombre">Nombre del arma</param>
        /// <param name="descripcion">Descripción del arma</param>
        /// <param name="habilidad">Habilidad del arma.<br/>Si el arma no posee ninguna habilidad se debe usar la constante <see cref="Habilidades.SinHabilidad">SIN_HABILIDAD</see></param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si no se añaden los campos (<paramref name="nombre"/>, <paramref name="descripcion"/>)</exception>
        protected AbstractArma(int dano, int usos, string nombre, string descripcion, AbstractHabilidad habilidad)
        {
            this.dano = dano;
            this.usos = usos;

            this.habilidad = habilidad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_ARMAS, nombre);

            this.descripcion = GetPropiedad(FICHERO_DESC_ARMAS, descripcion);

            if (this.nombre == null)
                throw new ArgumentNullException("nombre", $"El nombre del arma {nombre} no existe");

            if (this.descripcion == null)
                throw new ArgumentNullException("descripcion",
                    $"La descripción {descripcion} del arma {nombre} no existe");
        }

        /// <summary>
        /// Devuelve la cantidad máxima de usos que tiene el arma
        /// </summary>
        /// <returns>Usos máximos del arma</returns>
        public abstract int GetUsosMaximos();

        /// <summary>
        /// Repara un arma sumando los usos introducidos y los usos restantes del arma
        /// </summary>
        /// <param name="usos">Usos que se sumarán al arma</param>
        public abstract void RepararArma(int usos);

        /// <summary>
        /// Calcula el daño base del arma
        /// </summary>
        /// <param name="mob">Mob atacado por el arma</param>
        /// <returns>Daño básico del arma</returns>
        public abstract int GetDanoBase(AbstractMob mob);

        /// <summary>
        /// Calcula el daño causado a un enemigo a partir del <see cref="GetDanoBase(AbstractMob)">daño base del arma</see> y del daño crítico del portador.<br/>Gasta el arma cada vez que se ataca con ella<br/>Si se sobreescribe este método se ha de llamar al padre siempre
        /// </summary>
        /// <param name="mob">Enemigo al que ataca el arma</param>
        /// <returns>Daño causado al enemigo</returns>
        /// <exception cref="InvalidOperationException">Lanza una excepción si el arma no tiene usos</exception>
        public virtual int Atacar(AbstractMob mob)
        {
            if (usos <= SIN_USOS)
                throw new InvalidOperationException("No se puede usar un arma sin usos");

            int danoBasico = GetDanoBase(mob);
            double crit = 1 + portador.GetCritico();

            int dano = (int)(danoBasico * crit);
            if (dano <= 0)
                dano = 1;

            GastarArma();

            return dano;
        }

        /// <summary>
        /// gasta usos del arma.<br/>Si el arma se queda sin usos, se elimina de la lista de armas del <see cref="portador">portador</see>
        /// </summary>
        /// <exception cref="InvalidOperationException">Lanza una excepción si </exception>
        public virtual void GastarArma()
        {
            usos--;
            if (usos == SIN_USOS)
                portador.EliminarArma(this);

            if (usos < SIN_USOS)
                throw new InvalidOperationException("No se puede gastar un arma sin usos");
        }

        /// <summary>
        /// Añade el <see cref="AbstractJugador">jugador</see> que porta el arma
        /// </summary>
        /// <param name="portador"><see cref="portador">Portador</see> del arma</param>
        public void SetPortador(AbstractJugador portador)
        {
            this.portador = portador;
        }

        /// <summary>
        /// Ataca al enemigo sumando el daño adicional
        /// </summary>
        /// <param name="mob"><see cref="Entidades.Mobs.Enemigos.AbstractEnemigo">Enemigo</see> que recibe el ataque</param>
        /// <param name="danoAdicional"></param>
        /// <returns>La suma del ataque más el daño adicional</returns>
        public int Atacar(AbstractMob mob, int danoAdicional)
        {
            int dano = Atacar(mob);
            return dano + danoAdicional;
        }

        /// <summary>
        /// Devuelve la habilidad del arma
        /// </summary>
        /// <returns><see cref="AbstractHabilidad">Habilidad</see> del arma</returns>
        public AbstractHabilidad GetHabilidad() => habilidad;

        /// <summary>
        /// Devuelve el <see cref="dano">daño</see> del arma
        /// </summary>
        /// <returns><see cref="dano">Daño</see> del arma</returns>
        public int GetDano() => dano;

        /// <summary>
        /// Devuelve los <see cref="usos">usos</see> del arma
        /// </summary>
        /// <returns><see cref="usos">Usos</see> del arma</returns>
        public int GetUsos() => usos;

        /// <summary>
        /// Devuelve el <see cref="nombre">nombre</see> del arma
        /// </summary>
        /// <returns><see cref="nombre">Nombre</see> del arma</returns>
        public string GetNombre() => nombre;

        /// <summary>
        /// Devuelve la <see cref="descripcion">descripción</see> del arma
        /// </summary>
        /// <returns><see cref="usos">Descripción</see> del arma</returns>
        public string GetDescripcion() => descripcion;

        public override string ToString() => $"{GetNombre()} {GetUsos()}/{GetUsosMaximos()}";
    }
}
