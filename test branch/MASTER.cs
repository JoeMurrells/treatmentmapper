using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;

namespace Treatment_Mapper
{
    public class MASTER
    {
        public int code { get; set; }
       public string nomenclature { get; set; }

        public List<MASTER> GenerateMasterList(string masterPath) 
        {
            var masterReader = new StreamReader(masterPath);
            var masterCSV = new CsvReader(masterReader, System.Globalization.CultureInfo.InvariantCulture);
            var master = masterCSV.GetRecords<MASTER>();
            var masterlist = master.ToList();
            masterReader.Close();
            return masterlist;
            
        }

        public void UpdateMasterList(string masterPath, Object T0, Object T1)
        {
            var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {

                HasHeaderRecord = false,
            };
            using (var stream = File.Open($"{masterPath}", FileMode.Append))
            using (var masterUpdate = new StreamWriter(stream))
            using (var mastercsvUpdate = new CsvWriter(masterUpdate, config))
            {
                /*mastercsvUpdate.WriteRecord(T);*/
                mastercsvUpdate.WriteField(T0);
                mastercsvUpdate.WriteField(T1);
                mastercsvUpdate.NextRecord();
            }
        }

        public List<int> CodeValidation (List<MASTER>masterlist)
        {
            List<int> valid_codes = new List<int>();

            foreach (var T in masterlist)
            {
                valid_codes.Add(T.code);
            }
            return valid_codes;
        }
    }
}
