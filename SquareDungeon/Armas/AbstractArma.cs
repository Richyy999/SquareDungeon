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

        protected AbstractMob portador;

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

        public void SetPortador(AbstractMob portador)
        {
            if (!portador.GetType().IsSubclassOf(typeof(AbstractJugador)))
                throw new ArgumentException("portador", "Solo los jugadores pueden ortar armas");

            this.portador = portador;
        }

        public virtual void GastarArma()
        {
            if (portador is AbstractJugador)
            {
                AbstractJugador portador = (AbstractJugador)this.portador;
                usos--;
                if (usos == SIN_USOS)
                    portador.EliminarArma(this);
            }

            if (usos < SIN_USOS)
                throw new InvalidOperationException("No se puede gastar un arma sin usos");
        }

        public abstract void RepararArma(int usos);

        public abstract int Atacar(AbstractMob mob);

        public abstract int Atacar(AbstractMob mob, int danoAdicional);

        public AbstractHabilidad GetHabilidad() => habilidad;

        public int GetDano() => dano;

        public int GetUsos() => usos;

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;
    }
}
