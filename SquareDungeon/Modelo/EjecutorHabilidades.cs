using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Modelo
{
    /// <summary>
    /// Clase encargada de la ejecución de las habilidades durante el combate. Para usar cualquier habilidad se debe realizar desde esta clase.
    /// </summary>
    class EjecutorHabilidades
    {
        /// <summary>
        /// <see cref="AbstractMob">Mob</see> que ejecuta las habilidades
        /// </summary>
        private AbstractMob ejecutor;

        /// <summary>
        /// <see cref="AbstractMob">Mob</see> que recibe el efecto de las habilidades
        /// </summary>
        private AbstractMob victima;

        /// <summary>
        /// <see cref="AbstractSala">Sala</see> en la que se ejecuta las habilidades
        /// </summary>
        private AbstractSala sala;

        /// <summary>
        /// Lista de habilidades del <see cref="ejecutor"/>
        /// </summary>
        private List<AbstractHabilidad> habilidades;

        /// <summary>
        /// Constructor por defecto de la clase
        /// </summary>
        /// <param name="ejecutor">Mob que ejecuta las habilidades</param>
        /// <param name="victima">Mob que recibe el efecto de las habilidades</param>
        /// <param name="sala">Sala en la que se ejecutan las habilidades</param>
        /// <param name="habilidades">Lista de las habilidades del <paramref name="ejecutor"/></param>
        public EjecutorHabilidades(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala, List<AbstractHabilidad> habilidades)
        {
            this.ejecutor = ejecutor;
            this.victima = victima;
            this.sala = sala;
            this.habilidades = habilidades;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="ejecutor">Mob que ejecuta las habilidades</param>
        /// <param name="victima">Mob que recibe el efecto de las habilidades</param>
        /// <param name="habilidad">Habilidad del ejecutor</param>
        public EjecutorHabilidades(AbstractMob ejecutor, AbstractMob victima, AbstractHabilidad habilidad)
        {
            this.ejecutor = ejecutor;
            this.victima = victima;
            this.habilidades = new List<AbstractHabilidad>();
            this.habilidades.Add(habilidad);
        }

        /// <summary>
        /// Ejecuta las habilidades pre-combate y devuelve el resultado de la ejecución
        /// </summary>
        /// <returns>true si se ha ejecutado alguna habilidad. false en caso contrario</returns>
        public bool EjecutarPreCombate()
        {
            bool ejecutado = false;

            foreach (AbstractHabilidad habilidad in habilidades)
            {
                if (habilidad.EjecutarPreCombate(ejecutor, victima, sala) && !habilidad.IsAnulada())
                {
                    habilidad.RealizarAccionPreCombate(ejecutor, victima, sala);
                    EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                    ejecutado = true;
                }
            }

            return ejecutado;
        }

        /// <summary>
        /// Ejecuta las habilidades pre-ataque y devuelve el resultado de la ejecución
        /// </summary>
        /// <returns>true si se ha ejecutado alguna habilidad. false en caso contrario</returns>
        public bool EjecutarPreAtaque()
        {
            bool ejecutado = false;

            foreach (AbstractHabilidad habilidad in habilidades)
            {
                if (habilidad.EjecutarPreAtaque(ejecutor, victima, sala) && !habilidad.IsAnulada())
                {
                    habilidad.RealizarAccionPreAtaque(ejecutor, victima, sala);
                    EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                    ejecutado = true;
                }
            }

            return ejecutado;
        }

        /// <summary>
        /// Ejecuta la habilidades de ataque con más prioridad y devuelve el daño infligido a la víctima
        /// </summary>
        /// <returns>Daño infligido a la víctima</returns>
        public int EjecutarAtaque()
        {
            List<AbstractHabilidad> habilidadesEjecutadas = new List<AbstractHabilidad>();
            foreach (AbstractHabilidad habilidad in this.habilidades)
            {
                if (habilidad.EjecutarAtaque(ejecutor, victima, sala) && !habilidad.IsAnulada())
                    habilidadesEjecutadas.Add(habilidad);
            }

            if (habilidadesEjecutadas.Count > 0)
            {
                AbstractHabilidad habilidad = AbstractHabilidad.GetHabilidadPorPrioridad(habilidadesEjecutadas);
                int res = habilidad.RealizarAccionAtaque(ejecutor, victima, sala);
                EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                return res;
            }

            return AbstractHabilidad.RESULTADO_SIN_ACTIVAR;
        }

        /// <summary>
        /// Ejecuta las habilidades durante el ataque del rival y devuelve el daño que recibe el ejecutor
        /// </summary>
        /// <param name="danoRecibido">Daño que recibe el ejecutor</param>
        /// <returns>Daño que recibe el ejecutor</returns>
        public int EjecutarAtaqueRival(int danoRecibido)
        {
            List<AbstractHabilidad> habilidadesEjecutadas = new List<AbstractHabilidad>();
            foreach (AbstractHabilidad habilidad in this.habilidades)
            {
                if (habilidad.EjecutarAtaqueRival(ejecutor, victima, sala) && !habilidad.IsAnulada())
                    habilidadesEjecutadas.Add(habilidad);
            }

            if (habilidadesEjecutadas.Count > 0)
            {
                AbstractHabilidad habilidad = AbstractHabilidad.GetHabilidadPorPrioridad(habilidadesEjecutadas);
                int dano = habilidad.RealizarAccionAtaqueRival(ejecutor, victima, sala, danoRecibido);
                EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                return dano;
            }

            return danoRecibido;
        }

        /// <summary>
        /// Ejecuta las habilidades post-ataque y devuelve el resultado de la ejecución
        /// </summary>
        /// <returns>true si se ha ejecutado alguna habilidad. false en caso contrario</returns>
        public bool EjecutarPostAtaque()
        {
            bool ejecutado = false;

            foreach (AbstractHabilidad habilidad in habilidades)
            {
                if (habilidad.EjecutarPostAtaque(ejecutor, victima, sala) && !habilidad.IsAnulada())
                {
                        habilidad.RealizarAccionPostAtaque(ejecutor, victima, sala);
                        EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                        ejecutado = true;
                }
            }

            return ejecutado;
        }

        /// <summary>
        /// Ejecuta las habilidades post-combate y devuelve el resultado de la ejecución
        /// </summary>
        /// <returns>true si se ha ejecutado alguna habilidad. false en caso contrario</returns>
        public bool EjecutarPostCombate()
        {
            bool ejecutado = false;

            foreach (AbstractHabilidad habilidad in habilidades)
            {
                if (habilidad.EjecutarPostCombate(ejecutor, victima, sala) && !habilidad.IsAnulada())
                {
                        habilidad.RealizarAccionPostCombate(ejecutor, victima, sala);
                        EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                        ejecutado = true;
                }
            }

            return ejecutado;
        }

        /// <summary>
        /// Resetea las habilidades del ejecutor
        /// </summary>
        public void ResetearHabilidades()
        {
            foreach (AbstractHabilidad habilidad in habilidades)
            {
                habilidad.ResetearHabilidad();
            }
        }
    }
}
