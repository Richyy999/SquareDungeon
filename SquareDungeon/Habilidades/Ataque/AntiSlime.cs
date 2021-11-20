using System;

using SquareDungeon.Armas;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Habilidades.Ataque
{
    class AntiSlime : Habilidad
    {
        public AntiSlime() : base(30, PRIORIDAD_MEDIA, TIPO_ATAQUE, NOMBRE_ANTI_SLIME, DESC_ANTI_SLIME) { }

        public override int RealizarAccion(Jugador jugador, Enemigo enemigo)
        {
            if (enemigo.GetType() != typeof(Slime))
                return RESULTADO_SIN_ACTIVAR;

            Arma arma = jugador.GetArmaCombate();

            int dano;
            if (arma.GetType().IsSubclassOf(typeof(ArmaFisica)))
            {
                int fue = jugador.GetStatCombate(Mob.INDICE_FUERZA);
                int ata = fue + arma.GetDano();
                dano = ata - enemigo.GetStatCombate(Mob.INDICE_DEFENSA);
            }
            else if (arma.GetType().IsSubclassOf(typeof(ArmaMagica)))
            {
                int mag = jugador.GetStatCombate(Mob.INDICE_MAGIA);
                int ata = mag + arma.GetDano();
                dano = ata - enemigo.GetStatCombate(Mob.INDICE_RESISTENCIA);
            }
            else
                throw new ArgumentException("Se ha utilizado un arma no válida." +
                    "Todas las armas deben heredar de " +
                    $"{typeof(ArmaFisica)} o de {typeof(ArmaMagica)}");

            int crit = 1 + jugador.GetCritico();
            dano *= 3;
            dano *= crit;
            if (enemigo.Danar(dano))
                return RESULTADO_MOB_DERROTADO;
            else
                return RESULTADO_ACTIVADA;
        }
    }
}
