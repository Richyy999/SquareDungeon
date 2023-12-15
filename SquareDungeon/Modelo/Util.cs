using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Armas;

namespace SquareDungeon.Modelo
{
    static class Util
    {
        public static double GetPorcentaje(int total, double porcentaje, double extra)
        {
            double porcentajeAux = porcentaje / 100f;
            double res = total * porcentajeAux;

            return res + extra;
        }

        public static double GetPorcentaje(int total, double porcentaje) => GetPorcentaje(total, porcentaje, 0);

        public static bool Probabilidad(double porcentaje)
        {
            Random random = new Random();
            int num = random.Next(101);

            return num <= porcentaje;
        }

        public static int GetDano(AbstractMob atacante, AbstractMob victima)
        {
            int dano = 0;

            if (atacante is AbstractJugador)
            {
                AbstractJugador jugador = (AbstractJugador)atacante;
                AbstractArma arma = jugador.GetArmaCombate();
                dano = arma.GetDanoBase(victima);

            } else if (atacante is AbstractEnemigo)
            {
                AbstractEnemigo enemigo = (AbstractEnemigo)atacante;
                dano = enemigo.Atacar(victima);
            }

            return dano;
        }
    }
}
