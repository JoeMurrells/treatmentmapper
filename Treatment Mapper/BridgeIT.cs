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
    public class BridgeIT
    {
        public string treatment_id { get; set; }
        public int count { get; set; }
        public int? dentally_code { get; set; }

        public void BridgeITMapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip, bool logcheck, int thresholdValue, string exePath, string csvName)
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
                var bridgeTreatments = csvReader.ReadBridgeITCSV(readerpath);
                var outputcsv = csvReader.GenerateOutputCSV(exePath, pRef, csvName, system);


                MasterComparison master = new MasterComparison();


                foreach (var T in bridgeTreatments)
                {
                    if (T.dentally_code >= 0 && skip == true)
                    {
                        outputcsv.WriteRecord(T);
                        outputcsv.NextRecord();
                        continue;
                    }

                    p += 1;

                    if (reportProgress != null)
                        reportProgress.Report(p);

                    T.dentally_code = master.MapFromMaster(masterlist, T.treatment_id, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, outputcsv);

                    if (T.dentally_code == null)
                    {
                        count += 1;
                    }

                    outputcsv.WriteRecord(T);
                    outputcsv.NextRecord();
                }
                csvReader.WriteOutputCSV(outputcsv, bridgeTreatments);
                MessageBox.Show($"Finished! Unable to map {count} treatments");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } }
