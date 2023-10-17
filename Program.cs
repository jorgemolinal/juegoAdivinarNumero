using System;

namespace adivinarNumero
{
    class Program
    {
        private static int numeroDeIntentos;
        private static int numerosDeIntentosElegidos;
        private static int numeroDeIntentosMaquina;
        private static bool continuaPrograma = true;
        private static List<int> numeros = new List<int>(); 

        private static int cargarRandomNum(int max)
        {
            Random rnd = new Random(); //crea un valor aleatorio del tipo random
            int objetivo = rnd.Next(0, max +1);//Con next(max, min) devuelvo un int positivo entre el rango min y max (incluye min y excluye max).
            return objetivo;
        }

        private static void derrota(bool isMaquina)
        {
            if (isMaquina)
            {
                Console.WriteLine("Has acabado el numero de intentos");
                if (JugarOtraPartida(isMaquina))
                {
                    inicioPartida();
                }
                else
                {
                    continuaPrograma = false;
                }
            }

        }

        private static bool JugarOtraPartida(bool isMaquina)
        {
            estadisticas(isMaquina);
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("¿Quieres jugar otra partida? (S/N)");
            //string respuesta = Console.ReadLine();

            if (Console.ReadLine() == "s" || Console.ReadLine() == "S")
            {
                return true;
            }
            else if (Console.ReadLine() == "N" || Console.ReadLine() == "n")
            {
                return false;
            }
            else { return JugarOtraPartida(isMaquina); }
        }

        private static void estadisticas(bool isMaquina)
        {
            if (isMaquina)
            {
                Console.WriteLine($"Ha hecho {numeroDeIntentosMaquina} intentos");
                Console.WriteLine($"Los numeros que ha utilizado son los siguientes:");

            } else
            {
                int intentosUtilizados = numerosDeIntentosElegidos - numeroDeIntentos;

                Console.WriteLine($"Has hecho {intentosUtilizados} intentos");
                Console.WriteLine($"Los numeros que has dicho son los siguientes:");
            }
            
            foreach (int num in numeros)
            {
                Console.WriteLine($"{num}");
            }
            numeros.Clear();

        }

        private static void jugar(int objetivo)
        {
            Console.WriteLine("Di un numero: ");
            int numero = Convert.ToInt32(Console.ReadLine());


            while (numeroDeIntentos > 0)
            {
                numeros.Add(numero);
                if(numero > objetivo)
                {
                    Console.WriteLine("El objetivo es menor, vuelve a probar");
                    numeroDeIntentos--;
                    Console.WriteLine($"Te quedan { numeroDeIntentos} intentos");
                    jugar(objetivo);
                } else if (numero < objetivo)
                {
                    Console.WriteLine("El objetivo es mayor, vuelve a probar");
                    numeroDeIntentos--;
                    Console.WriteLine($"Te quedan { numeroDeIntentos} intentos");
                    jugar(objetivo);
                } else
                {
                    Console.WriteLine("Enhorabuena has acertado el numero");
                    numeroDeIntentos--;
                    if (JugarOtraPartida(false))
                    {
                        inicioPartida();
                    }
                    else
                    {
                        continuaPrograma = false;
                    }
                }
            } 
            derrota(false);
              
        }

        private static void jugarMaquina(int objetivo, int max) //Paso el max para que la maquina sepa cual es el max
        {
            //El primer numero que genera la maquina
            int min = 0;
            int maxCambiante = max; //Inicialmente
            int numero =  (maxCambiante + min) /2 ;
            bool sinAcertar = true;

            while (sinAcertar)
            {
                numeros.Add(numero);
                if (numero == objetivo)
                {
                    Console.WriteLine("Enhorabuena la maquina ha acertado el numero");
                    numeroDeIntentosMaquina++;
                    if (JugarOtraPartida(true))
                    {
                        inicioPartida();
                    }
                    else
                    {
                        continuaPrograma = false;
                    }
                    break;
                }
                else if (numero < objetivo)
                {
                    numeroDeIntentosMaquina++;
                    min = numero+1;
                    numero =  (maxCambiante + min) /2 ;
                }
                else
                {
                    numeroDeIntentosMaquina++;
                    maxCambiante = numero - 1;
                    numero = (maxCambiante + min) / 2;
                }
            }

        }
        private static void inicioPartida()
        {
            do
            {
                Console.WriteLine("***********************************************");
                
                Console.WriteLine("Bienvenido,vamos a adivinar un numero del 0 al valor maximo que indiques");
                Console.WriteLine("Indica el valor maximo (numero entero): ");
                int max;
                if (!int.TryParse(Console.ReadLine(), out max)) //Convierte la entrada string en un int si se puede, si no da false y...
                {
                    Console.WriteLine("Entrada no válida. Por favor, introduce un número entero. \n");
                    inicioPartida();
                }
                int objetivo = cargarRandomNum(max); //Num aleatorio entre 0 y el maximo dado

                Console.WriteLine("Quieres jugar tú o que juegue la máquina?(YO/MAQUINA)");
                string jugador=Console.ReadLine();
                if (jugador == "YO" || jugador == "yo")
                {
                    Console.WriteLine("Cuantos intentos quieres?");
                    numeroDeIntentos = Convert.ToInt32(Console.ReadLine());
                    numerosDeIntentosElegidos = numeroDeIntentos;

                    Console.WriteLine($"El objetivo es : {objetivo}");
                    jugar(objetivo);

                }
                else if (jugador == "maquina" || jugador == "MAQUINA" || jugador == "MÁQUINA" || jugador == "máquina")
                {
                    numeroDeIntentosMaquina = 0;  
                    Console.WriteLine($"El objetivo es : {objetivo}");
                    jugarMaquina(objetivo, max);

                } else
                {
                    Console.WriteLine("Respuesta incorrecta.");
                    inicioPartida();
                }

            } while (continuaPrograma);
        }

        static void Main(string[] arg)
        {
            inicioPartida();

        }

        
    }
}
