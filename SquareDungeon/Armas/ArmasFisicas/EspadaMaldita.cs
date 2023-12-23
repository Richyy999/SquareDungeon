using SquareDungeon.Entidades.Mobs;

using static SquareDungeon.Resources.Resource;
using static SquareDungeon.Habilidades.SinHabilidad;

namespace SquareDungeon.Armas.ArmasFisicas
{
    /// <summary>
    /// Espada que calcula el daño en función de la resistencia mágica del enemigo.<br/>Reduce la fuerza de su <see cref="AbstractArma.portador">portador</see>
    /// </summary>
    class EspadaMaldita : AbstractArmaFisica
    {
        private const int USOS_MAX = 25;
        private const int DANO = 8;

        public EspadaMaldita() : base(DANO, USOS_MAX, NOMBRE_ESPADA_MALDITA, DESC_ESPADA_MALDITA, SIN_HABILIDAD)
        { }

        public override int GetDanoBase(AbstractMob mob)
        {
            int fue = portador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int ata = fue + this.dano;
            int resEnemiga = mob.GetStatCombate(AbstractMob.INDICE_RESISTENCIA);
            
            return ata - resEnemiga;
        }

        public override int Atacar(AbstractMob mob)
        {
            int dano = base.Atacar(mob);

            // TODO habilidad que disminuya la fuerza en combate

            return dano;
        }

        public override void RepararArma(int usos) { }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
