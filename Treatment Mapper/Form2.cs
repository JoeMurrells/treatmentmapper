using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Treatment_Mapper.Mapping_Functions;
using FuzzySharp;

namespace Treatment_Mapper
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            CodeValidation codes = new CodeValidation();

            foreach (KeyValuePair<string, int> C in codes.Codes)
            {
                resultsBox.AppendText($"{C.Key},{C.Value}" + Environment.NewLine);
            }
            
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            CodeValidation codes = new CodeValidation();

            resultsBox.Text = "";

            foreach (KeyValuePair<string, int> C in codes.Codes)
            {
                var match = Fuzz.WeightedRatio(searchBox.Text.ToLower(), C.Key.ToLower());

                if (searchBox.Text == "")
                    match = 100;

                if (match > 80)
                {
                    resultsBox.AppendText($"{C.Key},{C.Value}" + Environment.NewLine);

                }
            }
            resultsBox.Select(0, 0);
            resultsBox.ScrollToCaret();
        }
    }
}
