using System.Net;
using System.Text;
using System.IO;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace osu_AdvancedDownloader
{
    internal class Parsers
    {
        public static JObject GetJSONFromAPI(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return JObject.Parse(new StreamReader(response.GetResponseStream()).ReadToEnd());
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}
