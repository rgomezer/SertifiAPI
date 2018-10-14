using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SertifiAPITest
{
    //Another Data Transfer Object
    public class StudentAggregate
    {
        public string YourName { get; set; }
        public string YourEmail { get; set; }
        public int YearWithHighestAttendance { get; set; }
        public int YearWithHighestOverallGpa { get; set; }
        public List<int> Top10StudentIdsWithHighestGpa { get; set; }
        public int StudentIdMostInconsistent { get; set; }
    }

    //Business Logic done here
    public class StudentAggregateParser
    {
        //data
        private List<StudentData> students;

        //special ctor
        public StudentAggregateParser(List<StudentData> input)
        {
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
                YourEmail = "rgomez16@mail.depaul.edu",
                YourName = "Rodolfo Gomez",
                YearWithHighestAttendance = privFindYearWithHighestAttendance(),
                YearWithHighestOverallGpa = privFindYearWithHighestOverallGpa(),
                Top10StudentIdsWithHighestGpa = privFindTop10StudentIdsWithHighestGpa(),
                StudentIdMostInconsistent = privFindStudentIdMostInconsistent()
            };

            string jsonOutput = JsonConvert.SerializeObject(aggregregate, Formatting.Indented);

            Console.WriteLine("");
            Console.Write(jsonOutput);
        }

        //Helper functions
        private int privFindYearWithHighestAttendance()
        {
            var grouped = this.students.SelectMany(g => g.AnnualGrades)
                                .GroupBy(g => g.GradeYear)
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
