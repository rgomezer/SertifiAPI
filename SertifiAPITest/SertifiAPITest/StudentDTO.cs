using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    
    //Our data transfer object
    public class StudentData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<double> GPARecord { get; set; }
        public List<AnnualStudentGrades> AnnualGrades { get { return privCalculateAnnualGrades(); } }

        //Helper function
        private List<AnnualStudentGrades> privCalculateAnnualGrades()
        {
            List<AnnualStudentGrades> temp = new List<AnnualStudentGrades>();

            int startYear = StartYear;

            foreach (double grade in GPARecord)
            {
                AnnualStudentGrades grades = new AnnualStudentGrades
                {
                    GradeYear = startYear,
                    GPA = grade
                };

                startYear++;
                temp.Add(grades);
            }

            return temp;
        }
    }

    public class AnnualStudentGrades
    {
        public int GradeYear { get; set; }
        public double GPA { get; set; }
    }
}
