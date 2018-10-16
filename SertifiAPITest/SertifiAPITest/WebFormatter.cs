using System;
using System.Collections.Generic;
using System.IO;
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

        public static void UploadJSONToURL(string data, string address, out string response)
        {
            WebFormatter pWeb = privGetInstance();
            pWeb.privUploadJSONToURL(address, data, out response);
        }

        //Helper functions for Downloading and Uploading JSON Data
        private void privGetJSONFromURL(string url, out string data)
        {
            try
            {
                data = this.wc.DownloadString(url);
            }
            catch (WebException ex)
            {
                data = null;
                string response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Download JSON Failed: {0}", response);
            }
        }

        private void privUploadJSONToURL(string address, string data, out string response)
        {
            this.wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            this.wc.Encoding = Encoding.UTF8;

            try
            {
                response = this.wc.UploadString(address, "PUT", data);
            }
            catch (WebException ex)
            {
                response = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Upload JSON Failed: {0}", response);
            }
           
        }
    }
}
