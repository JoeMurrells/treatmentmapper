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
    public class EXACT
    {
        public string soe_code { get; set; }
        public string service { get; set; }
        public string ada_code { get; set; }
        public int? occurrence { get; set; }
        public string exact_desc { get; set; }
        public string dentally_code { get; set; }
        public string descriptions { get; set; }


    }
}