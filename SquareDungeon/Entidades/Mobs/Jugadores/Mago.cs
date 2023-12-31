using System;
using System.Collections.Generic;

using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    class Mago : AbstractJugador
    {
        public Mago(string nombre, AbstractHabilidad habilidad) : base(18, 2, 4, 3, 4, 3, 4,
            67, 12, 89, 68, 45, 14, 73,
            55, 15, 60, 30, 30, 25, 40, nombre, DESC_MAGO, habilidad)
        { }

        public override bool EquiparArma(AbstractArma arma)
        {
            if (arma is not AbstractArmaMagica)
                throw new ArgumentException("arma", $"El guerrero solo puede utilizar armas mágicas. Se ha recibido un {arma.GetType()}");

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
                    arma.SetPortador(this);
                    return true;
                }
            }

            return false;
        }

        public override AbstractArmaMagica[] GetArmas()
        {
            List<AbstractArmaMagica> armas = new List<AbstractArmaMagica>();
            for (int i = 0; i < this.armas.Length; i++)
            {
                AbstractArma arma = this.armas[i];
                if (arma != null)
                    armas.Add((AbstractArmaMagica)arma);
            }

            return armas.ToArray();
        }

        public override AbstractArmaMagica GetArmaCombate() => (AbstractArmaMagica)armaCombate;
    }
}
