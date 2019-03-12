using System;
using System.IO;
using System.Linq;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Services;
using NUnit.Framework;

namespace AirTrafficMonitor.Lib.UnitTests.Services
{
    [TestFixture]
    public class LoggingServiceTests
    {
        private LoggingService uut;
        private string testString;

        [SetUp]
        public void SetUp()
        {
            uut=new LoggingService();
        }

        #region Log

        [Test]
        public void Log_StringEqualsNull_ThrowException()
        {
            testString = null;
            
            Assert.That(()=>uut.Log(testString), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Log_StringIsEmpty_FileContainsEmptyString()
        {
            testString = "";
            uut.Log(testString);

            string[] stringsInFile = File.ReadAllLines("log.txt");

            Assert.That(stringsInFile.Contains(""));
        }

        [Test]
        public void Log_StringContainsData_FileContainsSpecifiedString()
        {
            testString = "09-09-2006 05:00:00 , KLM999 , JET253";
            uut.Log(testString);

            string[] stringsInFile = File.ReadAllLines("log.txt");
            
            Assert.That(stringsInFile.Contains("09-09-2006 05:00:00 , KLM999 , JET253"));
        }

        #endregion
    }
}