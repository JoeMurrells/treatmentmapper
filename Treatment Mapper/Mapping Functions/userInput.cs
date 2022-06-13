using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Treatment_Mapper.Master_Class;
using System.Windows.Input;

namespace Treatment_Mapper.Mapping_Functions
{
    public static class UserInput
    {
        public static DialogResult UserCodeInput(Object TDesc, string masterPath, ref object TCode, string finalResult)
        {
            using(Form2 Form2 = new Form2 {StartPosition = FormStartPosition.CenterScreen} )
            {
                CodeValidation codes = new CodeValidation();
                Dictionary<String,String> codeList = new Dictionary<String,String>();

                if(masterPath.Contains("eng_master.csv"))
                {
                    codeList = codes.eng_valid_codes;
                }
                else if(masterPath.Contains("sco_master.csv"))
                {
                    codeList = codes.sco_valid_codes;
                    Form2.scottishcheck.Checked = true;
                }
                else if (masterPath.Contains("bupa_master.csv"))
                {
                    codeList = codes.bupa_valid_codes;
                    Form2.bupacheck.Checked = true;
                }

                Form2.inputdescBox.Text = TDesc.ToString();
                Form2.matchBox.Text = codeList[finalResult];
                Form2.codeBox.Text = finalResult; 

                Form2.AcceptButton = Form2.okButton;
                Form2.okButton.DialogResult = DialogResult.Retry;

                foreach (KeyValuePair<string, string> C in codeList)
                {
                    Form2.resultsBox.AppendText($"{C.Key},{C.Value}" + Environment.NewLine);
                }

                Form2.ShowDialog();

                DialogResult result;
                if (codeList.ContainsKey(Form2.codeBox.Text) == false)
                {
                    MessageBox.Show("Invalid Code Entered");

                    result = DialogResult.Retry;

                }
                else
                {
                    result = DialogResult.OK;

                    MasterFunctions.UpdateMasterList(masterPath, TDesc, Form2.codeBox.Text);
                }
                TCode = Form2.codeBox.Text;


                return result;
            }

        }
    }
}
