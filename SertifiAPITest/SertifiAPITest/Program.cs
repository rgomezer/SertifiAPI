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
            JSONParser parser = new JSONParser();
            parser.ReadJSONFile($"../../json/StudentData.json");
            parser.dump();
        }
    }
}
