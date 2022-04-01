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
            int accuracy = Convert.ToInt16(numericUpDown1.Text);
            int threshold = 85; /*Convert.ToInt16(thresholdValue.Text);*/
            string masterPath;
            string exePath = Application.StartupPath;

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
            }
            else if (bupa.Checked == false && csvcheckbox.Checked == true)
            {
                masterPath = $@"{exePath}\MasterCSV\comparisonmaster.csv";
                threshold = 95;
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
                Logger log = new Logger();

                if (logCheck.Checked == true)
                {
                    log.CreateLog(exePath, openFileDialog1.FileName, masterPath, pRef, PMS, accuracy);
                }

                var reportProgress = new Progress<int>(processed =>
                {
                   label4.Visible = true;
                   label4.Text = "Treatments Processed:" + processed;
                   button1.Enabled = false;
                });

                    switch (PMS)
                    {
                        case "R4":
                            R4 r4Map = new R4();
                            await Task.Run(() =>
                            {
                                r4Map.R4Mapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, accuracy, pRef, skipcheck.Checked, logCheck.Checked,log, threshold, exePath, "dentally_treatments.csv");
                            });
                        break;
                        case "EXACT":
                            EXACT exactMap = new EXACT();
                            await Task.Run(() =>
                            {
                                exactMap.ExactMapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, accuracy, pRef, skipcheck.Checked, logCheck.Checked,log, threshold, exePath, "treatment_map.csv");
                            });
                        break;
                        case "BRIDGEIT":
                            BridgeIT bridgeMap = new BridgeIT();
                            await Task.Run(() =>
                            {
                                bridgeMap.BridgeITMapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, accuracy, pRef, skipcheck.Checked, logCheck.Checked,log, threshold, exePath, "treatment_map.csv");
                            });
                        break;
                    case "ISMILE":
                        ISMILE ismileMap = new ISMILE();
                        await Task.Run(() =>
                        {
                            ismileMap.IsmileMapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, accuracy, pRef, skipcheck.Checked, logCheck.Checked,log, threshold, exePath, "treatments.csv");
                        });
                        break;
                    case "SFD":
                        SFD sfdMap = new SFD();
                        await Task.Run(() =>
                        {
                            sfdMap.SFDMapper(reportProgress, openFileDialog1.FileName, masterPath, PMS, accuracy, pRef, skipcheck.Checked, logCheck.Checked,log, threshold, exePath, "dentally_treatments.csv");
                        });
                        break;
                }
                
                button1.Enabled = true;
             
              
            }
        
            
        }
       
    }
}
