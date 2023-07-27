using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using webApiDom.Models;
using NLog;
using System.Reflection;

namespace webApiDom
{
    public class clsAuditoria
    {
        private string[] columnas;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Auditoria(Object obj, int id, string usuario, string tabla, int transaccion)
        {
            domEntities db = new domEntities();

            try
            {

                switch (tabla)
                {
                    case "Noticia":
                        columnas = typeof(Noticia).GetProperties().Select(property => property.Name).ToArray();
                        break;
                    case "Menu":
                        columnas = typeof(Menu).GetProperties().Select(property => property.Name).ToArray();
                        break;
                    case "Organismo":
                        columnas = typeof(Organismo).GetProperties().Select(property => property.Name).ToArray();
                        break;
                    case "ContenidoLogo":
                        columnas = typeof(Contenido_Logo).GetProperties().Select(property => property.Name).ToArray();
                        break;
                    case "ContenidoPrincipal":
                        columnas = typeof(Contenido_Principal).GetProperties().Select(property => property.Name).ToArray();
                        break;
                    case "ContenidoSecundario":
                        columnas = typeof(Contenido_Secundario).GetProperties().Select(property => property.Name).ToArray();
                        break;
                }


                foreach (string columna in columnas)
                {
                    var property = obj.GetType().GetProperty(columna);
                    var value = property.GetValue(obj, null);

                    if (value == null)
                    {
                        value = "";
                    }

                    Auditoria auditoria = new Auditoria();
                    auditoria.IdAuditoria = 0;
                    auditoria.TipoTransaccion = transaccion;
                    auditoria.Tabla = tabla.ToString();
                    auditoria.CodigoTablaOrigen = id;
                    auditoria.Campo = columna.ToString();
                    auditoria.ValorOriginal = value.ToString();
                    auditoria.Fecha = DateTime.Now;
                    auditoria.Usuario = usuario.ToString();
                    db.Entry(auditoria).State = EntityState.Added;
                    db.Auditoria.Add(auditoria);

                }

                db.SaveChanges();


            }
            catch (Exception ex)
            {
                Log(3, 5, GetCurrentPageName(), MethodInfo.GetCurrentMethod().Name.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "");
            }



        }

        public void Log(int Tipo,
                   int Operacion,
                   string Servicio,
                   string Metodo,
                   string Cabezera = "",
                   string Exception = "",
                   string Usuario = "")
        {

            //Tipos de Log [1: INFO / 2: DEBUG / 3: ERROR]
            //Tipos de Operacion [1: INFO / 2: INSERT / 3: UPDATE / 4: DELETE / 5: ERROR]

            string sOperacion = "";
            string Cadena = "";

            try
            {

                switch (Operacion)
                {
                    case 1:
                        sOperacion = "INFO";
                        break;
                    case 2:
                        sOperacion = "INSERT";
                        break;
                    case 3:
                        sOperacion = "UPDATE";
                        break;
                    case 4:
                        sOperacion = "DELETE";
                        break;
                    case 5:
                        sOperacion = "ERROR";
                        break;
                }


                Cadena = "Operacion=" + sOperacion.ToString() +
                        "|Servicio=" + Servicio.ToString() +
                        "|Metodo=" + Metodo.ToString() +
                        "|Usuario=" + Usuario.ToString() +
                        "|Cabezera=" + Cabezera.ToString() +
                        "|Exception=" + Exception.ToString();


                switch (Tipo)
                {
                    case 1:
                        logger.Info(Cadena);
                        break;
                    case 2:
                        logger.Debug(Cadena);
                        break;
                    case 3:
                        logger.Fatal(Cadena);
                        break;
                }


            }
            catch (Exception ex)
            {
                logger.Fatal("Exception : " + ex.Message.ToString() + "| StackTrace : " + ex.StackTrace.ToString());
            }

        }

        public string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Directory.Name.ToString(); ;
            return sRet;
        }

    }
}