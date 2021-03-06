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
using Treatment_Mapper.Mapping_Functions;

namespace Treatment_Mapper
{
    public static class MasterComparison
    {
        public static string MapFromMaster(List<MASTER> masterlist, Object TDesc, Object TCode, int thresholdValue, string masterPath,string exePath,bool logcheck,Logger log)
        {

            var results = new ConcurrentBag<Results>();

            Parallel.ForEach(masterlist, M =>
            {

                string description = TDesc.ToString().ToLower();

                string nomenclature = M.nomenclature.ToString().ToLower();


                ThreadLocal<int> match = new ThreadLocal<int>
                {
                    Value = Fuzz.WeightedRatio(description, nomenclature)
                };

                
                results.Add(new Results(nomenclature, match.Value, M.code));
                
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

            if (finalMatch < thresholdValue || finalResult == "")
            {
                var inputResult = UserInput.UserCodeInput(TDesc, masterPath, ref TCode, finalResult);
                while (inputResult == DialogResult.Retry)
                {
                    inputResult = UserInput.UserCodeInput(TDesc, masterPath, ref TCode, finalResult);
                }
            }
            else { TCode = finalResult; }

            if (logcheck == true)
            {
                log.UpdateLog(logcheck, TDesc, finalDesc, finalMatch, finalResult, exePath);
            }
            

            return TCode.ToString();
            
        }
    }
}
