using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SertifiAPITest
{
    //Business Logic done here
    public class StudentAggregateParser
    {
        //data
        private List<StudentData> students;

        //special ctor
        public StudentAggregateParser(List<StudentData> input)
        {
            Debug.Assert(input != null); //safety
            this.students = privCopyData(input);
        }

        private List<StudentData> privCopyData(List<StudentData> data)
        {
            Debug.Assert(data != null); //safety

            List<StudentData> temp = new List<StudentData>();

           foreach(var item in data)
           {
                temp.Add(item);
           }

            return temp;
        }

        public void Process()
        {
            StudentAggregate aggregregate = new StudentAggregate
            {
                YourEmail = "rgomez16@mail.depaul.edu", //hard-coded for requirements
                YourName = "Rodolfo Gomez", //hard-coded for requirements
                YearWithHighestAttendance = privFindYearWithHighestAttendance(),
                YearWithHighestOverallGpa = privFindYearWithHighestOverallGpa(),
                Top10StudentIdsWithHighestGpa = privFindTop10StudentIdsWithHighestGpa(),
                StudentIdMostInconsistent = privFindStudentIdMostInconsistent()
            };

            string jsonOutput = JsonConvert.SerializeObject(aggregregate, Formatting.Indented);

            WebFormatter.UploadJSONToURL(jsonOutput, "http://apitest.sertifi.net/api/StudentAggregate");

            dump(jsonOutput);         
        }

        public void dump(string output)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Data Query");
            Console.WriteLine("");
            Console.Write(output);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        //Helper functions
        private int privFindYearWithHighestAttendance()
        {
            var grouped = this.students.SelectMany(x => x.AnnualGrades)
                                .GroupBy(x => x.GradeYear)
                                .Select(x => new { x.Key, Count = x.Count() });

            var mostYears = grouped.Max(x => x.Count);

            var highestYears = grouped.Where(x => x.Count == mostYears).Select(x => x.Key).ToList();

            return highestYears.Min(x => x);
        }

        private int privFindYearWithHighestOverallGpa()
        {
            return students.SelectMany(g => g.AnnualGrades)
                                  .GroupBy(g => g.GradeYear)
                                  .Select(x => new { Year = x.Key, GpaAverage = x.Average(i => i.GPA) })
                                  .OrderByDescending(x => x.GpaAverage)
                                  .FirstOrDefault()
                                  .Year;
        }

        private List<int> privFindTop10StudentIdsWithHighestGpa()
        {
            return students.Select(x => new { StudentId = x.Id, GpaAverage = x.AnnualGrades.Average(y => y.GPA) })
                           .OrderByDescending(x => x.GpaAverage)
                           .Select(x => x.StudentId)
                           .Take(10)
                           .ToList();
        }

        private int privFindStudentIdMostInconsistent()
        {
            return students.Select(x => new { StudentId = x.Id, GpaDifferential = x.AnnualGrades.Max(y => y.GPA) - x.AnnualGrades.Min(z => z.GPA) })
                               .OrderByDescending(x => x.GpaDifferential)
                               .Select(x => x.StudentId)
                               .FirstOrDefault();
        }
    }
}
