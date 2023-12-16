using System;
using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Habilidades;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    class Guerrero : AbstractJugador
    {
        public Guerrero(string nombre, AbstractHabilidad habilidad) : base(20, 4, 2, 3, 3, 4, 3,
            75, 80, 5, 50, 60, 60, 35,
            60, 55, 15, 35, 40, 45, 30,
            nombre, DESC_GUERRERO, habilidad)
        { }

        public override bool EquiparArma(AbstractArma arma)
        {
            if (arma is not AbstractArmaFisica)
                throw new ArgumentException("arma", $"El guerrero solo puede utilizar armas físicas. Se ha recibido un {arma.GetType()}");

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
