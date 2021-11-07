using System;
using System.IO;
using System.Reflection;

using static System.IO.File;

namespace SquareDungeon.Resources
{
    static class Resource
    {
        public const string FICHERO_NOMBRE_ARMAS = @"\Resources\NombreArmas.properties";
        public const string FICHERO_NOMBRE_ENTIDADES = @"\Resources\NombreEntidades.properties";
        public const string FICHERO_NOMBRE_HABILIDADES = @"\Resources\NombreHabilidades.properties";

        public const string FICHERO_DESC_ARMAS = @"\Resources\DescripcionArmas.properties";
        public const string FICHERO_DESC_ENTIDADES = @"\Resources\DescripcionEntidades.properties";
        public const string FICHERO_DESC_HABILIDADES = @"\Resources\DescripcionHabilidades.properties";

        public const string NOMBRE_SLIME = "slime";

        public const string NOMBRE_SIN_HABILIDAD = "sinHabilidad";
        public const string NOMBRE_ANTI_SLIME = "antiSlime";

        public const string NOMBRE_ESPADA_HIERRO = "espHierro";
        public const string NOMBRE_GRIMORIO_BASICO = "grimBasico";

        public const string DESC_GUERRERO = "guerrero";

        public const string DESC_SLIME = "slime";

        public const string DESC_SIN_HABILIDAD = "sinHabilidad";
        public const string DESC_ANTI_SLIME = "antiSlime";

        public const string DESC_ESPADA_HIERRO = "espHierro";
        public const string DESC_GRIMORIO_BASICO = "grimBasico";

        private const string SEPARADOR = "=";

        public static string GetPropiedad(string file, string propiedad)
        {
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string[] propiedades = ReadAllLines(root + file);

            foreach (string linea in propiedades)
            {
                string[] prop = linea.Split(SEPARADOR);
                if (prop[0].Equals(propiedad))
                    return prop[1];
            }

            return null;
        }
    }
}
