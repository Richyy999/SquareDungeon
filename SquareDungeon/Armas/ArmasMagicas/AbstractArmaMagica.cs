using SquareDungeon.Entidades.Mobs;
using SquareDungeon.Habilidades;

namespace SquareDungeon.Armas.ArmasMagicas
{
    /// <summary>
    /// Arma mágica con la que el jugador ataca al enemigo.<br/>Todas las armas mágicas deben heredar de esta clase
    /// </summary>
    abstract class AbstractArmaMagica : AbstractArma
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="dano">Daño básico del arma</param>
        /// <param name="usos">Usos máximos del arma</param>
        /// <param name="nombre">Nombre del arma</param>
        /// <param name="descripcion">Descripción del arma</param>
        /// <param name="habilidad">Habilidad del arma.<br/>Si el arma no posee ninguna habilidad se debe usar la constante <see cref="Habilidades.SinHabilidad">SIN_HABILIDAD</see></param>
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
