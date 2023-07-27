using System;
using System.Configuration;

namespace Framework.Core
{

    /// <summary>
    /// Wrapper de archivo de configuración de la aplicación
    /// </summary>
    public static class ConfigWrapper
    {

        /// <summary>
        /// Obtiene una valor desde el archido de configuración de la aplicación
        /// </summary>
        /// <typeparam name="T">Tipo al que se espera convertir el valor</typeparam>
        /// <param name="key">Llave en el archivo de configuración con al que se obtiene el valor</param>
        /// <returns>Valor casteado</returns>
        public static T Value<T>(string key)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(ConfigurationManager.AppSettings[key], typeof(T));
            }
            catch (Exception)
            {
                result = default(T);
            }

            return result;
        }

        /// <summary>
        /// Obtiene una cadena de conexión en función de la llave entregada
        /// </summary>
        /// <param name="key">Llave en el archivo de configuración con al que se obtiene la cadena de conexión</param>
        /// <returns>Cadena de conexión</returns>
        public static string ConnectionString(string key)
        {
            var result = ConfigurationManager.ConnectionStrings[key].ConnectionString;

            return result;
        }

    }

}