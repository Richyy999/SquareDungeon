using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Armas;

namespace SquareDungeon.Modelo
{
    /// <summary>
    /// Clase de utilidades
    /// </summary>
    static class Util
    {
        /// <summary>
        /// Obtiene el porcentaje de un valor y le suma el extra
        /// </summary>
        /// <param name="total">Valor del cual se desea obtener su porcentaje</param>
        /// <param name="porcentaje">Porcentaje del valor que se desea obtener</param>
        /// <param name="extra">Extra que se suma al porcentaje calculado</param>
        /// <returns>Porcentaje calculado</returns>
        public static double GetPorcentaje(int total, double porcentaje, double extra)
        {
            double porcentajeAux = porcentaje / 100f;
            double res = total * porcentajeAux;

            return res + extra;
        }

        /// <summary>
        /// Obtiene el porcentaje de un valor
        /// </summary>
        /// <param name="total">Valor del cual se desea obtener su porcentaje</param>
        /// <param name="porcentaje">Porcentaje del valor que se desea obtener</param>
        /// <returns>Porcentaje calculado</returns>
        public static double GetPorcentaje(int total, double porcentaje) => GetPorcentaje(total, porcentaje, 0);

        /// <summary>
        /// Genera un número aleatorio para ver si se cumple la probabilidad indicada
        /// </summary>
        /// <param name="porcentaje">porcentaje de la probabilidad</param>
        /// <returns>true si el número generado es inferior o igual al porcentaje, falkse en caso contrario</returns>
        public static bool Probabilidad(double porcentaje)
        {
            Random random = new Random();
            int num = random.Next(100);

            return num <= porcentaje;
        }

        /// <summary>
        /// Obtiene el daño base que hace un mob a otro
        /// </summary>
        /// <param name="atacante"><see cref="AbstractMob">Mob</see> que realiza el ataque</param>
        /// <param name="victima"><see cref="AbstractMob">Mob</see> que recibe el ataque</param>
        /// <returns>Daño infligido a la <paramref name="victima"/></returns>
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

        /// <summary>
        /// Suma los núeros indicados
        /// </summary>
        /// <param name="numeros">Números a sumar</param>
        /// <returns>Suma de los núeros indicados</returns>
        public static int Sumar(params int[] numeros)
        {
            int res = 0;

            foreach (int numero in numeros)
            {
                res += numero;
            }

            return res;
        }
    }
}
