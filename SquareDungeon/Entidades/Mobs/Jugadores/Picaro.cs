using System;
using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    internal class Picaro : AbstractJugador
    {
        public Picaro(string nombre, AbstractHabilidad habilidad) : base(20, 4, 1, 8, 5, 2, 1, 6, 18,
            75, 80, 5, 50, 50, 60, 10, 20, 40,
            55, 35, 10, 50, 45, 30, 20, 40, 100, nombre, DESC_PICARO, habilidad)
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
