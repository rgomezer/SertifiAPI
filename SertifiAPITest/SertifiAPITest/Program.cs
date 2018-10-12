using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            ErrorCode retCode;
            List<StudentData> students;

            retCode = JSONParser.ReadJSONData(jsonData, out students);
            Debug.Assert(retCode == ErrorCode.SUCCESS); //safety

            JSONParser.dump(students);
        }
    }
}
