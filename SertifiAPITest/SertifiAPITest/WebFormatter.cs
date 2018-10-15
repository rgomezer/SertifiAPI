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
        //data
        private static WebFormatter instance;
        private WebClient wc;

        private WebFormatter()
        {
            this.wc = new WebClient();
        }

        private static WebFormatter privGetInstance()
        {
            if(instance == null)
            {
                instance = new WebFormatter();
            }

            return instance;
        }

        public static void GetJSONFromURL(string url, out string data)
        {
            WebFormatter pWeb = privGetInstance();
            pWeb.privGetJSONFromURL(url, out data);
        }

        public static void UploadJSONToURL(string data, string address)
        {
            WebFormatter pWeb = privGetInstance();
            pWeb.privUploadJSONToURL(address, data);
        }

        private void privGetJSONFromURL(string url, out string data)
        {
            data = this.wc.DownloadString(url);
        }

        private void privUploadJSONToURL(string address, string data)
        {
            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            this.wc.Encoding = Encoding.UTF8;
            this.wc.UploadString(new Uri(address), "PUT", data);
        }
    }
}
