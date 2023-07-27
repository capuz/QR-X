using Framework.Core;
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
    public class ContenidoSecundarioBusinessAgent
    {

        private readonly RestClient _client;
        public ContenidoSecundarioBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }
        public IEnumerable<ContenidoSecundario> GetContenido()
        {
            var request = new RestRequest("ContenidoSecundario/GetContenido_Secundario", Method.GET);

            var result = _client.Execute<List<ContenidoSecundario>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            if (result.Data == null)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResultsContent_P0);

            return result.Data;
        }
        public ContenidoSecundario GetContenido(int id)
        {
            var request = new RestRequest("ContenidoSecundario/GetContenido_Secundario", Method.GET);
            request.AddParameter("id", id, ParameterType.QueryString);
            var result = _client.Execute<ContenidoSecundario>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            if (result.Data == null)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResultsContent_P0);

            return result.Data;
        }
        public void PostContenido(ContenidoSecundario contenido, string usuario)
        {
            var request =
               new RestRequest("ContenidoSecundario/PostContenido_Secundario", Method.POST);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(contenido);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

        }
        public void PutContenido(ContenidoSecundario contenido, string usuario)
        {
            var request =
               new RestRequest("ContenidoSecundario/PutContenido_Secundario", Method.PUT);
            request.AddParameter("id", contenido.IdContenidoSecundario, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(contenido);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

        }
        public ContenidoSecundario DeleteContenidoById(int id, string usuario)
        {
            var request =
                 new RestRequest("ContenidoSecundario/DeleteContenido_Secundario?id=" + id, Method.DELETE);
            request.AddParameter("id", id, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            var result = _client.Execute<ContenidoSecundario>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            return result.Data;
        }

    }
}