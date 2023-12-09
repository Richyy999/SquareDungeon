using System;

using SquareDungeon.Armas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;
using SquareDungeon.Armas.ArmasFisicas;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    internal class Barbaro : AbstractJugador
    {
        public Barbaro(string nombre, AbstractHabilidad habilidad) : base(20, 7, 0, 1, 1, 2, 1, 5, 30,
            75, 68, 5, 50, 50, 60, 10, 20, 40,
            70, 80, 15, 10, 5, 20, 20, 55, 150, nombre, DESC_BARBARO, SIN_HABILIDAD)
        { }

        public Barbaro(string nombre) : base(20, 4, 1, 2, 2, 3, 1, 10, 15,
            75, 80, 5, 50, 50, 60, 10, 20, 40,
            60, 55, 15, 35, 40, 40, 20, 40, 100, nombre, DESC_BARBARO, SIN_HABILIDAD)
        { }

        public override bool EquiparArma(AbstractArma arma)
        {
            if (!arma.GetType().IsSubclassOf(typeof(AbstractArmaFisica)))
                throw new ArgumentException("arma",
                    $"El guerrero solo puede utilizar armas físicas. Se ha recibido un {arma.GetType()}");

            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] != null && armas[i].GetNombre().Equals(arma.GetNombre()))
                {
                    armas[i].RepararArma(arma.GetUsos());
                    return true;
                }
                if (armas[i] == null)
                {
                    armas[i] = arma;
                    return true;
                }
            }

            return false;
        }

        public override AbstractArmaFisica[] GetArmas()
        {
            AbstractArmaFisica[] armas = new AbstractArmaFisica[4];
            for (int i = 0; i < armas.Length; i++)
            {
                armas[i] = (AbstractArmaFisica)this.armas[i];
            }

            return armas;
        }

        public override AbstractArmaFisica GetArmaCombate() => (AbstractArmaFisica)armaCombate;
    }
}
