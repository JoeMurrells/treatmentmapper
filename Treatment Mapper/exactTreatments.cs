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
    public class EXACT
    {
        public string soe_code { get; set; }
        public string service { get; set; }
        public string ada_code { get; set; }
        public int? occurrence { get; set; }
        public string exact_desc { get; set; }
        public int? dentally_code { get; set; }
        public string descriptions { get; set; }

        public void ExactMapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip, bool logcheck, int thresholdValue, string exePath, string csvName)
        {
            try {
                int count = 0;
                int p = 0;

                Logger log = new Logger();
                log.CreateLog(exePath, readerpath, masterPath, pRef, system, accuracy);

                MASTER generateMaster = new MASTER();
                var masterlist = generateMaster.GenerateMasterList(masterPath);
                var valid_codes = generateMaster.CodeValidation(masterlist);

                CSV csvReader = new CSV();
                var exactTreatments = csvReader.ReadExactCSV(readerpath);
                var outputcsv = csvReader.GenerateOutputCSV(exePath, pRef, csvName, system);

                MasterComparison master = new MasterComparison();

                foreach (var T in exactTreatments)
                {
                    p += 1;
                    if (reportProgress != null)
                        reportProgress.Report(p);
                    

                    if (T.dentally_code >= 0 && skip == true)
                    {
                        outputcsv.WriteRecord(T);
                        outputcsv.NextRecord();
                        continue;
                    }
                    
                    T.dentally_code = master.MapFromMaster(masterlist, T.exact_desc, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, outputcsv);
                }

                csvReader.WriteOutputCSV(outputcsv, exactTreatments);
                MessageBox.Show($"Finished! Unable to map {count} treatments");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } }
