using System;
using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;

using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Entidades.Mobs.Jugadores
{
    class Guerrero : Jugador
    {

        public Guerrero() : base(20, 4, 1, 2, 3, 1, 10, 15,
            75, 80, 5, 50, 60, 10, 20, 40,
            60, 55, 15, 35, 40, 20, 40, 100, SIN_HABILIDAD)
        { }

        public override bool EquiparArma(Arma arma)
        {
            if (!arma.GetType().IsSubclassOf(typeof(ArmaFisica)))
                throw new ArgumentException("arma",
                    $"El guerrero solo puede utilizar armas físicas. Se ha recibido un {arma.GetType()}");

            for (int i = 0; i < armas.Length; i++)
            {
                if (armas[i] == null)
                {
                    armas[i] = arma;
                    return true;
                }
            }

            return false;
        }

        public override ArmaFisica[] GetArmas() => (ArmaFisica[])armas;
    }
}
