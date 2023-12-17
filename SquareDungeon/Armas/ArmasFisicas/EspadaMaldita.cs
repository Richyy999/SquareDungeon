using SquareDungeon.Entidades.Mobs;
using static SquareDungeon.Resources.Resource;
using SquareDungeon.Modelo;
using SquareDungeon.Habilidades.ReducirStats;

namespace SquareDungeon.Armas.ArmasFisicas
{
    class EspadaMaldita : AbstractArmaFisica
    {
        private const int USOS_MAX = 25;
        private const int DANO = 8;

        public EspadaMaldita() : base(DANO, USOS_MAX, NOMBRE_ESPADA_MALDITA, DESC_ESPADA_MALDITA, new Corrupcion())
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

            EjecutorHabilidades ejecutor = new EjecutorHabilidades(this.portador, portador, this.habilidad);

            return dano;
        }

        public override void RepararArma(int usos) { }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
