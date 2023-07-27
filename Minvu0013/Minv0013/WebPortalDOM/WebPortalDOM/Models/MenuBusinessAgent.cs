using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPortalDOM.Models.DTO;
using WebPortalDOM.Models.Enums;
using RestSharp;
using Framework.Core;
using System.Net;
using WebPortalDOM.Resources;
namespace WebPortalDOM.Models
{
    public class MenuBusinessAgent
    {

        public IEnumerable<MenuLayout> GetMenuLayout()
        {
            var client = new RestClient(ConfigWrapper.Value<string>("BaseUrlWebApi"));
            var request = new RestRequest("Menu/GetHomePage", Method.GET);

            var result = client.Execute<List<MenuLayout>>(request);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException(ErrorMessageResources.NoResponseService);

            if (result.Data != null)
            {
                result.Data = result.Data.ToList();

                foreach (var menu in result.Data)
                {
                    switch (menu.Target)
                    {
                        case (int)HtmlTargets._blank:
                            menu.TargetString = HtmlTargets._blank.ToString();
                            break;
                        case (int)HtmlTargets._self:
                            menu.TargetString = HtmlTargets._self.ToString();
                            break;
                        default:
                            menu.TargetString = HtmlTargets._self.ToString();
                            break;
                    }
                }
            }
            return result.Data;
        }

    }
}