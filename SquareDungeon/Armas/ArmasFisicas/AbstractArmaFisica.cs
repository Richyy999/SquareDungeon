using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Armas.ArmasFisicas
{
    /// <summary>
    /// Arma física con la que el jugador ataca al enemigo.<br/>Todas las armas mágicas deben heredar de esta clase
    /// </summary>
    abstract class AbstractArmaFisica : AbstractArma
    {

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="dano">Daño básico del arma</param>
        /// <param name="usos">Usos máximos del arma</param>
        /// <param name="nombre">Nombre del arma</param>
        /// <param name="descripcion">Descripción del arma</param>
        /// <param name="habilidad">Habilidad del arma.<br/>Si el arma no posee ninguna habilidad se debe usar la constante <see cref="Habilidades.SinHabilidad">SIN_HABILIDAD</see></param>
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
