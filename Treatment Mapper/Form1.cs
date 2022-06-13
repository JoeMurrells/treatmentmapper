using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using FuzzySharp;
using System.IO;
using Treatment_Mapper.Support_Functions;

namespace Treatment_Mapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string pRef = textBox1.Text;
            string PMS = listBox1.Text;
            int threshold = 85;
            string masterPath;
            string exePath = Application.StartupPath;
            string csvName;
            Logger log = null;

            FolderCreation.CreateFolders(exePath, pRef);

            

            File.Delete($@"{exePath}\log.txt");

            if (pRef == "") 
            {
                MessageBox.Show("Please enter a practice reference");
                return;
            }
            else if(PMS == "") 
            {
                MessageBox.Show("Please select a system");
                return;
            }

            if (bupa.Checked == true && scotlandcheckbox.Checked == false)
            {
                masterPath = $@"{exePath}\MasterCSV\bupa_master.csv";
                File.Copy(masterPath, $@"{exePath}\backup\bupa_master.csv", true);
                threshold = 95;
                skipcheck.Checked = false;
            }
            else if (bupa.Checked == false && scotlandcheckbox.Checked == true)
            {
                masterPath = $@"{exePath}\MasterCSV\sco_master.csv";
                File.Copy(masterPath, $@"{exePath}\backup\sco_master.csv", true);
            }
            else if (bupa.Checked == true && scotlandcheckbox.Checked == true)
            {
                MessageBox.Show("Please only tick 1 option");
                return;
            }
            else 
            {
                masterPath = $@"{exePath}\MasterCSV\eng_master.csv";
                File.Copy(masterPath, $@"{exePath}\backup\eng_master.csv", true);
            }

            if (PMS == "R4" || PMS == "SFD" || PMS == "EDGE" || PMS == "AERONA" || PMS == "BRIDGEIT")

            {
                csvName = "dentally_treatments.csv";
            }
            else if (PMS == "EXACT/SOEL")
            {
                csvName = "treatment_map.csv";
            }
            else
            {
                csvName = "treatments.csv";
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse CSV Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                

                if (logCheck.Checked == true)
                {
                     log = new Logger(exePath, openFileDialog1.FileName, masterPath, pRef, PMS);
                }

                var reportProgress = new Progress<int>(processed =>
                {
                   label4.Visible = true;
                   label4.Text = "Treatments Processed:" + processed;
                   button1.Enabled = false;
                });

                await Task.Run(() =>
                {
                    GenericMapper.Mapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, pRef, skipcheck.Checked, logCheck.Checked, log, threshold, exePath, csvName);
                });
               
                
                button1.Enabled = true;
             
              
            }
        
            
        }
       
    }
}
