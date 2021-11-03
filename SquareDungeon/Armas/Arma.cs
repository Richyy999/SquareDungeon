using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Armas
{
    abstract class Arma
    {
        public const int SIN_USOS = 0;

        protected int dano;
        protected int usos;

        protected string nombre;

        protected Habilidad habilidad;

        protected Mob portador;

        protected Arma(int dano, int usos, string nombre, Habilidad habilidad, Mob portador)
        {
            this.dano = dano;
            this.usos = usos;

            this.nombre = nombre;

            this.habilidad = habilidad;

            this.portador = portador;
        }

        public virtual void GastarArma()
        {
            usos--;

            if (usos < SIN_USOS)
                throw new InvalidOperationException("No se puede gastar un arma sin usos");
        }

        public abstract void RepararArma(int usos);

        public abstract bool Danar(Mob mob);

        public int GetUsos() => usos;

        public string GetNombre() => nombre;
    }
}
