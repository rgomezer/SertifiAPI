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
        public string GetJSONFromURL(string url)
        {
            string temp = "";

            WebClient wc = new WebClient();

            temp = wc.DownloadString(url);

            return temp;
        }

    }
}
