using System;

using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Ataque.Jefes
{
    class HabilidadTrol : Habilidad
    {
        public HabilidadTrol() : base(15, PRIORIDAD_MAXIMA, TIPO_ATAQUE, NOMBRE_HABILIDAD_TROL, DESC_HABILIDAD_TROL) { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            if (enemigo.GetType() != typeof(Trol))
                throw new ArgumentException("enemigo",
                    $"Solo un {typeof(Trol)} puede usar esta habilidad y la está utilizando un {enemigo.GetType()}");

            int ata = (int)(enemigo.GetStat(Mob.INDICE_FUERZA) * 1.2);

            int dano = ata - jugador.GetStat(Mob.INDICE_DEFENSA);
            int crit = 1 + enemigo.GetCritico();

            dano *= crit;
            dano *= 2;

            if (jugador.Danar(dano))
                return RESULTADO_MOB_DERROTADO;

            return RESULTADO_ACTIVADA;
        }
    }
}
