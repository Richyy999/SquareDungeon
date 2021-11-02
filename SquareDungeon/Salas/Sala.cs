using SquareDungeon.Entidades;

namespace SquareDungeon.Salas
{
    abstract class Sala
    {
        private byte x, y;

        private bool visitado;

        protected Entidad entidad;

        public Sala(byte x, byte y, Entidad entidad)
        {
            this.x = x;
            this.y = y;

            this.entidad = entidad;

            this.visitado = false;
        }

        public bool EstaVisitado() => visitado;

        public byte GetX() => x;

        public byte GetY() => y;

        public virtual Entidad GetEntidad() => entidad;
    }
}
