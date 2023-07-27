using Framework.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Resources;
namespace WebOficinaDOM.Models
{
    public class ContenidoLogoBusinessAgent
    {
        private readonly RestClient _client;
        public ContenidoLogoBusinessAgent()
        {

            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }
        public ContenidoLogo GetLogo()
        {
            var request = new RestRequest("ContenidoLogo/GetContenido_Logo", Method.GET);

            var result = _client.Execute<List<ContenidoLogo>>(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(SeccionResouces.NoResponseService_P0);

            if (result.Data == null)
                throw new ArgumentException(SeccionResouces.NoResultsLogo_P0);

            if (result.Data.Count() > 0)
            {
                return result.Data.Where(x => x.Activo)
                .OrderByDescending(x => x.IdContenidoLogo)
                .FirstOrDefault();
            }
            return PostFirtLogo();
        }
        private ContenidoLogo PostFirtLogo()
        {
            var logo = new ContenidoLogo();
            logo.Activo = true;
            logo.Imagen = "sin-imagen.png";
            logo.RutaFisica = ConfigWrapper.Value<string>("UploadDirLogo");

            var request = new RestRequest("ContenidoLogo/PostContenido_Logo", Method.POST);
            request.AddParameter("usuario", "System", ParameterType.QueryString);
            request.AddJsonBody(logo);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(SeccionResouces.NoResponseService_P0);

            return logo;

        }

        public void PutLogo(string nombreArchivo, string usuario)
        {
            var logo = GetLogo();
            logo.RutaFisica = ConfigWrapper.Value<string>("UploadDirLogo");
            logo.Imagen = nombreArchivo;
            logo.Activo = true;

            var request = new RestRequest("ContenidoLogo/PutContenido_Logo", Method.PUT);
            request.AddParameter("id", logo.IdContenidoLogo, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(logo);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(SeccionResouces.NoResponseService_P0);
        }
        public bool UploadFile(HttpPostedFileBase file, string path)
        {
            if (file.ContentLength > 0)
            {
                file.SaveAs(path);

                var uriLogo = string.Format(ConfigWrapper.Value<string>("UriImagenLogo"),
                              Path.GetExtension(file.FileName));
                var pathReplica = HttpContext.Current.Server.MapPath(uriLogo);

                file.SaveAs(pathReplica);
                return true;
            }
            return false;
        }
        public string GetUriLogoOficina()
        {
            var filename = GetLogo().Imagen;
            var uriLogo = string.Format(ConfigWrapper.Value<string>("UriImagenLogo"),
                                   Path.GetExtension(filename)) + "?" + DateTime.Now.Ticks;
            return uriLogo;

        }
    }
}