using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Treatment_Mapper.Master_Class;

namespace Treatment_Mapper.Mapping_Functions
{
    public class userInput
    {
        public int userCodeInput(Object TDesc, string finalDesc, int finalMatch, List<int> valid_codes, string masterPath, object TCode, int? finalResult)
        {
            string userCode = Interaction.InputBox($"Original Description : {TDesc} Best match found : {finalDesc} Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");

            int convertedCode;

            while (int.TryParse(userCode, out convertedCode) == false || valid_codes.Contains(Convert.ToInt16(userCode)) == false)
            {
                MessageBox.Show("Invalid Code Entered");
                userCode = Interaction.InputBox($"Original Description : {TDesc} \n Best match found : {finalDesc} \n Match : {finalMatch}, Please confirm or enter new code.", "Confirm Code", $"{finalResult}");
            }

            
            MasterFunctions updateMaster = new MasterFunctions();
            updateMaster.UpdateMasterList(masterPath, TDesc, convertedCode);

            return convertedCode;
        }
    }
}
