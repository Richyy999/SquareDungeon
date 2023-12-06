using System;

namespace SquareDungeon.Entidades.Cofres
{
    abstract class Cofre : Entidad
    {
        protected Object contenido;

        protected Cofre(string nombre, string descripcion, Object contenido) : base(nombre, descripcion)
        {
            this.contenido = contenido;
        }

        public abstract Object AbrirCofre();
    }
}
