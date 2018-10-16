using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SertifiAPITest;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SertifiAPITestTests
{
    [TestClass]
    public class WebFormatterTests
    {
        [TestMethod]
        public void WebFormatterDownloadTest()
        {
            string expected = "[{\"Id\":1,\"Name\":\"Jack\",\"StartYear\":2013,\"EndYear\":2016,\"GPARecord\":[3.4,2.1,1.2,3.6]},{\"Id\":2,\"Name\":\"Jill\",\"StartYear\":2010,\"EndYear\":2013,\"GPARecord\":[3.3,2.3,1.1,3.7]},{\"Id\":3,\"Name\":\"Bob\",\"StartYear\":2010,\"EndYear\":2012,\"GPARecord\":[2.3,2.5,2.8]},{\"Id\":4,\"Name\":\"Alice\",\"StartYear\":2013,\"EndYear\":2016,\"GPARecord\":[3.6,2.9,3.4,3.7]},{\"Id\":5,\"Name\":\"Eve\",\"StartYear\":2012,\"EndYear\":2015,\"GPARecord\":[3.3,2.5,1.1,3.7]},{\"Id\":6,\"Name\":\"Malcom\",\"StartYear\":2011,\"EndYear\":2014,\"GPARecord\":[3.8,2.7,1.1,3.7]},{\"Id\":7,\"Name\":\"Don\",\"StartYear\":2009,\"EndYear\":2012,\"GPARecord\":[3.1,2.1,1.1,3.7]},{\"Id\":8,\"Name\":\"Mike\",\"StartYear\":2009,\"EndYear\":2011,\"GPARecord\":[3.6,2.2,1.1]},{\"Id\":9,\"Name\":\"Stacy\",\"StartYear\":2015,\"EndYear\":2016,\"GPARecord\":[3.3,2.3]},{\"Id\":10,\"Name\":\"Safron\",\"StartYear\":2016,\"EndYear\":2016,\"GPARecord\":[3.3]},{\"Id\":11,\"Name\":\"Bill\",\"StartYear\":2012,\"EndYear\":2015,\"GPARecord\":[3.6,2.4,2.3,3.7]},{\"Id\":12,\"Name\":\"Quin\",\"StartYear\":2008,\"EndYear\":2012,\"GPARecord\":[3.3,2.7,1.1,3.7,2.4]},{\"Id\":13,\"Name\":\"Erin\",\"StartYear\":2008,\"EndYear\":2011,\"GPARecord\":[3.8,2.9,1.1,3.7]},{\"Id\":14,\"Name\":\"Sharon\",\"StartYear\":2013,\"EndYear\":2016,\"GPARecord\":[3.6,2.3,1.1,3.7]},{\"Id\":15,\"Name\":\"Lilly\",\"StartYear\":2011,\"EndYear\":2014,\"GPARecord\":[1.0,2.5,1.1,3.8]},{\"Id\":16,\"Name\":\"Inara\",\"StartYear\":2004,\"EndYear\":2007,\"GPARecord\":[3.3,2.8,1.1,3.7]},{\"Id\":17,\"Name\":\"Harry\",\"StartYear\":2005,\"EndYear\":2007,\"GPARecord\":[2.4,2.9,1.1]},{\"Id\":18,\"Name\":\"Emma\",\"StartYear\":2007,\"EndYear\":2011,\"GPARecord\":[3.8,2.6,1.6,3.7,2.8]},{\"Id\":19,\"Name\":\"Sharon\",\"StartYear\":2010,\"EndYear\":2013,\"GPARecord\":[3.5,2.8,1.1,3.7]},{\"Id\":20,\"Name\":\"Uday\",\"StartYear\":2002,\"EndYear\":2005,\"GPARecord\":[3.8,2.5,1.8,3.7]},{\"Id\":21,\"Name\":\"Ross\",\"StartYear\":2003,\"EndYear\":2006,\"GPARecord\":[3.3,2.7,1.5,3.7]},{\"Id\":22,\"Name\":\"Michael\",\"StartYear\":2001,\"EndYear\":2004,\"GPARecord\":[3.7,2.5,1.4,3.7]},{\"Id\":23,\"Name\":\"Nick\",\"StartYear\":2001,\"EndYear\":2004,\"GPARecord\":[3.4,2.0,1.0,3.7]},{\"Id\":24,\"Name\":\"Laura\",\"StartYear\":2010,\"EndYear\":2013,\"GPARecord\":[1.5,2.7,3.2,4.0]},{\"Id\":25,\"Name\":\"Kevin\",\"StartYear\":2000,\"EndYear\":2003,\"GPARecord\":[3.3,2.4,1.6,3.7]}]";

            string actual;
            WebFormatter.GetJSONFromURL("http://apitest.sertifi.net/api/Students", out actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebFormatterUploadTest()
        {
            string data = "{\r\n  \"YourName\": \"Rodolfo Gomez\",\r\n  \"YourEmail\": \"rgomez16@mail.depaul.edu\",\r\n  \"YearWithHighestAttendance\": 2011,\r\n  \"YearWithHighestOverallGpa\": 2016,\r\n  \"Top10StudentIdsWithHighestGpa\": [\r\n    4,\r\n    10,\r\n    11,\r\n    20,\r\n    18,\r\n    13,\r\n    24,\r\n    6,\r\n    22,\r\n    9\r\n  ],\r\n  \"StudentIdMostInconsistent\": 15\r\n}";

            string actual;
            string expected = "";
            WebFormatter.UploadJSONToURL(data, "http://apitest.sertifi.net/api/StudentAggregate", out actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WebFormatterDownloadErrorTest()
        {
            string actual;
            WebFormatter.GetJSONFromURL(null, out actual);
            WebFormatter.GetJSONFromURL("", out actual);
            WebFormatter.GetJSONFromURL(" ", out actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WebFormatterUploadErrorTest()
        {
            string response;
            string nullData = "";
            string nullData2 = " ";

            WebFormatter.UploadJSONToURL(null, null, out response);
            WebFormatter.UploadJSONToURL(null, "http://apitest.sertifi.net/api/StudentAggregate", out response);
            WebFormatter.UploadJSONToURL(nullData, "", out response);
            WebFormatter.UploadJSONToURL(nullData2, "", out response);
            WebFormatter.UploadJSONToURL(nullData, " ", out response);
            WebFormatter.UploadJSONToURL(nullData2, " ", out response);
        }

        private void privReadDataFromFile(string path, out string data)
        {
            StreamReader reader = new StreamReader(path, Encoding.UTF8);

            data = reader.ReadToEnd();
        }
    }
}
