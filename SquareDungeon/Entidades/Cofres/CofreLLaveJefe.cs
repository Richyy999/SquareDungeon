using SquareDungeon.Objetos;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Cofres
{
    class CofreLLaveJefe : Cofre
    {
        public CofreLLaveJefe() : base(NOMBRE_COFRE_LLAVE_JEFE, DESC_COFRE_LLAVE_JEFE, new LlaveJefe()) { }

        public override LlaveJefe AbrirCofre() => (LlaveJefe)contenido;
    }
}
