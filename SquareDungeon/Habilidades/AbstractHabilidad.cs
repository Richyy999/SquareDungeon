using System;
using System.Collections.Generic;

using SquareDungeon.Salas;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    abstract class AbstractHabilidad
    {
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
            public const string REDUCIR_STATS = "reducirStats";
        }

        public const int PRIORIDAD_MAXIMA = 5;
        public const int PRIORIDAD_ALTA = 4;
        public const int PRIORIDAD_MEDIA = 3;
        public const int PRIORIDAD_BAJA = 2;
        public const int PRIORIDAD_MINIMA = 1;

        public const int RESULTADO_SIN_ACTIVAR = -10;
        public const int RESULTADO_ACTIVADA = -11;
        public const int RESULTADO_MOB_DERROTADO = -12;

        protected int activacion;

        private int prioridad;

        private string nombre;
        private string descripcion;
        private string categoria;

        private bool anulada;

        protected AbstractHabilidad(int activacion, int prioridad, string nombre, string descripcion, string categoria)
        {
            this.activacion = activacion;
            this.prioridad = prioridad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_HABILIDADES, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_HABILIDADES, descripcion);

            this.categoria = categoria;

            ResetearHabilidad();
        }

        protected AbstractHabilidad(int prioridad, string nombre, string descripcion, string categoria) : this(0, prioridad, nombre, descripcion, categoria) { }

        public virtual void ResetearHabilidad() { anulada = false; }

        public virtual bool EjecutarPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual bool EjecutarPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual bool EjecutarAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual bool EjecutarAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual bool EjecutarPostAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual bool EjecutarPostCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => false;

        public virtual void RealizarAccionPreCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        public virtual void RealizarAccionPreAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        public virtual int RealizarAccionAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) => RESULTADO_SIN_ACTIVAR;

        public virtual int RealizarAccionAtaqueRival(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala, int danoRecibido) => danoRecibido;

        public virtual void RealizarAccionPostAtaque(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        public virtual void RealizarAccionPostCombate(AbstractMob ejecutor, AbstractMob victima, AbstractSala sala) { }

        public string GetCategoria() => this.categoria;

        public string GetNombre() => this.nombre;

        public string GetDescripcion() => this.descripcion;

        public int GetPrioridad() => this.prioridad;

        public bool IsAnulada() => this.anulada;

        public void SetAnulada(bool anulada) { anulada = this.anulada; }

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
