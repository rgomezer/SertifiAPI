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
            string expected;
            privReadDataFromFile("../../StudentData.txt", out expected);
            string actual;
            WebFormatter.GetJSONFromURL("http://apitest.sertifi.net/api/Students", out actual);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WebFormatterUploadTest()
        {
            string data;
            privReadDataFromFile("../../StudentAggregate.txt", out data);
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
