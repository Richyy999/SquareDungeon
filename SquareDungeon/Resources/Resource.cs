using System;
using System.IO;

using static System.IO.File;

namespace SquareDungeon.Resources
{
    class Resource
    {
        // Nombre ficheros
        public const string FICHERO_NOMBRE_ARMAS = @"\Resources\NombreArmas.properties";
        public const string FICHERO_NOMBRE_ENTIDADES = @"\Resources\NombreEntidades.properties";
        public const string FICHERO_NOMBRE_HABILIDADES = @"\Resources\NombreHabilidades.properties";
        public const string FICHERO_NOMBRE_OBJETOS = @"\Resources\NombreObjetos.properties";

        public const string FICHERO_DESC_ARMAS = @"\Resources\DescripcionArmas.properties";
        public const string FICHERO_DESC_ENTIDADES = @"\Resources\DescripcionEntidades.properties";
        public const string FICHERO_DESC_HABILIDADES = @"\Resources\DescripcionHabilidades.properties";
        public const string FICHERO_DESC_OBJETOS = @"\Resources\DescripcionObjetos.properties";

        // Nombre jefes
        public const string NOMBRE_TROL = "trol";

        // Nombre enemigos
        public const string NOMBRE_SLIME = "slime";
        public const string NOMBRE_ESQUELETO = "esqueleto";

        // Nombre cofres
        public const string NOMBRE_COFRE_HABILIDAD = "cofreHabilidad";
        public const string NOMBRE_COFRE_OBJETO = "cofreObjeto";
        public const string NOMBRE_COFRE_LLAVE_JEFE = "cofreLlaveJefe";
        public const string NOMBRE_COFRE_ARMA = "cofreArma";

        // Nombre habilidades
        public const string NOMBRE_SIN_HABILIDAD = "sinHabilidad";
        public const string NOMBRE_ANTI_SLIME = "antiSlime";
        public const string NOMBRE_DOS_POR_UNO = "2x1";
        public const string NOMBRE_BANO_MAGIA = "banoMagia";
        public const string NOMBRE_ASESINATO = "asesinato";
        public const string NOMBRE_SANACION = "sanacion";
        public const string NOMBRE_GOLPE_SANADOR = "golpeSanador";
        public const string NOMBRE_RENCOR = "rencor";

        public const string NOMBRE_HABILIDAD_TROL = "habTrol";

        // Nombre armas físicas
        public const string NOMBRE_ESPADA_HIERRO = "espHierro";
        public const string NOMBRE_VIOLA_SLIMES = "violaSlimes";
        public const string NOMBRE_ESPADA_MALDITA = "espadaMaldita";
        public const string NOMBRE_APLASTA_CRANEOS = "aplastaCraneos";
        public const string NOMBRE_DAGA_HIERRO = "dagaHierro";

        // Nombre armas mágicas
        public const string NOMBRE_GRIMORIO_BASICO = "grimBasico";
        public const string NOMBRE_BASTON_MAGICO = "bastonMagico";
        public const string NOMBRE_ESPADA_MAGICA = "espadaMagica";
        public const string NOMBRE_GRIMORIO_LETAL = "grimLetal";

        // Nombre objetos
        public const string NOMBRE_LLAVE_JEFE = "llaveJefe";
        public const string NOMBRE_POCION = "pocion";
        public const string NOMBRE_POCION_FUERZA = "pocionFuerza";
        public const string NOMBRE_POCION_MAGIA = "pocionMagia";
        public const string NOMBRE_POCION_LETAL = "pocionLetal";
        public const string NOMBRE_POCION_ASESINA = "pocionAsesina";

        // Descripción jugador
        public const string DESC_GUERRERO = "guerrero";
        public const string DESC_MAGO = "mago";
        public const string DESC_LADRON = "ladron";

        // Descripción jefes
        public const string DESC_TROL = "trol";

        // Descripción enemigos
        public const string DESC_SLIME = "slime";
        public const string DESC_ESQUELETO = "esqueleto";

        // Descripción cofres
        public const string DESC_COFRE_HABILIDAD = "cofreHabilidad";
        public const string DESC_COFRE_OBJETO = "cofreObjeto";
        public const string DESC_COFRE_LLAVE_JEFE = "cofreLlaveJefe";
        public const string DESC_COFRE_ARMA = "cofreArma";

        // Descripción habilidades
        public const string DESC_SIN_HABILIDAD = "sinHabilidad";
        public const string DESC_ANTI_SLIME = "antiSlime";
        public const string DESC_DOS_POR_UNO = "2x1";
        public const string DESC_BANO_MAGIA = "banoMagia";
        public const string DESC_ASESINATO = "asesinato";
        public const string DESC_SANACION = "sanacion";
        public const string DESC_GOLPE_SANADOR = "golpeSanador";
        public const string DESC_RENCOR = "rencor";

        public const string DESC_HABILIDAD_TROL = "habTrol";

        // Descripción armas físicas
        public const string DESC_ESPADA_HIERRO = "espHierro";
        public const string DESC_VIOLA_SLIMES = "violaSlimes";
        public const string DESC_ESPADA_MALDITA = "espadaMaldita";
        public const string DESC_APLASTA_CRANEOS = "aplastaCraneos";
        public const string DESC_DAGA_HIERRO = "dagaHierro";

        // Descripción armas mágicas
        public const string DESC_GRIMORIO_BASICO = "grimBasico";
        public const string DESC_BASTON_MAGICO = "bastonMagico";
        public const string DESC_ESPADA_MAGICA = "espadaMagica";
        public const string DESC_GRIMORIO_LETAL = "grimLetal";

        // Descripción objetos
        public const string DESC_LLAVE_JEFE = "llaveJefe";
        public const string DESC_POCION = "pocion";
        public const string DESC_POCION_FUERZA = "pocionFuerza";
        public const string DESC_POCION_MAGIA = "pocionMagia";
        public const string DESC_POCION_LETAL = "pocionLetal";
        public const string DESC_POCION_ASESINA = "pocionAsesina";

        public const char SALTO_LINEA = '/';

        private const string SEPARADOR = "=";

        public static string GetPropiedad(string file, string propiedad)
        {
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string[] propiedades = ReadAllLines(root + file);

            foreach (string linea in propiedades)
            {
                string[] prop = linea.Split(SEPARADOR);
                if (prop[0].Equals(propiedad))
                    return prop[1].Replace(SALTO_LINEA, '\n');
            }

            return null;
        }
    }
}
