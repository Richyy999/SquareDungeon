using System;
using System.Collections.Generic;

using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades
{
    abstract class Habilidad
    {
        public const int TIPO_COMBATE = 0;
        public const int TIPO_PRE_COMBATE = -1;
        public const int TIPO_POST_COMBATE = -2;
        public const int TIPO_ATAQUE = -3;

        public const int PRIORIDAD_MAXIMA = 5;
        public const int PRIORIDAD_ALTA = 4;
        public const int PRIORIDAD_MEDIA = 3;
        public const int PRIORIDAD_BAJA = 2;
        public const int PRIORIDAD_MINIMA = 1;

        public const int RESULTADO_SIN_ACTIVAR = 10;
        public const int RESULTADO_ACTIVADA = 11;
        public const int RESULTADO_MOB_DERROTADO = 12;

        private int activacion;
        private int tipoHabilidad;
        private int prioridad;

        private string nombre;
        private string descripcion;

        protected Habilidad(int activacion, int prioridad, int tipoHabilidad, string nombre, string descripcion)
        {
            this.activacion = activacion;
            this.prioridad = prioridad;
            this.tipoHabilidad = tipoHabilidad;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_HABILIDADES, nombre);
            this.descripcion = GetPropiedad(FICHERO_DESC_HABILIDADES, descripcion);
        }

        public abstract int RealizarAccion(Jugador jugador, Enemigo enemigo);

        public int GetTipoHabilidad() => tipoHabilidad;

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;

        public int GetPrioridad() => prioridad;

        public static Habilidad GetHabilidadPorPrioridad(List<Habilidad> habilidades)
        {
            habilidades.Sort((h1, h2) => h1.prioridad.CompareTo(h2.prioridad));

            List<Habilidad> prioridadMaxima = new List<Habilidad>();
            int prioridad = habilidades[habilidades.Count - 1].prioridad;

            for (int i = habilidades.Count - 1; i >= 0; i--)
            {
                Habilidad habilidad = habilidades[i];
                if (habilidad.prioridad < prioridad)
                    break;

                prioridadMaxima.Add(habilidad);
            }

            Random random = new Random();
            int indice = random.Next(0, prioridadMaxima.Count);

            return prioridadMaxima[indice];
        }

        public bool Ejecutar()
        {
            Random random = new Random();

            int res = random.Next(0, 101);

            return res <= activacion;
        }
    }
}
