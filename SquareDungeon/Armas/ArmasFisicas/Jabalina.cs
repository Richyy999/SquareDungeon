﻿using System;

using static SquareDungeon.Habilidades.SinHabilidad;
using static SquareDungeon.Resources.Resource;

namespace SquareDungeon.Armas.ArmasFisicas
{
    internal class Jabalina : AbstractArmaFisica
    {
        private const int USOS_MAX = 50;
        private const int DANO = 6;
        public Jabalina() :
            base(DANO, USOS_MAX, NOMBRE_JABALINA, DESC_JABALINA, SIN_HABILIDAD)
        { }

        public override void RepararArma(int usos)
        {
            if (usos <= 0)
                throw new ArgumentException("usos", "No se puede reparar un arma con usos menores a 1");

            usos = usos / 2;
            if (this.usos + usos <= USOS_MAX)
                this.usos += usos;
            else
                this.usos = USOS_MAX;
        }

        public override int GetUsosMaximos() => USOS_MAX;
    }
}
