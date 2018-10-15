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
        static void Run()
        {
            string jsonData;
            WebFormatter.GetJSONFromURL("http://apitest.sertifi.net/api/Students", out jsonData);

            ErrorCode retCode;
            List<StudentData> students;

            retCode = JSONParser.ReadJSONData(jsonData, out students);
            Debug.Assert(retCode == ErrorCode.SUCCESS); //safety

            JSONParser.dump(students);

            StudentAggregateParser pSAggregate = new StudentAggregateParser(students);
            pSAggregate.Process();
        }

        static void Main(string[] args)
        {
            Run();
        }
    }
}
