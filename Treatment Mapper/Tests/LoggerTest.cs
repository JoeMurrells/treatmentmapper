using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using FuzzySharp;
using System.IO;
using Xunit;

namespace Treatment_Mapper.Tests
{
    public class LoggerTest
    {
        [Fact]
        public void CreateLog()
        {
            //arrange

            string exePath = $@"C:\Users\Job0\source\repos\Treatment Mapper\Treatment Mapper\tests\tmp";
            string readerpath = "./tmp/input.csv";
            string masterPath = "./tmp/master.csv";
            string pRef = "Test";
            string system = "Test System";
            DateTime time = DateTime.Now;

            new Logger(exePath, readerpath, masterPath, pRef, system);

            //act
            string[] logContents = File.ReadAllLines($@"C:\Users\Job0\source\repos\Treatment Mapper\Treatment Mapper\tests\tmp\log.txt");

            //assert
            Assert.True(File.Exists($@"C:\Users\Job0\source\repos\Treatment Mapper\Treatment Mapper\tests\tmp\log.txt"));
            Assert.Contains($"{time},Source CSV = {readerpath}", logContents);
            Assert.Contains($"{time},Master CSV = {masterPath}", logContents);
            Assert.Contains($"{time},Practice Ref = {pRef}", logContents);
            Assert.Contains($"{time},System = {system}", logContents);
            Assert.Contains("Description, Nomenclature, Match Value, Code Used", logContents);


        }
        [Theory]
        [InlineData(true,"Test 0", "1234", "Final 0", 100,"9999")]
        [InlineData(false, "Test 1", "1111", "Final 1", 99, "9998")]
        public void UpdateLog_withInlineData(bool logCheck, string objDesc, string objCode, string finalDesc, int finalMatch, string finalResult)
        {
            R4 T = new R4();
            T.Description = objDesc;
            T.DentallyCode = objCode;
            string exePath = $@"C:\Users\Job0\source\repos\Treatment Mapper\Treatment Mapper\tests\tmp";
            string readerpath = "./tmp/input.csv";
            string masterPath = "./tmp/master.csv";
            string pRef = "Test";
            string system = "Test System";

            var log = new Logger(exePath, readerpath, masterPath, pRef, system);

            log.UpdateLog(logCheck, T, finalDesc, finalMatch, finalResult, exePath);

            string[] logContents = File.ReadAllLines($@"C:\Users\Job0\source\repos\Treatment Mapper\Treatment Mapper\tests\tmp\log.txt");

            if (logCheck == true)
            {
                Assert.Contains($"{T},{finalDesc},{finalMatch},{finalResult}", logContents);

            }
            else
            {
                Assert.DoesNotContain($"{T},{finalDesc},{finalMatch},{finalResult}", logContents);
            }
        }
    }
}
