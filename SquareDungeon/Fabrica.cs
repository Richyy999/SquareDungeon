using System;

using SquareDungeon.Salas;
using SquareDungeon.Objetos;
using SquareDungeon.Armas;
using SquareDungeon.Habilidades;
using SquareDungeon.Habilidades.Ataque;
using SquareDungeon.Entidades.Cofres;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

namespace SquareDungeon
{
    static class Fabrica
    {
        private const int NUMERO_SALAS_MAXIMO = 64;

        public static Jugador GenerarJugador()
        {
            Jugador jugador;
            string nombre = EntradaSalida.PedirNombre();
            int eleccion = EntradaSalida.PedirPersonaje();
            switch (eleccion)
            {
                case EntradaSalida.ELECCION_GUERRERO:
                    jugador = new Guerrero(nombre, generarHabilidad());
                    EspadaHierro espadaHierro = new EspadaHierro();
                    jugador.EquiparArma(espadaHierro);
                    espadaHierro.SetPortador(jugador);
                    break;

                default:
                    throw new ArgumentException("Se ha seleccionado un personaje que no existe");
            }

            return jugador;
        }

        public static Sala[,] GenerarTablero()
        {
            Random random = new Random();
            Sala[,] tablero = new Sala[8, 8];
            int numSalas = 0;

            // Creo una sala vacía donde aparece el jugador
            SalaVacia salaJugador = new SalaVacia(7, 0);
            salaJugador.SetEstado(Sala.ESTADO_VISITADO);
            tablero[7, 0] = salaJugador;
            numSalas++;

            // Creo la sala del jefe
            int[] posicion = getPosicionLibre(tablero);
            SalaJefe salaJefe = new SalaJefe(posicion[0], posicion[1], generarJefe());
            tablero[posicion[0], posicion[1]] = salaJefe;
            numSalas++;

            // Creo la sala con el cofre que contiene la llave del jefe
            posicion = getPosicionLibre(tablero);
            CofreLLaveJefe cofreLLaveJefe = new CofreLLaveJefe();
            SalaCofre salaCofreJefe = new SalaCofre(posicion[0], posicion[1], cofreLLaveJefe);
            tablero[posicion[0], posicion[1]] = salaCofreJefe;
            numSalas++;

            // Creo entre 20 y 30 salas con enemigos
            int numSalasEnemigos = random.Next(20, 31);
            SalaEnemigo[] salasEnemigos = new SalaEnemigo[numSalasEnemigos];
            crearSalasEnemigos(salasEnemigos, tablero);
            numSalas += numSalasEnemigos;

            // Creo entre 15 y 20 salas con cofre
            int numSalasCofre = random.Next(15, 21);
            SalaCofre[] salasCofres = new SalaCofre[numSalasCofre];
            crearSalasCofre(salasCofres, tablero);
            numSalas += numSalasCofre;

            // Relleno el resto del tablero con salas vacías
            int numSalasVacias = NUMERO_SALAS_MAXIMO - numSalas;
            SalaVacia[] salasVacias = new SalaVacia[numSalasVacias];
            crearSalasVacias(salasVacias, tablero);

            return tablero;
        }

        private static int[] getPosicionLibre(Sala[,] tablero)
        {
            Random random = new Random();

            int[] posicion = new int[2];

            do
            {
                posicion[0] = random.Next(8);
                posicion[1] = random.Next(8);
            } while (tablero[posicion[0], posicion[1]] != null);

            return posicion;
        }

        private static Jefe generarJefe()
        {
            Random random = new Random();
            int num = random.Next(1);

            switch (num)
            {
                case 0:
                    return new Trol();

                default:
                    return null;
            }
        }

        private static void crearSalasEnemigos(SalaEnemigo[] salas, Sala[,] tablero)
        {
            for (int i = 0; i < salas.Length; i++)
            {
                SalaEnemigo salaEnemigo = generarSalaEnemigo(tablero);
                salas[i] = salaEnemigo;
                int x = salaEnemigo.GetX();
                int y = salaEnemigo.GetY();
                tablero[x, y] = salaEnemigo;
            }
        }

        private static SalaEnemigo generarSalaEnemigo(Sala[,] tablero)
        {
            Random random = new Random();
            int num = random.Next(2);

            Enemigo enemigo;

            switch (num)
            {
                case 0:
                    enemigo = new Slime(generarObjeto());
                    break;

                case 1:
                    enemigo = new Esqueleto(generarObjeto());
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear enemigos está fuera del rango");
            }

            int[] posicion = getPosicionLibre(tablero);
            SalaEnemigo salaEnemigo = new SalaEnemigo(posicion[0], posicion[1], enemigo);

            return salaEnemigo;
        }

        private static void crearSalasCofre(SalaCofre[] salas, Sala[,] tablero)
        {
            for (int i = 0; i < salas.Length; i++)
            {
                SalaCofre salaCofre = generarSalaCofre(tablero);
                salas[i] = salaCofre;
                int x = salaCofre.GetX();
                int y = salaCofre.GetY();
                tablero[x, y] = salaCofre;
            }
        }

        public static SalaCofre generarSalaCofre(Sala[,] tablero)
        {
            Random random = new Random();
            int num = random.Next(3);

            Cofre cofre;
            switch (num)
            {
                case 0:
                    cofre = new CofreHabilidad(generarHabilidad());
                    break;

                case 1:
                    cofre = new CofreObjeto(generarObjeto());
                    break;

                case 2:
                    cofre = new CofreArma(generarArma());
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear cofres está fuera del rango");
            }

            int[] posicion = getPosicionLibre(tablero);
            SalaCofre salaCofre = new SalaCofre(posicion[0], posicion[1], cofre);

            return salaCofre;
        }

        private static Habilidad generarHabilidad()
        {
            Random random = new Random();
            int num = random.Next(3);
            Habilidad habilidad;

            switch (num)
            {
                case 0:
                    habilidad = new AntiSlime();
                    break;
                case 1:

                    habilidad = new DosPorUno();
                    break;

                case 2:
                    habilidad = new BanoMagia();
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear enemigos está fuera del rango");
            }

            return habilidad;
        }

        private static Objeto generarObjeto()
        {
            Random random = new Random();
            int num = random.Next(1);
            Objeto objeto;

            switch (num)
            {
                case 0:
                    objeto = new Pocion();
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear objetos está fuera del rango");
            }

            return objeto;
        }

        private static Arma generarArma()
        {
            Random random = new Random();
            int num = random.Next(5);
            Arma arma;

            switch (num)
            {
                case 0:
                    arma = new EspadaHierro();
                    break;

                case 1:
                    arma = new GrimorioBasico();
                    break;

                case 2:
                    arma = new ViolaSlimes();
                    break;

                case 3:
                    arma = new EspadaMaldita();
                    break;

                case 4:
                    arma = new AplastaCraneos();
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear armas está fuera del rango");
            }

            return arma;
        }

        private static void crearSalasVacias(SalaVacia[] salas, Sala[,] tablero)
        {
            for (int i = 0; i < salas.Length; i++)
            {
                SalaVacia salaVacia = generarSalasVacias(tablero);
                salas[i] = salaVacia;
                int x = salaVacia.GetX();
                int y = salaVacia.GetY();
                tablero[x, y] = salaVacia;
            }
        }

        private static SalaVacia generarSalasVacias(Sala[,] tablero)
        {
            int[] posicion = getPosicionLibre(tablero);
            return new SalaVacia(posicion[0], posicion[1]);
        }
    }
}
