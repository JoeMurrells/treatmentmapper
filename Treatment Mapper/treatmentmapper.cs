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
    public class treatmentmapper
    {
        

        public void Mapper(IProgress<int> reportProgress,string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip,bool logcheck, int thresholdValue) 
        {
            try 
            {
                string exePath = Application.StartupPath;
                int progress = 0;

                Logger log = new Logger();
                log.CreateLog(exePath, readerpath, masterPath, pRef, system, accuracy);

                MASTER bigmaster = new MASTER();
                var masterlist = bigmaster.GenerateMasterList(masterPath);

                MASTER codeValidation = new MASTER();
                var valid_codes = codeValidation.CodeValidation(masterlist);

                CSV csvReader = new CSV();

                /*var reader = new StreamReader(readerpath);
                var r4csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
                var exactcsv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);*/


                var r4treatments = csvReader.ReadR4CSV(readerpath);
                var exacttreatments = csvReader.ReadExactCSV(readerpath);

                string csvName = null;

                if (system == "R4")
                {

                    csvName = "dentally_treatments.csv";
                }
                else if (system == "EXACT")
                {

                    csvName = "treatment_map.csv";
                }

                var writer = new StreamWriter($@"{exePath}\output\{pRef}\{csvName}");
                var outputcsv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);

                
                
                int count = 0;

                if (system == "R4") 
                {;
                    outputcsv.WriteHeader<R4>();
                    outputcsv.NextRecord();
                    int p = 0;
                    foreach (var T in r4treatments)
                    {
                        if (T.DentallyCode >=0 && skip == true)
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

                        log.UpdateLog(logcheck,T.Description,finalDesc,finalMatch,finalResult,exePath);

                        if (T.DentallyCode == null)
                        {
                            count += 1;
                        }

                        outputcsv.WriteRecord(T);
                        outputcsv.NextRecord();
                        progress++;
                    }

                    outputcsv.WriteRecords(r4treatments);
                    writer.Flush();
                    writer.Close();
                    MessageBox.Show($"Finished! Unable to map {count} treatments");
                }
                else if (system == "EXACT") 
                {
                    var exactlist = exacttreatments.ToList();
                    outputcsv.WriteHeader<EXACT>();
                    outputcsv.NextRecord();
                    int p = 0;
                    foreach (var T in exactlist)
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

                       
                        var results = new ConcurrentBag<Results>();
                        Parallel.ForEach(masterlist, M =>
                       {

                           string description = T.exact_desc.ToString();
                           description = description.ToLower();

                           string nomenclature = M.nomenclature.ToString();
                           nomenclature = nomenclature.ToLower();

                           ThreadLocal<int> match = new ThreadLocal<int>();
                           match.Value = Fuzz.WeightedRatio(description, nomenclature);

                           

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
                            string userCode = Interaction.InputBox($"Original Description : {T.exact_desc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");

                            int convertedCode;

                            while (int.TryParse(userCode, out convertedCode) == false || valid_codes.Contains(Convert.ToInt32(userCode)) == false)
                            {
                                MessageBox.Show("Invalid Code Entered");
                                userCode = Interaction.InputBox($"Original Description : {T.exact_desc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");
                            }

                            T.dentally_code = convertedCode;

                            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                            {

                                HasHeaderRecord = false,
                            };
                            using (var stream = File.Open($"{masterPath}", FileMode.Append))
                            using (var masterUpdate = new StreamWriter(stream))
                            using (var mastercsvUpdate = new CsvWriter(masterUpdate, config))
                            {
                                mastercsvUpdate.WriteField(T.exact_desc);
                                mastercsvUpdate.WriteField(T.dentally_code);
                                mastercsvUpdate.NextRecord();
                            }

                        }
                        else { T.dentally_code = finalResult; }


                      /*  if (logcheck == true) 
                        {
                            foreach (var R in results) { log.WriteLine($"{T.exact_desc},{R.nomenResult},{R.matchResult},{R.codeResult}"); }
                        }*/

                        if (T.dentally_code == null)
                        {
                            count += 1;
                        }
                        
                        outputcsv.WriteRecord(T);
                        outputcsv.NextRecord();
                    }

                    outputcsv.WriteRecords(exacttreatments);
                    writer.Flush();
                    writer.Close();
                    /*log.Close();*/
                    MessageBox.Show($"Finished! Unable to map {count} treatments");

                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
