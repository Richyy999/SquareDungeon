using System;

using SquareDungeon.Salas;
using SquareDungeon.Objetos;
using SquareDungeon.Armas;
using SquareDungeon.Habilidades;
using SquareDungeon.Entidades.Cofres;
using SquareDungeon.Armas.ArmasFisicas;
using SquareDungeon.Armas.ArmasMagicas;
using SquareDungeon.Habilidades.Cura;
using SquareDungeon.Habilidades.SubirStats;
using SquareDungeon.Habilidades.DobleGolpe;
using SquareDungeon.Habilidades.ReducirDano;
using SquareDungeon.Habilidades.DanoAdicional;
using SquareDungeon.Habilidades.DanoAdicional.TipoEnemigo;
using SquareDungeon.Habilidades.DanoAdicional.StatEjecutor;
using SquareDungeon.Entidades.Mobs.Enemigos;
using SquareDungeon.Entidades.Mobs.Jugadores;
using SquareDungeon.Entidades.Mobs.Enemigos.Jefes;

namespace SquareDungeon.Modelo
{
    /// <summary>
    /// Clase encargada de generar las instancias durante la ejecución del juego
    /// </summary>
    class Fabrica
    {
        private const int NUMERO_SALAS_MAXIMO = 64;

        private AbstractJugador jugador;

        /// <summary>
        /// Constructor de la clase. Crea al jugador
        /// </summary>
        public Fabrica()
        {
            jugador = generarJugador();
        }

        /// <summary>
        /// Devuelve al jugador
        /// </summary>
        /// <returns><see cref="AbstractJugador"/></returns>
        public AbstractJugador GetJugador() => jugador;

        /// <summary>
        /// Genera el personaje del jugador en función de su elección
        /// </summary>
        /// <returns><see cref="AbstractJugador"/></returns>
        /// <exception cref="ArgumentException">Lanza una excepción si se elige un personaje que no existe</exception>
        private AbstractJugador generarJugador()
        {
            string nombre = EntradaSalida.PedirNombre();
            string eleccion = EntradaSalida.Elegir("Elige un personaje:", false, EntradaSalida.GUERRERO, EntradaSalida.MAGO);
            switch (eleccion)
            {
                case EntradaSalida.GUERRERO:
                    jugador = new Guerrero(nombre, generarHabilidad());
                    jugador.EquiparArma(new EspadaHierro());
                    break;

                case EntradaSalida.MAGO:
                    jugador = new Mago(nombre, generarHabilidad());
                    GrimorioBasico grimorio = new GrimorioBasico();
                    jugador.EquiparArma(grimorio);
                    break;

                default:
                    throw new ArgumentException("Se ha seleccionado un personaje que no existe");
            }
            return jugador;
        }

        /// <summary>
        /// Genera el tablero y su contenido
        /// </summary>
        /// <returns>Tablero de juego</returns>
        public AbstractSala[,] GenerarTablero()
        {
            Random random = new Random();
            AbstractSala[,] tablero = new AbstractSala[8, 8];
            int numSalas = 0;

            // Creo una sala vacía donde aparece el jugador
            SalaVacia salaJugador = new SalaVacia(7, 0);
            salaJugador.SetEstado(AbstractSala.ESTADO_VISITADO);
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

        /// <summary>
        /// Devuelve coordenadas del tablero que estén vacías
        /// </summary>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns>Array con las coordenadas x e y respectivamente</returns>
        private int[] getPosicionLibre(AbstractSala[,] tablero)
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

        /// <summary>
        /// Genera el jefe del nivel
        /// </summary>
        /// <returns><see cref="AbstractJefe"/></returns>
        private AbstractJefe generarJefe()
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

        /// <summary>
        /// Genera las salas de enemigos
        /// </summary>
        /// <param name="salas"><see cref="SalaEnemigo"/> del tablero</param>
        /// <param name="tablero">Tablero de juego</param>
        private void crearSalasEnemigos(SalaEnemigo[] salas, AbstractSala[,] tablero)
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

        /// <summary>
        /// Genera una <see cref="SalaEnemigo"/>
        /// </summary>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns><see cref="SalaEnemigo"/></returns>
        /// <exception cref="IndexOutOfRangeException">Lanza una excepción si se genera un enemigo que no existe</exception>
        private SalaEnemigo generarSalaEnemigo(AbstractSala[,] tablero)
        {
            Random random = new Random();
            int num = random.Next(2);

            AbstractEnemigo enemigo;

            switch (num)
            {
                case 0:
                    enemigo = new Slime(new Pocion());
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

        /// <summary>
        /// Genera las salas de cofres
        /// </summary>
        /// <param name="salas"><see cref="SalaCofre"/> del tablero</param>
        /// <param name="tablero">Tablero de juego</param>
        private void crearSalasCofre(SalaCofre[] salas, AbstractSala[,] tablero)
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

        /// <summary>
        /// Genera una <see cref="SalaCofre"/>
        /// </summary>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns><see cref="SalaCofre"/></returns>
        /// <exception cref="IndexOutOfRangeException">Lanza una excepción si se genera un cofre que no existe</exception>
        public SalaCofre generarSalaCofre(AbstractSala[,] tablero)
        {
            Random random = new Random();
            int num = random.Next(3);

            AbstractCofre cofre;
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

        /// <summary>
        /// Genera una habilidad de forma aleatoria
        /// </summary>
        /// <returns><see cref="AbstractHabilidad"/></returns>
        /// <exception cref="IndexOutOfRangeException">Lanza una excepción si se genera una habilidad que no existe</exception>
        private AbstractHabilidad generarHabilidad()
        {
            Random random = new Random();
            int num = random.Next(8);
            AbstractHabilidad habilidad;

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

                case 3:
                    habilidad = new Asesinato();
                    break;

                case 4:
                    habilidad = new Sanacion();
                    break;

                case 5:
                    habilidad = new GolpeSanador();
                    break;

                case 6:
                    habilidad = new Rencor();
                    break;

                case 7:
                    habilidad = new Toxina();
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear enemigos está fuera del rango");
            }

            return habilidad;
        }

        /// <summary>
        /// Genera un objeto de forma aleatoria
        /// </summary>
        /// <returns><see cref="AbstractObjeto"/></returns>
        /// <exception cref="IndexOutOfRangeException">Lanza una excepción si se genera un objeto que no existe</exception>
        private AbstractObjeto generarObjeto()
        {
            Random random = new Random();
            int num = random.Next(5);
            AbstractObjeto objeto;

            switch (num)
            {
                case 0:
                    objeto = new Pocion();
                    break;

                case 1:
                    objeto = new PocionAsesina();
                    break;

                case 2:
                    objeto = new PocionFuerza();
                    break;

                case 3:
                    objeto = new PocionLetal();
                    break;

                case 4:
                    objeto = new PocionMagia();
                    break;

                default:
                    throw new IndexOutOfRangeException("El índice de crear objetos está fuera del rango");
            }

            return objeto;
        }

        /// <summary>
        /// Genera un arma según el tipo de arma del jugador
        /// </summary>
        /// <returns><see cref="AbstractArma"/></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private AbstractArma generarArma()
        {
            AbstractArma arma;
            Random random = new Random();
            if (jugador.GetTipoArmas().IsSubclassOf(typeof(AbstractArmaFisica)))
            {
                int num = random.Next(4);

                switch (num)
                {
                    case 0:
                        arma = new EspadaHierro();
                        break;

                    case 1:
                        arma = new ViolaSlimes();
                        break;

                    case 2:
                        arma = new EspadaMaldita();
                        break;

                    case 3:
                        arma = new AplastaCraneos();
                        break;

                    default:
                        throw new IndexOutOfRangeException("El índice de crear armas está fuera del rango");
                }
            }
            else
            {
                int num = random.Next(4);
                switch (num)
                {
                    case 0:
                        arma = new GrimorioBasico();
                        break;

                    case 1:
                        arma = new BastonMagico();
                        break;

                    case 2:
                        arma = new EspadaMagica();
                        break;

                    case 3:
                        arma = new GrimorioLetal();
                        break;

                    default:
                        throw new IndexOutOfRangeException("El índice de crear armas está fuera del rango");
                }
            }

            return arma;
        }

        /// <summary>
        /// Genera salas vacías
        /// </summary>
        /// <param name="salas"><see cref="SalaVacia"/> del tablero</param>
        /// <param name="tablero">Tablero de juego</param>
        private void crearSalasVacias(SalaVacia[] salas, AbstractSala[,] tablero)
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

        /// <summary>
        /// Genera una sala vacía
        /// </summary>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns><see cref="SalaVacia"/></returns>
        private SalaVacia generarSalasVacias(AbstractSala[,] tablero)
        {
            int[] posicion = getPosicionLibre(tablero);
            return new SalaVacia(posicion[0], posicion[1]);
        }
    }
}
