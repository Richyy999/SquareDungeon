using System;

using SquareDungeon.Armas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    internal class Explorador : AbstractJugador
    {
        public Explorador(string nombre, AbstractHabilidad habilidad) : base(20, 5, 6, 5, 1, 5, 3, 10, 15,
            67, 12, 89, 68, 45, 14, 73, 60, 65,
            45, 15, 50, 30, 30, 10, 40, 50, 100, nombre, DESC_EXPLORADOR, habilidad)
        { }

        public Explorador(string nombre) : base(20, 5, 6, 5, 1, 5, 3, 10, 15,
            67, 12, 89, 68, 45, 14, 73, 60, 65,
            45, 15, 50, 30, 30, 10, 40, 50, 100, nombre, DESC_EXPLORADOR, SIN_HABILIDAD)
        { }

        public override bool EquiparArma(AbstractArma arma)
        {
            if (!arma.GetType().IsSubclassOf(typeof(AbstractArma)))
                throw new ArgumentException("arma",
                    $"El guerrero solo puede utilizar armas mágicas. Se ha recibido un {arma.GetType()}");

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

        public override AbstractArma[] GetArmas()
        {
            AbstractArma[] armas = new AbstractArma[this.armas.Length];
            for (int i = 0; i < this.armas.Length; i++)
            {
                armas[i] = (AbstractArma)this.armas[i];
            }

            return armas;
        }

        public override AbstractArma GetArmaCombate() => (AbstractArma)armaCombate;
    }
}
