using System;

using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    class Mago : AbstractJugador
    {
        public Mago(string nombre, AbstractHabilidad habilidad) : base(18, 1, 8, 3, 2, 2, 3, 10, 17,
            67, 12, 89, 68, 45, 14, 73, 60, 65,
            55, 15, 50, 30, 30, 10, 40, 50, 125, nombre, DESC_MAGO, habilidad)
        { }

        public Mago(string nombre) : base(18, 1, 8, 3, 2, 2, 3, 10, 17,
            67, 12, 89, 68, 45, 14, 73, 60, 65,
            55, 15, 50, 30, 30, 10, 40, 50, 125, nombre, DESC_MAGO, SIN_HABILIDAD)
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
            AbstractArmaMagica[] armas = new AbstractArmaMagica[this.armas.Length];
            for (int i = 0; i < this.armas.Length; i++)
            {
                armas[i] = (AbstractArmaMagica)this.armas[i];
            }

            return armas;
        }

        public override AbstractArmaMagica GetArmaCombate() => (AbstractArmaMagica)armaCombate;
    }
}
