using System;

using SquareDungeon.Habilidades;
using SquareDungeon.Objetos;
using SquareDungeon.Entidades.Mobs.Jugadores;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    abstract class Jefe : Enemigo
    {
        protected Habilidad habilidad;

        protected Jefe(int pv, int fue, int mag, int agi, int def, int res, int probCrit, int danCrit,
            int pvMax, int fueMax, int magMax, int agiMax, int defMax, int resMax, int probCritMax, int danCritMax,
            string nombre, string descripcion, int dropExp, Objeto drop, Habilidad habilidad) :
            base(pv, fue, mag, agi, def, res, probCrit, danCrit,
                pvMax, fueMax, magMax, agiMax, defMax, resMax, probCritMax, danCritMax,
                nombre, descripcion, dropExp, drop)
        {
            if (habilidad.GetTipoHabilidad() != Habilidad.TIPO_ATAQUE)
                throw new ArgumentException("habilidad", "Los jefes solo pueden tener habilidades de tipo ataque");

            this.habilidad = habilidad;
        }
    }
}
