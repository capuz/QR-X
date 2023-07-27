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
    public class ContenidoSecundarioBusinessAgent
    {
        public IEnumerable<ContenidoSecundario> GetContenidoActivo()
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("ContenidoSecundario/GetContenido_Secundario", Method.GET);

            var result = client.Execute<List<ContenidoSecundario>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            if (result.Data == null)
                throw new ArgumentException(ErrorMessageResources.NoContent);

            var datos = result.Data != null
                ? result.Data.Where(x => x.Activo == 1)
                : new List<ContenidoSecundario>();

            return datos;
        }
    }
}