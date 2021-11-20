using System;

using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Ataque
{
    class DosPorUno : Habilidad
    {

        public DosPorUno() : base(25, PRIORIDAD_MAXIMA, TIPO_ATAQUE, NOMBRE_DOS_POR_UNO, DESC_DOS_POR_UNO)
        { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            int ataque = 0;
            do
            {
                Arma arma = jugador.GetArmaCombate();
                if (arma.Atacar(enemigo))
                    return RESULTADO_MOB_DERROTADO;
                else
                    ataque++;

            } while (ataque < 2);

            return RESULTADO_ACTIVADA;
        }
    }
}
