using System;
using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    /// <summary>
    /// Clase básica que define las acciones de las habilidades. Todas las habilidades deben heredar de esta clase
    /// </summary>
    abstract class AbstractHabilidad
    {
        /// <summary>
        /// Clase con las categorías a las que pertenecen las habilidades
        /// </summary>
        public static class Categorias
        {
            public const string SIN_HABILIDAD = "sinHabilidad";

            public const string REDUCIR_DANO = "reducirDano";
            public const string CURAR = "curar";
            public const string SUBIR_STATS = "subirStats";
            public const string DANO_ADICIONAL = "danoAdicional";
            public const string DANO_ADICIONAL_TIPO_ENEMIGO = "danoAdicionalTipoEnemigo";
            public const string DANO_ADICOINAL_STAT_EJECUTOR = "danoAdicionalEjecutor";
            public const string DOBLE_GOLPE = "dobleGolpe";
            public const string CONTRA_ATAQUE = "contraataque";
        }

        public const int PRIORIDAD_MAXIMA = 5;
        public const int PRIORIDAD_ALTA = 4;
        public const int PRIORIDAD_MEDIA = 3;
        public const int PRIORIDAD_BAJA = 2;
        public const int PRIORIDAD_MINIMA = 1;

        public const int RESULTADO_SIN_ACTIVAR = -10;
        public const int RESULTADO_ACTIVADA = -11;
        public const int RESULTADO_MOB_DERROTADO = -12;

        /// <summary>
        /// Porcentaje que tiene la habilidad de ejecutarse
        /// </summary>
        protected int activacion;

        /// <summary>
        /// Prioridad de ejecución de la habilidad
        /// </summary>
        private int prioridad;

        /// <summary>
        /// Nombre de la habilidad
        /// </summary>
        private string nombre;
        /// <summary>
        /// Descripción de la habilidad
        /// </summary>
        private string descripcion;
        /// <summary>
        /// Categoría a la que pertenece la habilidad
        /// </summary>
        /// <seealso cref="Categorias"/>
        private string categoria;

        /// <summary>
        /// Si la habilidad ha sido anulada o no
        /// </summary>
        private bool anulada;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="activacion">Porcentaje que tiene la habilidad de ejecutarse</param>
        /// <param name="prioridad">Prioridad de ejecución de la habilidad</param>
        /// <param name="nombre">Nombre de la habilidad</param>
        /// <param name="descripcion">Descripción de la habilidad</param>
        /// <param name="categoria">Categoría a la que pertenece la habilidad</param>
        protected AbstractHabilidad(int activacion, int prioridad, string nombre, string descripcion, string categoria)
        {
            this.activacion = activacion;
            this.prioridad = prioridad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_HABILIDADES, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_HABILIDADES, descripcion);

            this.categoria = categoria;

            ResetearHabilidad();
        }

        /// <summary>
        /// Constructor de la clase para aquellas habilidades cuya activación se base en un valor distinto a la activación
        /// </summary>
        /// <param name="prioridad">Prioridad de ejecución de la habilidad</param>
        /// <param name="nombre">Nombre de la habilidad</param>
        /// <param name="descripcion">Descripción de la habilidad</param>
        /// <param name="categoria">Categoría a la que pertenece la habilidad</param>
        protected AbstractHabilidad(int prioridad, string nombre, string descripcion, string categoria) : this(0, prioridad, nombre, descripcion, categoria) { }

        /// <summary>
        /// Inicializa los artributos de la habilidad.<br/>
        /// Todos los atributos de las habilidades deben inicializarse en este método y nunca en el constructor.<br/>
        /// Al finalizar el combate se ejecutará este método para reiniciar los atributos de la habilidad
        /// </summary>
        public virtual void ResetearHabilidad() { anulada = false; }

        /// <summary>
        /// Evalua las condiciones de activación de los efectos pre-combate de la habilidad
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos pre-combate de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionPreCombate(AbstractMob, AbstractMob, AbstractSala)"/>
        public virtual bool EjecutarPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Evalua las condiciones de activación de los efectos pre-ataque de la habilidad
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos pre-ataque de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionPreAtaque(AbstractMob, AbstractMob, AbstractSala)"/>
        public virtual bool EjecutarPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Evalua las condiciones de activación de los efectos durante el ataque
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos ataque de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionAtaque(AbstractMob, AbstractMob, AbstractSala)"/>
        public virtual bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Evalua las condiciones de activación de los efectos durante el ataque del rival
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos ataque de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionAtaqueRival(AbstractMob, AbstractMob, AbstractSala, int)"/>
        public virtual bool EjecutarAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Evalua las condiciones de activación de los efectos post-ataque de la habilidad
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos post-ataque de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionPostAtaque(AbstractMob, AbstractMob, AbstractSala)"/>
        public virtual bool EjecutarPostAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Evalua las condiciones de activación de los efectos post-combate de la habilidad
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>true para que se ejecuten los efectos post-combate de la habilidad. false en caso contrario</returns>
        /// <seealso cref="RealizarAccionPostCombate(AbstractMob, AbstractMob, AbstractSala)"/>
        public virtual bool EjecutarPostCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        /// <summary>
        /// Ejecuta el efecto de la habilidad antes del combate
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        public virtual void RealizarAccionPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        /// <summary>
        /// Ejecuta el efecto de la habilidad antes del ataque
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        public virtual void RealizarAccionPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        /// <summary>
        /// Ejecuta el efecto de combate de la habilidad en lugar del ataque del mob
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <returns>Daño infligido a la <paramref name="victima"/></returns>
        public virtual int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => RESULTADO_SIN_ACTIVAR;

        /// <summary>
        /// Ejecuta el efecto de la habilidad durante el ataque rival
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        /// <param name="danoRecibido">Daño que recibe el <paramref name="ejecutor"/></param>
        /// <returns>Daño que recibe el <paramref name="ejecutor"/></returns>
        public virtual int RealizarAccionAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala, int danoRecibido) => danoRecibido;

        /// <summary>
        /// Ejecuta el efecto de la habilidad después del ataque
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        public virtual void RealizarAccionPostAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        /// <summary>
        /// Ejecuta el efecto de la habilidad después del combate
        /// </summary>
        /// <param name="ejecutor"><see cref="AbstractMob">Mob</see> que ejecuta la habilidad</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque del <paramref name="ejecutor"/></param>
        /// <param name="sala"><see cref="AbstractSala">Sala</see> en la que se encuentra el <paramref name="ejecutor"/></param>
        public virtual void RealizarAccionPostCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        /// <summary>
        /// Devuelve la categoría a la que pertenece la habilidad
        /// </summary>
        /// <returns>Categoría a la que pertenece la habilidad</returns>
        public string GetCategoria() => this.categoria;

        /// <summary>
        /// Devuelve el nombre de la habilidad
        /// </summary>
        /// <returns>Nombre de la habilidad</returns>
        public string GetNombre() => this.nombre;

        /// <summary>
        /// Devuelve la descripción de la habilidad
        /// </summary>
        /// <returns>Descripción de la habilidad</returns>
        public string GetDescripcion() => this.descripcion;

        /// <summary>
        /// Devuelve la prioridad de la habilidad
        /// </summary>
        /// <returns>Prioridad de la habilidad</returns>
        public int GetPrioridad() => this.prioridad;

        /// <summary>
        /// Devuelve si la habilidad ha sido anulada o no
        /// </summary>
        /// <returns>true si está anulada, false en caso contrario</returns>
        public bool IsAnulada() => this.anulada;

        /// <summary>
        /// Anula la habilidad
        /// </summary>
        public void Anular() { anulada = true; }

        /// <summary>
        /// Devuelve la habilidad con más prioridad. En caso de haber varias con la misma prioridad, se elige una al azar
        /// </summary>
        /// <param name="habilidades">Lista de habilidades a elegir</param>
        /// <returns>Habilidad con más prioridad</returns>
        public static AbstractHabilidad GetHabilidadPorPrioridad(List<AbstractHabilidad> habilidades)
        {
            habilidades.Sort((h1, h2) => h1.GetPrioridad().CompareTo(h2.GetPrioridad()));

            List<AbstractHabilidad> prioridadMaxima = new List<AbstractHabilidad>();
            int prioridad = habilidades[habilidades.Count - 1].prioridad;

            for (int i = habilidades.Count - 1; i >= 0; i--)
            {
                AbstractHabilidad habilidad = habilidades[i];
                if (habilidad.prioridad < prioridad)
                    break;

                prioridadMaxima.Add(habilidad);
            }

            Random random = new Random();
            int indice = random.Next(0, prioridadMaxima.Count);

            return prioridadMaxima[indice];
        }
    }
}
