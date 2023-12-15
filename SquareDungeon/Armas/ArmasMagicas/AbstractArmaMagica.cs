using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Armas.ArmasMagicas
{
    abstract class AbstractArmaMagica : AbstractArma
    {
        protected AbstractArmaMagica(int dano, int usos, string nombre, string descripcion, AbstractHabilidad habilidad) :
            base(dano, usos, nombre, descripcion, habilidad)
        { }

        public override int GetDanoBase(AbstractMob mob)
        {
            int mag = portador.GetStatCombate(AbstractMob.INDICE_MAGIA);
            int ata = mag + this.dano;
            int resEnemiga = mob.GetStatCombate(AbstractMob.INDICE_RESISTENCIA);

            return dano = ata - resEnemiga;
        }
    }
}
