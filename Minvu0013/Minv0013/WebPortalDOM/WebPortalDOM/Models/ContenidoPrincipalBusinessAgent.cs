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
    public class ContenidoPrincipalBusinessAgent
    {
        private readonly RestClient _client;
        public ContenidoPrincipalBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }

        public IEnumerable<ContenidoPrincipal> GetContenidoActivo()
        {
            var request = new RestRequest("ContenidoPrincipal/GetContenido_Principal", Method.GET);

            var result = _client.Execute<List<ContenidoPrincipal>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            if (result.Data != null)
            {
                result.Data = result.Data.Where(x => x.Activo == (byte)1).ToList();

                var numCol = 12;
                var numColXs = 12;

                switch (result.Data.Count())
                {
                    case 2:
                        numCol = 6;
                        numColXs = 6;
                        break;
                    case 3:
                        numCol = 4;
                        numColXs = 6;
                        break;
                    case 4:
                        numCol = 3;
                        numColXs = 6;
                        break;
                }
                var uriIcon = GetUriImagen();

                result.Data.ForEach(x =>
                {
                    x.NumCol = numCol;
                    x.NumColXs = numColXs;
                    x.Icono = string.IsNullOrEmpty(x.Icono) ? null : uriIcon + x.Icono;
                });
            }
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
                     ConfigWrapper.Value<string>("PathImagenesPrincipal"));
            }
            return string.Empty;
        }
    }
}