using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SertifiAPITest
{
    class Program
    {
        static void Main(string[] args)
        {
            WebFormatter wf = new WebFormatter();

            string jsonData = wf.GetJSONFromURL("http://apitest.sertifi.net/api/Students");

            JSONParser parser = new JSONParser();
            //parser.ReadJSONFile($"../../json/StudentData.json");
            parser.ReadJSONData(jsonData);

            parser.dump();
        }
    }
}
