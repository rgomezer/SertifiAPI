using Microsoft.VisualStudio.TestTools.UnitTesting;
using SertifiAPITest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SertifiAPITest.Tests
{
    [TestClass()]
    public class JSONParserTests
    {
        [TestMethod()]
        public void ReadJSONDataTest()
        {
            string jsonData;
            WebFormatter.GetJSONFromURL("http://apitest.sertifi.net/api/Students", out jsonData);

            ErrorCode retCode;
            
            List<StudentData> studentsActual;

            retCode = JSONParser.ReadJSONData(jsonData, out studentsActual);

            Assert.AreEqual(ErrorCode.SUCCESS, retCode);
        }

        [TestMethod()]
        public void ReadJSONDataErrorTests()
        {
            ErrorCode retCode;
            List<StudentData> students2;

            retCode = JSONParser.ReadJSONData(null, out students2);
            Assert.AreEqual(ErrorCode.FAIL, retCode);

            retCode = JSONParser.ReadJSONData("", out students2);
            Assert.AreEqual(ErrorCode.FAIL, retCode);

            retCode = JSONParser.ReadJSONData(" ", out students2);
            Assert.AreEqual(ErrorCode.FAIL, retCode);
        }
    }
}