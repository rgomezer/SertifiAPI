using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SertifiAPITest
{
    public class StudentDataInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<double> GPARecord { get; set; }
    }

    public enum FileErrorCode
    {
        SUCCESS,
        FAIL
    }

    public class JSONParser
    {
        List<StudentDataInput> students;

        public FileErrorCode ReadJSONFile(string fileName)
        {
            if(fileName == null || fileName == "")
            {
                return FileErrorCode.FAIL;
            }

            students = JsonConvert.DeserializeObject<List<StudentDataInput>>(File.ReadAllText(fileName));

            if(students == null)
            {
                return FileErrorCode.FAIL;
            }

            return FileErrorCode.SUCCESS;
        }

        public void dump()
        {
            foreach (var item in students)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("");

                System.Console.WriteLine("ID: {0}", item.Id);
                System.Console.WriteLine("Name: {0}", item.Name);
                System.Console.WriteLine("Start Year: {0}", item.StartYear);
                System.Console.WriteLine("End Year: {0}", item.EndYear);

                foreach (var gpa in item.GPARecord)
                {
                    System.Console.WriteLine("GPA Record: {0}", gpa);
                }
            }
        }
    }
}
