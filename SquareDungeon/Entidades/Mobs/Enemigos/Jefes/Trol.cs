using SquareDungeon.Habilidades;
using SquareDungeon.Habilidades.Ataque.Jefes;
using SquareDungeon.Entidades.Mobs.Jugadores;

using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Entidades.Mobs.Enemigos.Jefes
{
    class Trol : Jefe
    {
        public Trol() :
            base(100, 3, 1, 2, 3, 2, 10, 40, 1000, 50, 10, 25, 40, 30, 50, 100,
                NOMBRE_TROL, DESC_TROL, 120, null, new HabilidadTrol())
        { }

        public override bool Atacar(Jugador jugador)
        {
            if (habilidad.Ejecutar())
            {
                EntradaSalida.MostrarHabilidad(this, habilidad);
                int resultadoHabilidad = habilidad.RealizarAccion(jugador, this);

                if (resultadoHabilidad != Habilidad.RESULTADO_SIN_ACTIVAR)
                    EntradaSalida.MostrarHabilidad(this, habilidad);

                return resultadoHabilidad == Habilidad.RESULTADO_MOB_DERROTADO;
            }

            return base.Atacar(jugador);
        }
    }
}
