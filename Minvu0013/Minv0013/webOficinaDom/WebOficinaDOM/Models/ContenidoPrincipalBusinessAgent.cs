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
    public class ContenidoPrincipalBusinessAgent
    {
        private int CANTIDAD_MAXIMA = ConfigWrapper.Value<int>("MaximoContenidoPrincipal");
        private int CANTIDAD_MINIMA = ConfigWrapper.Value<int>("MinimoContenidoPrincipal");
        private readonly RestClient _client;
        public ContenidoPrincipalBusinessAgent()
        {

            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }

        public IEnumerable<ContenidoPrincipal> GetContenido()
        {
            var request = new RestRequest("ContenidoPrincipal/GetContenido_Principal", Method.GET);

            var result = _client.Execute<List<ContenidoPrincipal>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            if (result.Data == null)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResultsContent_P0);

            foreach (var item in result.Data)
            {
                switch (item.Activo)
                {
                    case false:
                        item.ActivoString = "Inhabilitado";
                        break;
                    case true:
                        item.ActivoString = "Habilitado";
                        break;
                    default:
                        item.ActivoString = "No definido";
                        break;
                }
            }
            return result.Data;
        }

        private void ValidateCreateContenidoPrincipal()
        {
            IEnumerable<ContenidoPrincipal> data = GetContenido();
            var cantidad = data.Count();
            if (cantidad >= CANTIDAD_MAXIMA)
                throw new ArgumentException(string.Format(SeccionResouces.CreateContenidoPrincipal_P1, CANTIDAD_MAXIMA));

        }
        private void ValidateUpdateContenidoPrincipal(ContenidoPrincipal contenido)
        {
            IEnumerable<ContenidoPrincipal> data = GetContenido();

            var activos = data.Where(x => x.Activo);
            var cantidadActivos = activos.Count();

            var exActivo = activos.Any(x => x.IdContenidoPrincipal == contenido.IdContenidoPrincipal);
            if (cantidadActivos <= CANTIDAD_MINIMA && contenido.Activo == false && exActivo)
                throw new ArgumentException(string.Format(SeccionResouces.DisableContenidoPrincipal_P1, CANTIDAD_MINIMA));

        }
        private void ValidateDeleteContenidoPrincipal()
        {
            IEnumerable<ContenidoPrincipal> data = GetContenido();

            var cantidad = data.Count();
            if (cantidad <= CANTIDAD_MINIMA)
                throw new ArgumentException(string.Format(SeccionResouces.DeleteContenidoPrincipal_P1, CANTIDAD_MINIMA));

        }

        public void PostContenidoPrincipal(ContenidoPrincipal contenido, string usuario)
        {
            ValidateCreateContenidoPrincipal();

            var request = new RestRequest("ContenidoPrincipal/PostContenido_Principal", Method.POST);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(contenido);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);
        }

        public void PutContenidoPrincipal(ContenidoPrincipal contenido, string usuario)
        {
            ValidateUpdateContenidoPrincipal(contenido);
            var request = new RestRequest("ContenidoPrincipal/PutContenido_Principal", Method.PUT);
            request.AddParameter("id", contenido.IdContenidoPrincipal, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(contenido);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);
        }

        public ContenidoPrincipal DeleteContenidoPrincipalById(int id, string usuario)
        {
            ValidateDeleteContenidoPrincipal();
            var request =
                 new RestRequest("ContenidoPrincipal/DeleteContenido_Principal", Method.DELETE);
            request.AddParameter("id", id, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            var result = _client.Execute<ContenidoPrincipal>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            return result.Data;
        }

        public ContenidoPrincipal GetContenidoPrincipalById(int id)
        {
            var request = new RestRequest("ContenidoPrincipal/GetContenido_Principal", Method.GET);
            request.AddParameter("id", id, ParameterType.QueryString);
            var result = _client.Execute<ContenidoPrincipal>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResponseService_P0);

            if (result.Data == null)
                throw new ArgumentException(result.ErrorMessage ?? SeccionResouces.NoResultsContent_P0);

            return result.Data;

        }
        public bool UploadFile(HttpPostedFileBase file, string path)
        {
            if (file.ContentLength > 0)
            {
                file.SaveAs(path);
                return true;
            }
            return false;
        }
    }
}