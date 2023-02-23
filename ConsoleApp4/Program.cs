using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static StreamReader Lectura;
        static TextReader Leer;
        static StreamWriter escritura, temporal;
        static string cadena, respuesta;
        static string hoja, texto;
        static string numerodehoja;
        static bool encontrado;
        static string[] campos = new string[2];
        public static void Main(string[] args)
        {
            menu();
            Console.ReadKey(true);

        }
        static void menu()
        {
            byte opcion;
            opcion = 0;
            do
            {
                try

                {
                    Console.WriteLine("Menu de Opciones");
                    Console.WriteLine("1. Creando Número de Hojas");
                    Console.WriteLine("2. Consultas");
                    Console.WriteLine("3. Modificaciones");
                    Console.WriteLine("4. Leyendo archivo");
                    Console.WriteLine("5. Salir");
                    Console.WriteLine("Que deseas hacer?...");
                    opcion = Convert.ToByte(Console.ReadLine());
                    switch (opcion)
                    {
                        case 1:

                            crearArchivos();
                            NumeroDeHojas();

                            break;

                        case 2:
                            consultas();
                            break;
                        case 3:
                            modificaciones();
                            break;
                        case 4:
                            leerArchivo();
                            break;
                        case 5:
                            Console.WriteLine("*********************************");
                            Console.WriteLine("***********Aplicacion Finalizda****************");
                            Console.WriteLine("***************************");
                            break;
                        default:
                            Console.WriteLine("*********************************");
                            Console.WriteLine("***********Opcion Incorrecta****************");
                            Console.WriteLine("***************************");
                            break;

                    }
                }

                catch (FormatException fe)
                {
                    Console.WriteLine("*********************************");
                    Console.WriteLine("***********Error****************" + fe.Message);
                    Console.WriteLine("***************************");
                }
                catch (Exception e)
                {
                    Console.WriteLine("*********************************");
                    Console.WriteLine("***********Error****************" + e.Message);
                    Console.WriteLine("***************************");
                }

            } while (opcion != 6);
        }

        //Creando el método altas

        [Obsolete]
        static void leerArchivo()
        {

            //Leer = new StreamReader("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/VM_sco03_19.wri");

            //Leer.Close();

            string strContains = "HOJA:  1208";
            string strFolderScan = "C:/Users/jtarrillo/Downloads/chanfletoide";
            string strExtension = ".wri";

            DirectoryInfo dir = new DirectoryInfo(strFolderScan);
            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);

            IEnumerable<FileInfo> fileQuery =
            from file in fileList
            where file.Extension == strExtension//".wri"
            orderby file.Name
            select file;

            Console.WriteLine("INI::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            foreach (FileInfo fi in fileQuery)
            {
                int counter = 0;
                string[] lines = File.ReadAllLines(fi.FullName);

                Console.WriteLine("LOCATION " + fi.FullName);

                foreach (string line in lines)
                {
                    counter++;
                    if (line.Contains(strContains))//Hoja
                        Console.WriteLine("\t" + "LINE " + counter + " :: " + line);
                }
            }
            Console.WriteLine("FIN::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.ReadKey();
        } 

        static void crearArchivos()
        {
            
            escritura = File.AppendText("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
            Console.WriteLine("Ingresa el texto que quieres agregar al archivo");
            texto = Console.ReadLine().ToString();
        
            escritura.Close();
        }
        // Creando el método altas
        static void NumeroDeHojas()
        {
            encontrado = false;
            try
            {
               
                escritura = File.AppendText("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
                if (encontrado == false)
                {
                    
                    Console.Write("Ingresa el numero de hoja: ");
                    numerodehoja = Console.ReadLine();
                  
                  

                    //scribiendo los datos en el archivo
                    escritura.WriteLine(texto + ":  " + numerodehoja);
                    Console.WriteLine("*********************************");
                    Console.WriteLine("***********Registro agregado Correctamente****************");
                    Console.WriteLine("***************************");
                }
                else
                {
                    Console.WriteLine("*********************************");
                    Console.WriteLine("***********Ya existe un auto con el No de hoja" + hoja);
                    Console.WriteLine("***************************");
                }
                escritura.Close();
            }
            catch (FileNotFoundException fn)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + fn.Message);
                Console.WriteLine("***************************");
            }
            catch (Exception e)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + e.Message);
                Console.WriteLine("***************************");
            }
            finally
            {
               
                escritura.Close();
            }
        }
  


        //Creando consultas 
        static void consultas()
        {
            encontrado = false;
            try
            {
                Lectura = File.OpenText("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
                Console.WriteLine("Ingrese el numero de hoja: ");
                numerodehoja = Console.ReadLine();
                cadena = Lectura.ReadLine();
                while (cadena != null)
                {
                    campos = cadena.Split(':');

                    if (campos[1].Trim().Equals(numerodehoja))
                    {
                        encontrado = true;
                        Console.WriteLine("**************************");
                        Console.WriteLine("Numero de hoja encontrada");
                        Console.WriteLine("Numero de Hoja: " + campos[1]);
         
                        Console.WriteLine("*******************************************");


                    }
                    cadena = Lectura.ReadLine();
                }
                if (encontrado == false)
                {

                    Console.WriteLine("***************");
                    Console.WriteLine("No existe ese numero de hoja " + numerodehoja);
                    Console.WriteLine("*************");
                }
                Lectura.Close();
            }

            catch (FileNotFoundException fn)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + fn.Message);
                Console.WriteLine("***************************");
            }
            catch (Exception e)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + e.Message);
                Console.WriteLine("***************************");
            }
            finally
            {
                Lectura.Close();

            }

        }
        //Creando el método modificaciones

        static void modificaciones()
        {
            encontrado = false;
            byte opcionM;
            opcionM = 0;
            string nombredelahoja, nuevoNumerodeHoja;
         
            try
            {
                Lectura = File.OpenText("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
                temporal = File.CreateText("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/tmp.txt");
                Console.Write("Ingresa el No. de hoja  que desea modificar: ");
                hoja = Console.ReadLine();
                hoja = hoja.ToUpper();
                cadena = Lectura.ReadLine();

                while (cadena != null)
                {
                    campos = cadena.Split(':');

                    if (campos[1].Trim().Equals(hoja))
                    {
                        encontrado = true;
                        Console.WriteLine("**************************");
                        Console.WriteLine("Hoja encontrada con los siguientes datos");
                        Console.WriteLine("Hoja: " + campos[0]);
                        Console.WriteLine("Numero de Hoja: " + campos[1]);
                        Console.WriteLine("*******************************************");
                        Console.WriteLine("Es el registro que buscabas(SI/No)");

                        respuesta = Console.ReadLine();
                        respuesta = respuesta.ToUpper();
                        if (respuesta.Equals("SI"))
                        {
                            Console.WriteLine("Menu de Opciones para modificar");
                            Console.WriteLine("1. Nombre de la hoja");
                            Console.WriteLine("2. Numero de Hoja");
                            Console.Write("Que deseas Modificar?...");
                            opcionM = Convert.ToByte(Console.ReadLine());
                            switch (opcionM)
                            {
                                case 1:
                                    Console.Write("Cambie el nombre de la hoja por folio:");
                                    nombredelahoja = Console.ReadLine();
                                    nombredelahoja = nombredelahoja.ToUpper();
                                    temporal.WriteLine(nombredelahoja +  ":" + campos[1]
                                       );
                                    Console.WriteLine("***************");
                                    Console.WriteLine("Registro Modificado");
                                    Console.WriteLine("*************");
                                    break;
                                case 2:
                                    Console.Write("Ingresa el Nuevo numero de hoja:");
                                    nuevoNumerodeHoja = Console.ReadLine();
                                    nuevoNumerodeHoja = nuevoNumerodeHoja.ToUpper();
                                    temporal.WriteLine(campos[0] + ":  " + nuevoNumerodeHoja);
                                    Console.WriteLine("***************");
                                    Console.WriteLine("Registro Modificado");
                                    Console.WriteLine("*************");
                                    break;
                              
                                default:

                                    Console.WriteLine("*********************************");
                                    Console.WriteLine("Opcion incorrecta");
                                    Console.WriteLine("***************************");
                                    break;
                            }
                        }

                        else
                        {
                            temporal.WriteLine(cadena);

                        }
                    }
                    else
                    {

                        temporal.WriteLine(cadena);
                    }
                    cadena = Lectura.ReadLine();
                }
                if (encontrado == false)
                {
                    Console.WriteLine("***************");
                    Console.WriteLine("No se encontro ");
                    Console.WriteLine("*************");
                }
                Lectura.Close();
                temporal.Close();
                File.Delete("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
                File.Move("C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/tmp.txt", "C:/Users/jtarrillo/source/repos/ConsoleApp4/ConsoleApp4/hojas.txt");
            }
            catch (FileNotFoundException fn)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + fn.Message);
                Console.WriteLine("***************************");
            }
            catch (Exception e)
            {

                Console.WriteLine("*********************************");
                Console.WriteLine("***********Error****************" + e.Message);
                Console.WriteLine("***************************");
            }
            finally
            {
                Lectura.Close();
                temporal.Close();


            }

        }
    }
}
