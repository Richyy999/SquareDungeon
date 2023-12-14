using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Modelo
{
    class EjecutorHabilidades
    {
        private AbstractMob ejecutor;

        private AbstractMob victima;

        private AbstractSala sala;

        private List<AbstractHabilidad> habilidades;

        public EjecutorHabilidades(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala, List<AbstractHabilidad> habilidades)
        {
            this.ejecutor = ejecutor;
            this.victima = victima;
            this.sala = sala;
            this.habilidades = habilidades;
        }

        public EjecutorHabilidades(AbstractMob ejecutor, AbstractMob victima, AbstractHabilidad habilidad)
        {
            this.ejecutor = ejecutor;
            this.victima = victima;
            this.habilidades = new List<AbstractHabilidad>();
            this.habilidades.Add(habilidad);
        }

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
                habilidadesEjecutadas.Add(habilidad);
                int res = habilidad.RealizarAccionAtaque(ejecutor, victima, sala);
                EntradaSalida.MostrarHabilidad(ejecutor, habilidad);
                return res;
            }

            return AbstractHabilidad.RESULTADO_SIN_ACTIVAR;
        }

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

        public void ResetearHabilidades()
        {
            foreach (AbstractHabilidad habilidad in habilidades)
            {
                habilidad.ResetearHabilidad();
            }
        }
    }
}
