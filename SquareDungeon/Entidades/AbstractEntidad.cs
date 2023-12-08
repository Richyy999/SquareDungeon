using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades
{
    abstract class AbstractEntidad
    {
        private string nombre;
        private string descripcion;

        protected AbstractEntidad(string nombre, string descripcion)
        {
            if (this is AbstractJugador)
                this.nombre = nombre;
            else
                this.nombre = GetPropiedad(FICHERO_NOMBRE_ENTIDADES, nombre);

            this.descripcion = GetPropiedad(FICHERO_DESC_ENTIDADES, descripcion);
        }

        public string GetNombre() => nombre;

        public string GetDescripcion() => descripcion;
    }
}
