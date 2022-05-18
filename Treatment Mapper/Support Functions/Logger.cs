using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Treatment_Mapper
{
    public class Logger
    {
        public Logger (string exePath, string readerpath, string masterPath, string pRef, string system)
        {
            using (var log = new StreamWriter($@"{exePath}\log.txt", append: true))
            {
                DateTime time = DateTime.Now;
                log.WriteLine($"{time},Source CSV = {readerpath}");
                log.WriteLine($"{time},Master CSV = {masterPath}");
                log.WriteLine($"{time},Practice Ref = {pRef}");
                log.WriteLine($"{time},System = {system}");
                log.WriteLine($"Description, Nomenclature, Match Value, Code Used");
                log.Close();
            }    
            
        }
        public void UpdateLog(bool logcheck, Object T, string finalDesc, int finalMatch, int? finalResult, string exePath) 
        {

                using (var log = new StreamWriter($@"{exePath}\log.txt", append: true))
                {
                    log.WriteLine($"{T},{finalDesc},{finalMatch},{finalResult}");
                    log.Close();
                }

        }
    }
}
