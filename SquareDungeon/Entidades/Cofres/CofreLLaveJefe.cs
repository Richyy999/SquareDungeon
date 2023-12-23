using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    /// <summary>
    /// Cofre que contiene la <see cref="LlaveJefe">llave del jefe</see>
    /// </summary>
    class CofreLLaveJefe : AbstractCofre
    {
        public CofreLLaveJefe() : base(NOMBRE_COFRE_LLAVE_JEFE, DESC_COFRE_LLAVE_JEFE, new LlaveJefe()) { }

        public override LlaveJefe AbrirCofre() => (LlaveJefe)contenido;
    }
}
