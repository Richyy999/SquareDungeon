using SquareDungeon.Entidades.Mobs;

namespace SquareDungeon.Habilidades.Cura
{
    /// <summary>
    /// Clase base de las habilidades que sanen al ejecutor. Todas las habilidades que sanen a su ejecutor deben heredar de esta clase
    /// </summary>
    abstract class AbstractSanacion : AbstractHabilidad
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="activacion">Probabilidad de activación de la habilidad</param>
        /// <param name="prioridad">Prioridad de la habilidad</param>
        /// <param name="nombre">Nombre de la habilidad</param>
        /// <param name="descripcion">Descripción de la habilidad</param>
        /// <param name="categoria">Categoría a la que pertenece la habilidad</param>
        protected AbstractSanacion(int activacion, int prioridad, string nombre, string descripcion, string categoria)
            : base(activacion, prioridad, nombre, descripcion, categoria) { }

        /// <summary>
        /// Sana al ejecutor de la habilidad la cantidad indicada
        /// </summary>
        /// <param name="receptor">Ejecutor de la habilidad que recibe la sanación</param>
        /// <param name="curacion">Cantidad de salud que sana el ejecutor</param>
        protected virtual void Sanar(AbstractMob receptor, int curacion)
        {
            receptor.SubirStat(AbstractMob.INDICE_VIDA, curacion);
        }
    }
}
