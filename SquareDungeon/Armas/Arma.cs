using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas
{
    abstract class Arma
    {
        public const int SIN_USOS = 0;

        protected int dano;
        protected int usos;

        protected Habilidad habilidad;

        protected Mob portador;

        private string nombre;
        private string descripcion;

        protected Arma(int dano, int usos, string nombre, string descripcion, Habilidad habilidad, Mob portador)
        {
            this.dano = dano;
            this.usos = usos;

            this.habilidad = habilidad;

            this.portador = portador;

            this.nombre = GetPropiedad(FICHERO_NOMBRE_ARMAS, nombre);

            this.descripcion = GetPropiedad(FICHERO_DESC_ARMAS, descripcion);

            if (this.nombre == null)
                throw new ArgumentNullException("nombre", $"El nombre del arma {nombre} no existe");

            if (this.descripcion == null)
                throw new ArgumentNullException("descripcion",
                    $"La descripción {descripcion} del arma {nombre} no existe");
        }

        public virtual void GastarArma()
        {
            usos--;

            if (usos < SIN_USOS)
                throw new InvalidOperationException("No se puede gastar un arma sin usos");
        }

        public abstract void RepararArma(int usos);

        public abstract bool Atacar(Mob mob);

        public int GetDano() => dano;

        public int GetUsos() => usos;

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;
    }
}
