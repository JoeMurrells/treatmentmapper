using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using Treatment_Mapper.PMS_Classes;

namespace Treatment_Mapper
{
    public static class CSV
    {
       public static CsvWriter GenerateOutputCSV (string exePath, string pRef, string csvName, string system)
        {
            var writer = new StreamWriter($@"{exePath}\output\{pRef}\{csvName}");
            var outputcsv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture);
            
            switch (system)
            {
                case "R4": outputcsv.WriteHeader<R4>();
                    break;
                case "EXACT/SOEL": outputcsv.WriteHeader<EXACT>();
                    break;
                case "EDGE":
                case "BRIDGEIT": outputcsv.WriteHeader<BridgeIT>();
                    break;
                case "ISMILE":
                    outputcsv.WriteHeader<ISMILE>();
                    break;
                case "SFD":
                    outputcsv.WriteHeader<SFD>();
                    break;
                case "AERONA":
                    outputcsv.WriteHeader<Aerona>();
                    break;
            }
            outputcsv.NextRecord();
            return outputcsv;
        }

        public static void WriteOutputCSV(CsvWriter outputcsv, IEnumerable<Object> treatments)
        {
            outputcsv.WriteRecords(treatments);
            outputcsv.Flush();
            outputcsv.Dispose();
        }

        public static IEnumerable<R4> ReadR4CSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var R4 = csv.GetRecords<R4>();
            return R4;
        }
        public static IEnumerable<EXACT> ReadExactCSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var exact = csv.GetRecords<EXACT>();
            return exact;
        }
        public static IEnumerable<BridgeIT> ReadBridgeITCSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var bridgeIT = csv.GetRecords<BridgeIT>();
            return bridgeIT;
        }
        public static IEnumerable<ISMILE> ReadIsmileCSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var iSmile = csv.GetRecords<ISMILE>();
            return iSmile;
        }
        public static IEnumerable<SFD> ReadSFDCSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var SFD = csv.GetRecords<SFD>();
            return SFD;
        }
        public static IEnumerable<Aerona> ReadAeronaCSV(string readerpath)
        {
            var reader = new StreamReader(readerpath);
            var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);
            var Aerona = csv.GetRecords<Aerona>();
            return Aerona;
        }
    }
}
