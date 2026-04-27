using Services.Bll.CustomExceptions;
using Services.Dal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Bll
{
    public sealed class IdiomaService
    {
        private readonly static IdiomaService _instance = new IdiomaService();

        public static IdiomaService Current
        {
            get
            {
                return _instance;
            }
        }

        private IdiomaService()
        {
            //Implent here the initialization of your singleton
        }


        public static string Traducir(string texto)
        {
            try
            {
                return IdiomaRepository.Traducir(texto);
            }
            catch (PalabraNoEncontradaException ex)
            {
                // Que podría hacer?
                // 1) Puedo agregar la clave en el idioma que se solicita actualmente, con valor vacío
                // 2) Puedo agregar la clave en el idioma padre y/o todos aquellos otros idiomas que no tengan la palabra
                // 3) Puedo no hacer nada
                // 4) Puedo llamar a un modelito LLM, buscar la traducción y agregarla como value en l paso 1 o 2

                // En nuestro DEMO tomamos la determinación de ir por la opción 1
                IdiomaRepository.AgregarPalabra(texto);

                Console.WriteLine($"La palabra que no pudo traducirse fue: {ex.Palabra}. ");

                return texto; //En nuestro una palabra que no pudo traducirse, retorna la misma palabra
            }
            catch (Exception ex)
            {
                //Si estoy acá, es una exception más genérica. Para ver después
                //Bitácora y relanzamos la excepción

                throw ex;
            }
        }

        public static List<CultureInfo> ObtenerIdiomas()
        {
            return IdiomaRepository.ObtenerIdiomas();
        }
    }

}

