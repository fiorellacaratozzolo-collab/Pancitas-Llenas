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
            string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

            using (StreamReader sr = new StreamReader(fileNameIdioma))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');

                    if (parts.Length == 2 && parts[0].Trim().ToLower() == texto.ToLower())
                    {
                        //Si estoy acá significa que encontré la clave buscada.
                        return parts[1].Trim();
                    }
                }
            }
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
            string fileNameIdioma = Path.Combine(folder, fileName + "." + Thread.CurrentThread.CurrentCulture.Name);

            using (StreamWriter sw = new StreamWriter(fileNameIdioma, true)) //Append: Sirve para agregar data al final del archivo
            {
                sw.WriteLine($"{palabra}:{traduccion}");
            }
        }
    }
}
