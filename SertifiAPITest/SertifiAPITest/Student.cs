using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SertifiAPITest
{
    public enum ErrorCode
    {
        SUCCESS,
        FAIL
    }

    public class JSONParser
    {
        //data
        private static JSONParser instance;

        private JSONParser()
        {
            //does nothing
        }

        private static JSONParser privGetInstance()
        {
            if(instance == null)
            {
                instance = new JSONParser();
            }

            return instance;
        }

        //This function takes in a json string and deserializes the data into our list of students with their corresponding data
        public static ErrorCode ReadJSONData(string data, out List<StudentData> students)
        {
            if(data == null || data == "" || data == " ")
            {
                students = null;
                return ErrorCode.FAIL;
            }

            students = JsonConvert.DeserializeObject<List<StudentData>>(data); 

            return ErrorCode.SUCCESS;
        }

        //A simple print method for debugging
        public static void dump(List<StudentData> students)
        {
            System.Console.WriteLine("JSON Data Downloaded");
            System.Console.WriteLine("");

            foreach (var item in students)
            {
                System.Console.WriteLine("");

                System.Console.WriteLine("ID: {0}", item.Id);
                System.Console.WriteLine("Name: {0}", item.Name);
                System.Console.WriteLine("Start Year: {0}", item.StartYear);
                System.Console.WriteLine("End Year: {0}", item.EndYear);

                System.Console.Write("GPA Record: ");
                foreach (var gpa in item.GPARecord)
                {
                    System.Console.Write("{0}, ", gpa);
                }
                System.Console.WriteLine("");
            }
        }
    }
}
