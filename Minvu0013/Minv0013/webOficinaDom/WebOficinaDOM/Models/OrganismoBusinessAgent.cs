using Framework.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models
{
    public class OrganismoBusinessAgent
    {
        public IEnumerable<Organismo> GetList()
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("ORGANISMO/GetFiltro?Param1=", Method.GET);
            var result = client.Execute<List<Organismo>>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
            }
            return result.Data;
        }

        public IEnumerable<OrganismoTipo> GetOrganismoTipoList()
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("OrganismoTipo/GetOrganismo_Tipo", Method.GET);
            var result = client.Execute<List<OrganismoTipo>>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
            }
            return result.Data;
        }

        public IEnumerable<OrganismoPadre> GetOrganismoPadreList()
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("ORGANISMO/GetORGANISMO", Method.GET);
            var result = client.Execute<List<OrganismoPadre>>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
            }
            return result.Data;
        }

        public void PostOrganismo(string usuario, Organismo organismo)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("ORGANISMO/PostOrganismo?usuario="+usuario, Method.POST);
            request.AddJsonBody(organismo);
            var result =  client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public Organismo GetOrganismoById(int id)
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("ORGANISMO/GetORGANISMO?id=" + id.ToString(), Method.GET);
            var result = client.Execute<Organismo>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
 
            }
            var cont = result.Data;
            return cont;
        }

        public void PutOrganismo(int id,string usuario,Organismo organismo)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("ORGANISMO/PutORGANISMO?id=" + id.ToString()+"&usuario="+usuario, Method.PUT);
            request.AddJsonBody(organismo);
            var result = client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);

            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public void DeleteOrganismo(int id,string usuario)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("ORGANISMO/DeleteORGANISMO?id=" + id.ToString()+"&usuario="+usuario, Method.DELETE);
            var result =    client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public IEnumerable<Organismo> GetListSearch(string text)
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("ORGANISMO/GetFiltro?Param1="+text, Method.GET);
            var result = client.Execute<List<Organismo>>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(OrganismoResources.NoResponseService_P0);
            }
                return result.Data;
        }

        public String UploadFile(HttpFileCollectionBase files, string path)
        {
            string fileName = string.Empty;
            string completeFilename = string.Empty;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                

                fileName = DateTime.Now.ToString("ddMMyyyHHMMss") + file.FileName;
                completeFilename = Path.Combine(@path, fileName);
                file.SaveAs(completeFilename);

            }
            return fileName;
        }
    }
}