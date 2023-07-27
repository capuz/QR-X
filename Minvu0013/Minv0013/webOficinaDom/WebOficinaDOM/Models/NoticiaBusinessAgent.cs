using Framework.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebOficinaDOM.Models.DTO;
using WebOficinaDOM.Resources;

namespace WebOficinaDOM.Models
{
    public class NoticiaBusinessAgent
    {
        public IEnumerable<Noticia> GetList()
        {
             var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
             var request = new RestRequest("NOTICIA/getNoticia", Method.GET);
             var result = client.Execute<List<Noticia>>(request);
             if (result.IsNull() || result.Data.IsNull())
             {
                 throw new ArgumentException(NoticiaResources.NoResponseService_P0);
             }
             
             return result.Data;
        }

        public void PostNoticia(Noticia noticia, string usuario)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("Noticia/PostNoticia?usuario="+usuario, Method.POST);
            request.AddJsonBody(noticia);
            var result = client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public Noticia GetNoticiaById(int id)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("NOTICIA/GetNOTICIA?id=" + id.ToString(), Method.GET);
            var result = client.Execute<Noticia>(request);
            if (result.IsNull() || result.Data.IsNull())
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            var cont = result.Data;
            return cont;
        }

        public void PutNoticia(int id, Noticia noticia,string usuario)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("NOTICIA/PutNOTICIA?id=" + id.ToString()+"&usuario="+usuario, Method.PUT);
            request.AddJsonBody(noticia);
            var result = client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public void PublishNoticia(int id,string usuario)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("NOTICIA/PatchNoticia_Publicacion?id=" + id.ToString() + "&usuario=" + usuario, Method.PATCH);
            var result = client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
        }

        public void DeleteNoticia(int id, string usuario)
        {

            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request =
               new RestRequest("NOTICIA/DeleteNOTICIA?id=" + id.ToString() + "&usuario=" + usuario, Method.DELETE);
            var result = client.Execute(request);
            if (result.StatusCode == 0)
            {
                throw new ArgumentException(NoticiaResources.NoResponseService_P0);
            }
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ArgumentException(result.StatusDescription);
            }
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