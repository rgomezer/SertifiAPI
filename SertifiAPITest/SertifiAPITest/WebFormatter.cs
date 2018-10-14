using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SertifiAPITest
{
    public class WebFormatter
    {
        private WebClient wc;

        public WebFormatter()
        {
            this.wc = new WebClient();
        }

        public string GetJSONFromURL(string url)
        {
            string temp = "";
            temp = this.wc.DownloadString(url);

            return temp;
        }

        public void UploadJSONToURL(string data, string address)
        {
            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            string response = this.wc.UploadString(new Uri(address), "PUT", data);

            Console.WriteLine("Response: {0}",response);
        }
    }
}
