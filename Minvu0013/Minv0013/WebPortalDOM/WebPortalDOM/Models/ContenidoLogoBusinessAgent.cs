using Framework.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebPortalDOM.Models.DTO;
using WebPortalDOM.Resources;

namespace WebPortalDOM.Models
{
    public class ContenidoLogoBusinessAgent
    {
        private readonly RestClient _client;
        public ContenidoLogoBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }
        public string GetUriLogo()
        {
            var request = new RestRequest("ContenidoLogo/GetContenido_Logo", Method.GET);

            var result = _client.Execute<List<ContenidoLogo>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? ErrorMessageResources.NoResponseService);

            if (result.Data != null)
            {
                var filename = result.Data.Where(x => x.Activo == 1)
                .OrderByDescending(x => x.IdContenidoLogo).Select(x => x.Imagen)
                .FirstOrDefault();
                var uriLogo = GetUriImagen();
                return uriLogo + filename;
            }
            return string.Empty;


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
                     ConfigWrapper.Value<string>("PathImagenLogo"));
            }
            return string.Empty;
        }
    }
}