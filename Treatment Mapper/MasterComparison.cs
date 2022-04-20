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
    public class MasterComparison
    {
        public int? MapFromMaster(List<MASTER> masterlist, Object TDesc, Object TCode, int accuracy, int thresholdValue, string masterPath, List<int> valid_codes,string exePath,bool logcheck,Logger log)
        {

            var results = new ConcurrentBag<Results>();

            Parallel.ForEach(masterlist, M =>
            {

                string description = TDesc.ToString();
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
                string userCode = Interaction.InputBox($"Original Description : {TDesc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");

                int convertedCode;

                while (int.TryParse(userCode, out convertedCode) == false || valid_codes.Contains(Convert.ToInt16(userCode)) == false)
                {
                    MessageBox.Show("Invalid Code Entered");
                    userCode = Interaction.InputBox($"Original Description : {TDesc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");
                }

                TCode = convertedCode;
                MASTER updateMaster = new MASTER();
                updateMaster.UpdateMasterList(masterPath, TDesc, TCode);
            }
            else { TCode = finalResult; }

            log.UpdateLog(logcheck, TDesc, finalDesc, finalMatch, finalResult, exePath);

            int? outputCode = Convert.ToInt16(TCode);
            return outputCode;
        }
    }
}
