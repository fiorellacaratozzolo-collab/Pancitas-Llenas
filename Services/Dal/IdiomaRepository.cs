using Services.Bll.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Dal
    {
        public sealed class IdiomaRepository
        {
            #region Singleton
            private readonly static IdiomaRepository _instance = new IdiomaRepository();

            public static IdiomaRepository Current
            {
                get
                {
                    return _instance;
                }
            }

            private IdiomaRepository()
            {
                //Implent here the initialization of your singleton
            }
            #endregion

            private static string folder = ConfigurationManager.AppSettings["I18NFolderPath"];

            private static string fileName = ConfigurationManager.AppSettings["I18NFileName"];

            private static string path = default;
            static IdiomaRepository()
            {
                path = Path.Combine(folder, fileName);
            }

            public static string Traducir(string texto)
            {
                // Si nos mandan un texto vacío, no hacemos nada
                if (string.IsNullOrWhiteSpace(texto)) return texto;

                // Limpiamos espacios en blanco al principio y al final por las dudas
                texto = texto.Trim();

                string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

                if (File.Exists(fileNameIdioma))
                {
                    using (StreamReader sr = new StreamReader(fileNameIdioma))
                    {
                        string line;
                        // Armamos la clave exacta que vamos a buscar (Ej: "Nombre:")
                        string claveBuscada = texto + ":";

                        while ((line = sr.ReadLine()) != null)
                        {
                            // Buscamos si la línea EMPIEZA con nuestra clave exacta
                            if (line.StartsWith(claveBuscada, StringComparison.OrdinalIgnoreCase))
                            {
                                // Recortamos la clave y devolvemos solo la traducción
                                string traduccion = line.Substring(claveBuscada.Length).Trim();

                                // Si la traducción está vacía en el archivo, devolvemos el texto original
                                return string.IsNullOrWhiteSpace(traduccion) ? texto : traduccion;
                            }
                        }
                    }
                }

                // Si recorrió todo el archivo y no lo encontró, lanza la excepción
                throw new PalabraNoEncontradaException(texto);
            }

            public static List<CultureInfo> ObtenerIdiomas()
            {
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(folder);

                    List<CultureInfo> idiomas = new List<CultureInfo>();

                    foreach (FileInfo fo in directoryInfo.GetFiles())
                    {
                        idiomas.Add(new CultureInfo(fo.Extension.Replace(".", "")));
                    }

                    return idiomas;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static void AgregarPalabra(string palabra, string traduccion = "")
            {
                if (string.IsNullOrWhiteSpace(palabra)) return;

                // Limpiamos la palabra y le quitamos saltos de línea (Enters) que rompen el archivo de texto
                palabra = palabra.Trim().Replace("\r", "").Replace("\n", " ");

                string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

                // ---> EL FRENO: Verificamos si YA existe antes de agregarla <---
                bool yaExiste = false;

                if (File.Exists(fileNameIdioma))
                {
                    using (StreamReader sr = new StreamReader(fileNameIdioma))
                    {
                        string line;
                        string claveBuscada = palabra + ":";

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.StartsWith(claveBuscada, StringComparison.OrdinalIgnoreCase))
                            {
                                yaExiste = true;
                                break; // Cortamos la búsqueda, ya sabemos que está
                            }
                        }
                    }
                }

                // Si NO existe, recién ahí abrimos el archivo para escribir
                if (!yaExiste)
                {
                    using (StreamWriter sw = new StreamWriter(fileNameIdioma, true)) // true = Append
                    {
                        sw.WriteLine($"{palabra}:{traduccion}");
                    }
                }
            }
        }
}

