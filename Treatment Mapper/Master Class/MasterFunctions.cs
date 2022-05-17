using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;

namespace Treatment_Mapper.Master_Class
{
    public class MasterFunctions
    {
        public List<MASTER> masterList { get; set; }  

        public MasterFunctions(string masterPath)
        {
            using (var masterReader = new StreamReader(masterPath))
            using (var masterCSV = new CsvReader(masterReader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var master = masterCSV.GetRecords<MASTER>();
                var masterlist = master.ToList();
                this.masterList = masterlist;
            }
        }
        public List<MASTER> GenerateMasterList(string masterPath)
        {
            using (var masterReader = new StreamReader(masterPath))
            using (var masterCSV = new CsvReader(masterReader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var master = masterCSV.GetRecords<MASTER>();
                var masterlist = master.ToList();
                this.masterList = masterlist;
                return masterlist;
            }
             

        }

        public static void UpdateMasterList(string masterPath, Object T0, Object T1)
        {
            var masterlist = new MasterFunctions(masterPath);

            if (masterlist.masterList.Contains(T0))
            {
                return;
            }
            else
            {
                var config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
                {

                    HasHeaderRecord = false,
                };
                using (var stream = File.Open($"{masterPath}", FileMode.Append))
                using (var masterUpdate = new StreamWriter(stream))
                using (var mastercsvUpdate = new CsvWriter(masterUpdate, config))
                {
                    mastercsvUpdate.WriteField(T0);
                    mastercsvUpdate.WriteField(T1);
                    mastercsvUpdate.NextRecord();
                }
            }

           
        }

    }
}
