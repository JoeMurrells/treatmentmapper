using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Treatment_Mapper.Mapping_Functions;
using Treatment_Mapper.Master_Class;

namespace Treatment_Mapper
{
    public static class GenericMapper
    {
        public static void Mapper(IProgress<int> reportProgress, string readerpath, string masterPath, string system, string pRef, bool skip, bool logcheck, Logger log, int thresholdValue, string exePath, string csvName)
        {

                int count = 0;
                int p = 0;

                var masterlist = new MasterFunctions(masterPath);
                

                var outputcsv = CSV.GenerateOutputCSV(exePath, pRef, csvName, system);

                switch (system)
                {
                case "R4":
                    {
                        var r4treatments = CSV.ReadR4CSV(readerpath);

                        foreach (var T in r4treatments)
                        {
                            if (T.DentallyCode != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            p += 1;

                            if (reportProgress != null)
                                reportProgress.Report(p);

                            T.DentallyCode = MasterComparison.MapFromMaster(masterlist.masterList, T.Description, T.DentallyCode, thresholdValue, masterPath, exePath, logcheck, log);

                            if (T.DentallyCode == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        CSV.WriteOutputCSV(outputcsv, r4treatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "EXACT/SOEL":
                    {
                        var exactTreatments = CSV.ReadExactCSV(readerpath);

                        foreach (var T in exactTreatments)
                        {
                            p += 1;
                            if (reportProgress != null)
                                reportProgress.Report(p);

                            if (T.dentally_code != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            T.dentally_code = MasterComparison.MapFromMaster(masterlist.masterList, T.exact_desc, T.dentally_code, thresholdValue, masterPath,  exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }
                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }

                        CSV.WriteOutputCSV(outputcsv, exactTreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "EDGE":
                case "BRIDGEIT":
                    {
                        var bridgeTreatments = CSV.ReadBridgeITCSV(readerpath);

                        foreach (var T in bridgeTreatments)
                        {
                            if (T.dentally_code != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            p += 1;

                            if (reportProgress != null)
                                reportProgress.Report(p);

                            T.dentally_code = MasterComparison.MapFromMaster(masterlist.masterList, T.treatment_id, T.dentally_code, thresholdValue, masterPath, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        CSV.WriteOutputCSV(outputcsv, bridgeTreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "ISMILE":
                    {
                        var iSmiletreatments = CSV.ReadIsmileCSV(readerpath);

                        foreach (var T in iSmiletreatments)
                        {
                            if (T.dentally_code != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            p += 1;

                            if (reportProgress != null)
                                reportProgress.Report(p);

                            T.dentally_code = MasterComparison.MapFromMaster(masterlist.masterList, T.nomenclature, T.dentally_code, thresholdValue, masterPath,  exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        CSV.WriteOutputCSV(outputcsv, iSmiletreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "SFD":
                    {
                        var sfdtreatments = CSV.ReadSFDCSV(readerpath);

                        foreach (var T in sfdtreatments)
                        {
                            if (T.dentally_code != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            p += 1;

                            if (reportProgress != null)
                                reportProgress.Report(p);

                            T.dentally_code = MasterComparison.MapFromMaster(masterlist.masterList, T.nomenclature, T.dentally_code, thresholdValue, masterPath, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        CSV.WriteOutputCSV(outputcsv, sfdtreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
                case "AERONA":
                    {
                        var aeronaTreatments = CSV.ReadAeronaCSV(readerpath);

                        foreach (var T in aeronaTreatments)
                        {
                            if (T.dentally_code != "" && skip == true)
                            {
                                outputcsv.WriteRecord(T);
                                outputcsv.NextRecord();
                                continue;
                            }

                            p += 1;

                            if (reportProgress != null)
                                reportProgress.Report(p);

                            T.dentally_code = MasterComparison.MapFromMaster(masterlist.masterList, T.aerona_description, T.dentally_code, thresholdValue, masterPath, exePath, logcheck, log);

                            if (T.dentally_code == null)
                            {
                                count += 1;
                            }

                            outputcsv.WriteRecord(T);
                            outputcsv.NextRecord();
                        }
                        CSV.WriteOutputCSV(outputcsv, aeronaTreatments);
                        MessageBox.Show($"Finished! Unable to map {count} treatments");
                    }
                    break;
            }

                

                
            }
            
        }
}
