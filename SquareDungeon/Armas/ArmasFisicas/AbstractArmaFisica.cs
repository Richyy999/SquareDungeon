using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Armas.ArmasFisicas
{
    abstract class AbstractArmaFisica : AbstractArma
    {

        protected AbstractArmaFisica(int dano, int usos, string nombre, string descripcion, AbstractHabilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override int GetDanoBase(AbstractMob mob)
        {
            int fue = this.portador.GetStatCombate(AbstractMob.INDICE_FUERZA);
            int ata = fue + this.dano;

            int defEnemiga = mob.GetStatCombate(AbstractMob.INDICE_DEFENSA);

            return ata - defEnemiga;
        }
    }
}
