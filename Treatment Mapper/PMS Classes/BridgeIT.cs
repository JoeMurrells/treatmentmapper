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
    public class BridgeIT
    {
        public string treatment_id { get; set; }
        public int count { get; set; }
        public int? dentally_code { get; set; }

        
    } 
}
