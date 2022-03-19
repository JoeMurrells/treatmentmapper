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
        

        public void mapper(IProgress<int> reportProgress,string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip,bool logcheck, int thresholdValue) 
        {
            try 
            {
                string exePath = Application.StartupPath;
                int progress = 0;

                var masterReader = new StreamReader(masterPath);
                var masterCSV = new CsvReader(masterReader, System.Globalization.CultureInfo.InvariantCulture);
                var master = masterCSV.GetRecords<MASTER>();
                var masterlist = master.ToList();
                masterReader.Close();
                
                var reader = new StreamReader(readerpath);
                var r4csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
                var exactcsv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
               

                var r4treatments = r4csv.GetRecords<R4>();
                var exacttreatments = exactcsv.GetRecords<EXACT>();
                

                string csvName = null;

                List<int> valid_codes = new List<int>();

                foreach (var T in masterlist)
                {
                    valid_codes.Add(T.code);
                }

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
                
                var log = new StreamWriter($@"{exePath}\log.txt", append: true);
                DateTime time = DateTime.Now;
                log.WriteLine($"{time},Source CSV = {readerpath}");
                log.WriteLine($"{time},Master CSV = {masterPath}");
                log.WriteLine($"{time},Practice Ref = {pRef}");
                log.WriteLine($"{time},System = {system}");
                log.WriteLine($"{time},Accuracy = {accuracy}");
                log.WriteLine($"Description, Nomenclature, Match Value, Code Used");
                int count = 0;

                if (system == "R4") 
                {
                    var r4list = r4treatments.ToList();
                    outputcsv.WriteHeader<R4>();
                    outputcsv.NextRecord();
                    int p = 1;
                    foreach (var T in r4list)
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
                            string description = null;

                            description = T.Description.ToString();
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
                           restart: string userCode = Interaction.InputBox($"Original Description : {T.Description} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code",$"{finalResult}");
                            if (userCode == "")
                            {
                                goto restart;
                            }
                            else if(valid_codes.Contains(Convert.ToInt32(userCode)) == false)
                            {
                                goto restart;
                            }
                           int finaluserCode = Convert.ToInt32(userCode);
                           T.DentallyCode = finaluserCode;

                            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                            {
                                
                                HasHeaderRecord = false,
                            };
                            using (var stream = File.Open($"{masterPath}", FileMode.Append))
                            using (var masterUpdate = new StreamWriter(stream))
                            using (var mastercsvUpdate = new CsvWriter(masterUpdate, config))
                            {
                                mastercsvUpdate.WriteRecord(T);
                                mastercsvUpdate.NextRecord();
                            }
                        }
                        else { T.DentallyCode = finalResult; }


                        if (logcheck == true)
                        {
                            foreach (var R in results) { log.WriteLine($"{T.Description},{R.nomenResult},{R.matchResult},{R.codeResult}"); }
                        }

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
                    log.Close();
                    writer.Close();
                    MessageBox.Show($"Finished! Unable to map {count} treatments");
                }
                else if (system == "EXACT") 
                {
                    var exactlist = exacttreatments.ToList();
                    outputcsv.WriteHeader<EXACT>();
                    outputcsv.NextRecord();
                    int p = 1;
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
                            restart: string userCode = Interaction.InputBox($"Original Description : {T.exact_desc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");
                            if (userCode == "") 
                            {
                                goto restart;
                            }
                            else if (valid_codes.Contains(Convert.ToInt32(userCode)) == false)
                            {
                                goto restart;
                            }
                            int finaluserCode = Convert.ToInt32(userCode);
                            T.dentally_code = finaluserCode;

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


                        if (logcheck == true) 
                        {
                            foreach (var R in results) { log.WriteLine($"{T.exact_desc},{R.nomenResult},{R.matchResult},{R.codeResult}"); }
                        }

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
                    log.Close();
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
