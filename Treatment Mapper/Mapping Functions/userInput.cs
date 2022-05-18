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
        public static DialogResult UserCodeInput(Object TDesc, string finalDesc, int finalMatch, List<int> valid_codes, string masterPath, ref object TCode, int? finalResult)
        {
            using(Form2 Form2 = new Form2 {StartPosition = FormStartPosition.CenterScreen} )
            {
                Form2.inputdescBox.Text = TDesc.ToString();
                Form2.matchBox.Text = finalDesc;
                Form2.codeBox.Text = finalResult.ToString(); ;

                Form2.AcceptButton = Form2.okButton;
                Form2.okButton.DialogResult = DialogResult.Retry;

                Form2.ShowDialog();

                DialogResult result;
                if (int.TryParse(Form2.codeBox.Text, out int convertedCode) == false || valid_codes.Contains(Convert.ToInt16(Form2.codeBox.Text)) == false)
                {
                    MessageBox.Show("Invalid Code Entered");

                    result = DialogResult.Retry;

                }
                else
                {
                    result = DialogResult.OK;

                    MasterFunctions.UpdateMasterList(masterPath, TDesc, convertedCode);
                }
                TCode = convertedCode;


                return result;
            }

        }
    }
}
