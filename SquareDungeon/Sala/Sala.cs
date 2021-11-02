using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareDungeon.Sala
{
    abstract class Sala
    {
        private byte x, y;

        private bool visitado;

        protected Entidad.Entidad entidad;

        public Sala(byte x, byte y, Entidad.Entidad entidad)
        {
            this.x = x;
            this.y = y;

            this.entidad = entidad;

            this.visitado = false;
        }

        public bool EstaVisitado() => visitado;

        public byte GetX() => x;

        public byte GetY() => y;

        public virtual Entidad.Entidad GetEntidad() => entidad;
    }
}
