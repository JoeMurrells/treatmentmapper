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

        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            CodeValidation codes = new CodeValidation();
            Dictionary<string,string> codeList = new Dictionary<string,string>();

                if(scottishcheck.Checked)
                {
                    codeList = codes.sco_valid_codes;
                }
                else if(bupacheck.Checked)
                {
                    codeList = codes.bupa_valid_codes;
                }
                else if(colcheck.Checked){
                    codeList = codes.col_valid_codes;
                }
                else if (sconhscheck.Checked)
                {
                    codeList = codes.sco_nhs_valid_codes;
                }
                else
                {
                    codeList = codes.eng_valid_codes;
                }
            
            
            var lines = codeList.Select(kv => kv.Key + ": " + kv.Value.ToString());
            resultsBox.Clear();

            foreach (KeyValuePair<string, string> C in codeList)
            {
                var textMatch = Fuzz.WeightedRatio(searchBox.Text.ToLower(), C.Key.ToLower());
                var codeMatch = Fuzz.WeightedRatio(searchBox.Text.ToLower(), C.Value.ToLower());


                if (textMatch > 80 || codeMatch > 80)
                {
                    resultsBox.AppendText($"{C.Key}: {C.Value}" + Environment.NewLine);
                    resultsBox.Select(0, 0);
                    resultsBox.ScrollToCaret();
                }
                else if (searchBox.Text == "")
                {
                    resultsBox.Text = string.Join(Environment.NewLine, lines);
                }
            }
        }
    }
}
