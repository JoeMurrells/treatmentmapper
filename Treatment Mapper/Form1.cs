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

            if (!Directory.Exists($@"{exePath}\MasterCSV"))
            {
                Directory.CreateDirectory($@"{exePath}\MasterCSV");
            }

            if (!Directory.Exists($@"{exePath}\output"))
            {
                Directory.CreateDirectory($@"{exePath}\output");
            }

            if (!Directory.Exists($@"{exePath}\backup"))
            {
                Directory.CreateDirectory($@"{exePath}\backup");
            }

            Directory.CreateDirectory($@"{exePath}\output\{pRef}");

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

            if (bupa.Checked == true && csvcheckbox.Checked == false)
            {
                masterPath = $@"{exePath}\MasterCSV\bupa_master.csv";
                File.Copy(masterPath, $@"{exePath}\backup\bupa_master.csv", true);
                threshold = 95;
            }
            else if (bupa.Checked == false && csvcheckbox.Checked == true)
            {
                masterPath = $@"{exePath}\MasterCSV\comparisonmaster.csv";
                threshold = 100;
            }
            else if (bupa.Checked == true && csvcheckbox.Checked == true)
            {
                MessageBox.Show("Please only tick 1 option");
                return;
            }
            else 
            {
                masterPath = $@"{exePath}\MasterCSV\master.csv";
                File.Copy(masterPath, $@"{exePath}\backup\master.csv", true);
            }

            if (PMS == "R4" || PMS == "SFD")

            {
                csvName = "dentally_treatments.csv";
            }
            else if (PMS == "EXACT/SOEL" || PMS == "BRIDGEIT")
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
                    genericMapper.Mapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, pRef, skipcheck.Checked, logCheck.Checked, log, threshold, exePath, csvName);
                });
               
                
                button1.Enabled = true;
             
              
            }
        
            
        }
       
    }
}
