using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Threading;

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
            if (string.IsNullOrWhiteSpace(texto)) return texto;

            texto = texto.Trim();

            string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

            if (File.Exists(fileNameIdioma))
            {
                using (StreamReader sr = new StreamReader(fileNameIdioma))
                {
                    string line;
                    string claveBuscada = texto + ":";

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith(claveBuscada, StringComparison.OrdinalIgnoreCase))
                        {
                            string traduccion = line.Substring(claveBuscada.Length).Trim();
                            return string.IsNullOrWhiteSpace(traduccion) ? texto : traduccion;
                        }
                    }
                }
            }
            return texto;
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
        }
}