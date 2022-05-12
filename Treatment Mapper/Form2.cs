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

            var lines = codes.Codes.Select(kv => kv.Key + ": " + kv.Value.ToString());
            resultsBox.Clear();

            foreach (KeyValuePair<string, int> C in codes.Codes)
            {
                var textMatch = Fuzz.WeightedRatio(searchBox.Text.ToLower(), C.Key.ToLower());
                var codeMatch = Fuzz.WeightedRatio(searchBox.Text.ToLower(), C.Value.ToString());


                if (textMatch > 80 || codeMatch == 100)
                {
                    resultsBox.AppendText($"{C.Key}: {C.Value}" + Environment.NewLine);
                }
                else if (searchBox.Text == "")
                {
                    resultsBox.Text = string.Join(Environment.NewLine, lines);
                }
            }
        }
    }
}
