using Framework.Core;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models
{
    public class LogBusinessAgent
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly RestClient _client;
        public LogBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }
        public void PostLog(int tipo, string usuario, string mensaje, string cabecera)
        {

            var httpRequest = HttpContext.Current.Request;
            var log = new Log();
            log.TipoAuditoria = tipo;
            log.Usuario = usuario;
            log.Mensaje = mensaje;
            log.Cabecera = cabecera;
            log.Pagina = httpRequest.RawUrl;
            log.Browser = httpRequest.Browser.Browser;
            log.Fecha = DateTime.Now;

            log.Ip = httpRequest.UserHostAddress;

            if (httpRequest.RequestContext.RouteData.Values.ContainsKey("controller"))
                log.Clase = (string)httpRequest.RequestContext.RouteData.Values["controller"];

            if (httpRequest.RequestContext.RouteData.Values.ContainsKey("action"))
                log.Evento = (string)httpRequest.RequestContext.RouteData.Values["action"];

            log.Pagina = httpRequest.Url.PathAndQuery;

            switch (tipo)
            {
                case 1:
                    logger.Info(mensaje);
                    break;
                case 2:
                    logger.Debug(mensaje);
                    break;
                case 3:
                    logger.Fatal(mensaje);
                    break;
            }


            var request = new RestRequest("Log/PostLog", Method.POST);
            request.AddJsonBody(log);
            _client.Execute(request);

        }

    }
}