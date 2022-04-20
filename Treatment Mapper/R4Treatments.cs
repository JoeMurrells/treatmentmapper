using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using CsvHelper;
using CsvHelper.Configuration;
using FuzzySharp;
using System.IO;
using System.Collections.Concurrent;
using Microsoft.VisualBasic;

namespace Treatment_Mapper
{
    public class R4
    {
        public string Description { get; set; }
        public int? DentallyCode { get; set; }

        public void R4Mapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip, bool logcheck, Logger log, int thresholdValue, string exePath, string csvName)
        {
            try
            {
                int count = 0;
                int p = 0;

                MASTER generateMaster = new MASTER();
                var masterlist = generateMaster.GenerateMasterList(masterPath);
                var valid_codes = generateMaster.CodeValidation(masterlist);

                CSV csvReader = new CSV();
                var r4treatments = csvReader.ReadR4CSV(readerpath);
                var outputcsv = csvReader.GenerateOutputCSV(exePath, pRef, csvName, system);

                MasterComparison master = new MasterComparison();

                foreach (var T in r4treatments)
                {
                    if (T.DentallyCode >= 0 && skip == true)
                    {
                        outputcsv.WriteRecord(T);
                        outputcsv.NextRecord();
                        continue;
                    }

                    p += 1;

                    if (reportProgress != null)
                        reportProgress.Report(p);

                   T.DentallyCode = master.MapFromMaster(masterlist, T.Description, T.DentallyCode, accuracy, thresholdValue, masterPath, valid_codes,exePath,logcheck,log);

                    if (T.DentallyCode == null)
                    {
                        count += 1;
                    }

                    outputcsv.WriteRecord(T);
                    outputcsv.NextRecord();
                }
                csvReader.WriteOutputCSV(outputcsv, r4treatments);
                MessageBox.Show($"Finished! Unable to map {count} treatments");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

