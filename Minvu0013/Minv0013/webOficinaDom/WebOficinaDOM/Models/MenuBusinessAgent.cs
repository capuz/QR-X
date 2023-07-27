using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebOficinaDOM.Models.DTO;
using RestSharp;
using Framework.Core;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using WebPortalDOM.Models.Enums;
using System.Net;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models
{
    public class MenuBusinessAgent
    {
        private readonly RestClient _client;
        public MenuBusinessAgent()
        {
            _client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
        }

        public IEnumerable<Menu> GetMenu()
        {
            var request = new RestRequest("menu/getmantenedor", Method.GET);
            var result = _client.Execute<List<Menu>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? MenuResources.NoResponseService_P0);

            if (result.Data != null)
                result.Data = result.Data.Where(x => x.IdMenuPadre != null).ToList();

            foreach (var menu in result.Data)
            {
                switch (menu.Target)
                {
                    case (int)HtmlTargets._blank:
                        menu.Destino = HtmlTargets._blank.ToString();
                        break;
                    case (int)HtmlTargets._self:
                        menu.Destino = HtmlTargets._self.ToString();
                        break;
                    default:
                        menu.Destino = MenuResources.Undefined_P0;
                        break;
                }

                menu.Dependencia = result.Data
                    .Where(x => x.IdMenu == menu.IdMenuPadre)
                    .Select(x => x.Nombre).FirstOrDefault();

            }

            return result.Data;
        }
        public IEnumerable<Menu> GetMenuPadres()
        {
            var request = new RestRequest("Menu/GetParent", Method.GET);
            var result = _client.Execute<List<Menu>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? NoticiaResources.NoResponseService_P0);

            return result.Data;
        }
        public void PostMenu(Menu menu, string usuario)
        {
            var request = new RestRequest("Menu/PostMenu", Method.POST);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(menu);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? NoticiaResources.NoResponseService_P0);

        }

        public void PutMenu(Menu menu, string usuario)
        {
            var request =
               new RestRequest("menu/PutMENU", Method.PUT);
            request.AddParameter("id", menu.IdMenu, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            request.AddJsonBody(menu);
            var result = _client.Execute(request);
            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? NoticiaResources.NoResponseService_P0);
        }

        public Menu DeleteMenuById(int id, string usuario)
        {
            var request = new RestRequest("menu/DeleteMenu", Method.DELETE);
            request.AddParameter("id", id, ParameterType.QueryString);
            request.AddParameter("usuario", usuario, ParameterType.QueryString);
            var result = _client.Execute<Menu>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? NoticiaResources.NoResponseService_P0);

            return result.Data;
        }

        public Menu GetMenuById(int id)
        {
            var request = new RestRequest("menu/GetMenu?id=" + id, Method.GET);
            var result = _client.Execute<Menu>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(result.ErrorMessage ?? NoticiaResources.NoResponseService_P0);

            var menu = result.Data;
            return menu;

        }
        public IEnumerable<Target> GetTargets()
        {
            var targets = new List<Target>();
            targets.Add(new Target()
            {
                Id = 1,
                Nombre = "_blank"
            });
            targets.Add(new Target()
            {
                Id = 2,
                Nombre = "_self"
            });
            return targets;
        }
        public IEnumerable<Menu> GetSinDependencia()
        {
            Menu item = new Menu() { IdMenu = 1, Nombre = SeccionResouces.NoDepend_P0 };
            return new List<Menu>() { item };
        }
    }
}