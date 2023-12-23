using SquareDungeon.Habilidades;
using SquareDungeon.Objetos;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    /// <summary>
    /// Clase básica que define a los jefes. Todos los jefes deben heredar de esta clase
    /// </summary>
    abstract class AbstractJefe : AbstractEnemigo
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="pv">Vida inicial del enemigo</param>
        /// <param name="fue">Fuerza inicial del enemigo</param>
        /// <param name="mag">Magia inicial del enemigo</param>
        /// <param name="agi">Agilidad inicial del enemigo</param>
        /// <param name="hab">Habilidad inicial del enemigo</param>
        /// <param name="def">Defensa inicial del enemigo</param>
        /// <param name="res">Resistencia mágica inicial del enemigo</param>
        /// <param name="probCrit">Probabilidad de crítico inicial del enemigo</param>
        /// <param name="danCrit">Daño crítico inicial del enemigo</param>
        /// <param name="pvMax">Vida máxima del enemigo</param>
        /// <param name="fueMax">Fuerza máxima del enemigo</param>
        /// <param name="magMax">Magia máxima del enemigo</param>
        /// <param name="agiMax">Agilidad máxima del enemigo</param>
        /// <param name="habMax">habilidad máxima del enemigo</param>
        /// <param name="defMax">Defensa máxima del enemigo</param>
        /// <param name="resMax">Resistencia mágica máxima del enemigo</param>
        /// <param name="probCritMax">Probabilidad de crítico máxima del enemigo</param>
        /// <param name="danCritMax">Daño crítico máximo del enemigo</param>
        /// <param name="nombre">Nombre del enemigo</param>
        /// <param name="descripcion">Descripción del enemigo</param>
        /// <param name="dropExp">Experiencia que deja al jugador cuando el enemigo es derrotado</param>
        /// <param name="drop">Objeto que deja al jugador cuando el enemigo es derrotado</param>
        protected AbstractJefe(int pv, int fue, int mag, int agi, int hab, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int habMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, AbstractObjeto drop, AbstractHabilidad habilidad) :
            base(pv, fue, mag, agi, hab, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, habMax, defMax, resMax, probCritMax, danCritMax,
                nombre, descripcion, dropExp, drop)
        {
            habilidades.Add(habilidad);
        }
    }
}
