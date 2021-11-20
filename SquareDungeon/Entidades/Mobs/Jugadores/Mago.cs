using System;

using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    class Mago : Jugador
    {
        public Mago(string nombre, Habilidad habilidad) : base(18, 1, 3, 2, 2, 3, 10, 17,
            67, 12, 89, 68, 14, 73, 60, 65,
            55, 15, 50, 30, 10, 40, 50, 125, nombre, DESC_MAGO, habilidad)
        { }

        public Mago(string nombre) : base(18, 1, 3, 2, 1, 2, 10, 17,
            67, 12, 89, 68, 14, 73, 60, 65,
            55, 15, 50, 30, 20, 40, 50, 125, nombre, DESC_MAGO, SIN_HABILIDAD)
        { }

        public override bool EquiparArma(Arma arma)
        {
            if (!arma.GetType().IsSubclassOf(typeof(ArmaMagica)))
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

        public override ArmaMagica[] GetArmas()
        {
            ArmaMagica[] armas = new ArmaMagica[this.armas.Length];
            for (int i = 0; i < this.armas.Length; i++)
            {
                armas[i] = (ArmaMagica)this.armas[i];
            }

            return armas;
        }

        public override ArmaMagica GetArmaCombate() => (ArmaMagica)armaCombate;
    }
}
