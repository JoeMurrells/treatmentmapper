using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treatment_Mapper
{
    public class Results
    {
        public string nomenResult { get; set; }
        public int matchResult { get; set; }
        public string codeResult { get; set; }

        public Results(string nomenResult, int matchResult, string codeResult)
        {
            this.nomenResult = nomenResult;
            this.matchResult = matchResult;
            this.codeResult = codeResult;
        }
    }
}
