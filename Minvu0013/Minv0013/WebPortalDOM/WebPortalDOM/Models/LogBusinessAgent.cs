using Framework.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebPortalDOM.Models.DTO;


namespace WebPortalDOM.Models
{
    public class LogBusinessAgent
    {

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

            var request = new RestRequest("Log/PostLog", Method.POST);
            request.AddJsonBody(log);
            _client.Execute(request);

        }

    }
}