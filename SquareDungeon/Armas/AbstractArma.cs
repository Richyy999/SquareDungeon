using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas
{
    abstract class AbstractArma
    {
        public const int SIN_USOS = 0;

        protected int dano;
        protected int usos;

        protected AbstractHabilidad habilidad;

        protected AbstractJugador portador;

        private string nombre;
        private string descripcion;

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

        public abstract int GetUsosMaximos();

        public abstract void RepararArma(int usos);

        public abstract int GetDanoBase(AbstractMob mob);

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

        public virtual void GastarArma()
        {
            usos--;
            if (usos == SIN_USOS)
                portador.EliminarArma(this);

            if (usos < SIN_USOS)
                throw new InvalidOperationException("No se puede gastar un arma sin usos");
        }

        public void SetPortador(AbstractJugador portador)
        {
            this.portador = portador;
        }

        public int Atacar(AbstractMob mob, int danoAdicional)
        {
            int dano = Atacar(mob);
            return dano + danoAdicional;
        }

        public AbstractHabilidad GetHabilidad() => habilidad;

        public int GetDano() => dano;

        public int GetUsos() => usos;

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;
    }
}
