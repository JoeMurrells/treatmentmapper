﻿using System;
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

        public void R4Mapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip, bool logcheck, int thresholdValue, string exePath, string csvName)
        {
            try
            {
                int count = 0;
                int p = 0;

                Logger log = new Logger();
                log.CreateLog(exePath, readerpath, masterPath, pRef, system, accuracy);

                MASTER generateMaster = new MASTER();
                var masterlist = generateMaster.GenerateMasterList(masterPath);

                MASTER codeValidation = new MASTER();
                var valid_codes = codeValidation.CodeValidation(masterlist);

                CSV csvReader = new CSV();
                var r4treatments = csvReader.ReadR4CSV(readerpath);

                CSV generateOutput = new CSV();
                var outputcsv = generateOutput.GenerateOutputCSV(exePath, pRef, csvName, system);

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


                    var results = new ConcurrentBag<Results>();

                    Parallel.ForEach(masterlist, M =>
                    {

                        string description = T.Description.ToString();
                        description = description.ToLower();

                        string nomenclature = M.nomenclature.ToString();
                        nomenclature = nomenclature.ToLower();


                        ThreadLocal<int> match = new ThreadLocal<int>
                        {
                            Value = Fuzz.WeightedRatio(description, nomenclature)
                        };



                        if (match.Value >= accuracy)
                        {
                            results.Add(new Results(nomenclature, match.Value, M.code));

                        }
                        match.Dispose();

                    });
                    var finalResult = (from r in results
                                       orderby r.matchResult descending
                                       select r.codeResult).FirstOrDefault();
                    var finalMatch = (from r in results
                                      orderby r.matchResult descending
                                      select r.matchResult).FirstOrDefault();
                    var finalDesc = (from r in results
                                     orderby r.matchResult descending
                                     select r.nomenResult).FirstOrDefault();

                    if (finalMatch <= thresholdValue || finalResult <= 0)
                    {
                        string userCode = Interaction.InputBox($"Original Description : {T.Description} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");

                        int convertedCode;

                        while (int.TryParse(userCode, out convertedCode) == false || valid_codes.Contains(Convert.ToInt32(userCode)) == false)
                        {
                            MessageBox.Show("Invalid Code Entered");
                            userCode = Interaction.InputBox($"Original Description : {T.Description} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");
                        }

                        T.DentallyCode = convertedCode;
                        MASTER updateMaster = new MASTER();
                        updateMaster.UpdateMasterList(masterPath, T.Description, T.DentallyCode);
                    }
                    else { T.DentallyCode = finalResult; }

                    log.UpdateLog(logcheck, T.Description, finalDesc, finalMatch, finalResult, exePath);

                    if (T.DentallyCode == null)
                    {
                        count += 1;
                    }

                    outputcsv.WriteRecord(T);
                    outputcsv.NextRecord();
                }
                generateOutput.WriteOutputCSV(outputcsv, r4treatments);
                MessageBox.Show($"Finished! Unable to map {count} treatments");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

