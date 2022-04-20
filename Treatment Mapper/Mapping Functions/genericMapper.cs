using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Treatment_Mapper.Master_Class;

namespace Treatment_Mapper
{
    public class genericMapper
    {
        public void Mapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, int accuracy, string pRef, bool skip, bool logcheck, Logger log, int thresholdValue, string exePath, string csvName)
        {

                int count = 0;
                int p = 0;

                MasterFunctions generateMaster = new MasterFunctions();
                var masterlist = generateMaster.GenerateMasterList(masterPath);
                var valid_codes = generateMaster.CodeValidation(masterlist);

                MasterComparison master = new MasterComparison();

                CSV csvReader = new CSV();
                var outputcsv = csvReader.GenerateOutputCSV(exePath, pRef, csvName, system);

                switch (system)
                {
                case "R4":
                    {
                        var r4treatments = csvReader.ReadR4CSV(readerpath);

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

                            T.DentallyCode = master.MapFromMaster(masterlist, T.Description, T.DentallyCode, accuracy, thresholdValue, masterPath, valid_codes, exePath, logcheck, log);

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
                    break;
                case "EXACT/SOEL":
                    {
                        var exactTreatments = csvReader.ReadExactCSV(readerpath);

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

                            T.dentally_code = master.MapFromMaster(masterlist, T.exact_desc, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }
                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }

                        csvReader.WriteOutputCSV(outputcsv, exactTreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "BRIDGEIT":
                    {
                        var bridgeTreatments = csvReader.ReadBridgeITCSV(readerpath);

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

                            T.dentally_code = master.MapFromMaster(masterlist, T.treatment_id, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, exePath, logcheck, log);

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
                    break;
                case "ISMILE":
                    {
                        var iSmiletreatments = csvReader.ReadIsmileCSV(readerpath);

                        foreach (var T in iSmiletreatments)
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

                            T.dentally_code = master.MapFromMaster(masterlist, T.treatment_name, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        csvReader.WriteOutputCSV(outputcsv, iSmiletreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "SFD":
                    {
                        var sfdtreatments = csvReader.ReadSFDCSV(readerpath);

                        foreach (var T in sfdtreatments)
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

                            T.dentally_code = master.MapFromMaster(masterlist, T.nomenclature, T.dentally_code, accuracy, thresholdValue, masterPath, valid_codes, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        csvReader.WriteOutputCSV(outputcsv, sfdtreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
            }

                

                
            }
            
        }
}
