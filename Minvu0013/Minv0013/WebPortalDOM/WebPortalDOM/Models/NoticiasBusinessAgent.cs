using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPortalDOM.Models.DTO;
using RestSharp;
using Framework.Core;
using System.Net;
using WebPortalDOM.Resources;
namespace WebPortalDOM.Models
{
    public class NoticiasBusinessAgent
    {
        private readonly RestClient _client;
        public NoticiasBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }

        public IEnumerable<Noticia> GetNoticiasDestacadas()
        {
            var request =
                new RestRequest("NOTICIA/GetNOTICIA_HomePage", Method.GET);

            var result = _client.Execute<List<Noticia>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            if (result.Data != null)
            {
                var uriImagen = GetUriImagen();
                result.Data.ForEach(x => x.Imagen = uriImagen + x.Imagen);
            }
            return result.Data;
        }
        public Noticia GetNoticia(int? id)
        {
            var request =
                new RestRequest("NOTICIA/GetNOTICIA?id=" + id, Method.GET);

            var result = _client.Execute<Noticia>(request);

            if (result.StatusCode != HttpStatusCode.OK && result.StatusCode != HttpStatusCode.NotFound)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            if (result.Data != null)
            {
                var uriImagen = GetUriImagen();
                result.Data.Imagen = uriImagen + result.Data.Imagen;
            }

            return result.Data;
        }
        public IEnumerable<Noticia> GetNoticiasHistoricas()
        {
            var request =
                new RestRequest("NOTICIA/GetNOTICIA_Historica", Method.GET);

            var result = _client.Execute<List<Noticia>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            return result.Data;

        }
        private string GetUriImagen()
        {
            if (HttpContext.Current != null)
            {
                var request = HttpContext.Current.Request;
                return string.Format("{0}://{1}{2}{3}",
                     request.Url.Scheme,
                     request.Url.Authority,
                     VirtualPathUtility.ToAbsolute("~"),
                     ConfigWrapper.Value<string>("PathImagenesNoticia"));
            }
            return string.Empty;
        }
    }
}