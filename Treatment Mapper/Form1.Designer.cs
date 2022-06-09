
namespace Treatment_Mapper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bupa = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.scotlandcheckbox = new System.Windows.Forms.CheckBox();
            this.skipcheck = new System.Windows.Forms.CheckBox();
            this.logCheck = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(194, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "Import Treatment Map CSV";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select PMS";
            // 
            // bupa
            // 
            this.bupa.AutoSize = true;
            this.bupa.Location = new System.Drawing.Point(6, 91);
            this.bupa.Name = "bupa";
            this.bupa.Size = new System.Drawing.Size(55, 17);
            this.bupa.TabIndex = 4;
            this.bupa.Text = "BUPA";
            this.bupa.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(5, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(190, 20);
            this.textBox1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Practice Reference";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // scotlandcheckbox
            // 
            this.scotlandcheckbox.AutoSize = true;
            this.scotlandcheckbox.Location = new System.Drawing.Point(67, 91);
            this.scotlandcheckbox.Name = "scotlandcheckbox";
            this.scotlandcheckbox.Size = new System.Drawing.Size(64, 17);
            this.scotlandcheckbox.TabIndex = 10;
            this.scotlandcheckbox.Text = "Scottish";
            this.scotlandcheckbox.UseVisualStyleBackColor = true;
            // 
            // skipcheck
            // 
            this.skipcheck.AutoSize = true;
            this.skipcheck.Checked = true;
            this.skipcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.skipcheck.Location = new System.Drawing.Point(6, 114);
            this.skipcheck.Name = "skipcheck";
            this.skipcheck.Size = new System.Drawing.Size(145, 17);
            this.skipcheck.TabIndex = 11;
            this.skipcheck.Text = "Skip Mapped Treatments";
            this.skipcheck.UseVisualStyleBackColor = true;
            // 
            // logCheck
            // 
            this.logCheck.AutoSize = true;
            this.logCheck.Location = new System.Drawing.Point(5, 137);
            this.logCheck.Name = "logCheck";
            this.logCheck.Size = new System.Drawing.Size(80, 17);
            this.logCheck.TabIndex = 12;
            this.logCheck.Text = "Enable Log";
            this.logCheck.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "R4",
            "EXACT/SOEL",
            "BRIDGEIT",
            "ISMILE",
            "SFD",
            "EDGE",
            "AERONA"});
            this.listBox1.Location = new System.Drawing.Point(6, 64);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(189, 21);
            this.listBox1.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 224);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.logCheck);
            this.Controls.Add(this.skipcheck);
            this.Controls.Add(this.scotlandcheckbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.bupa);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Treatment Mapper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox bupa;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox scotlandcheckbox;
        private System.Windows.Forms.CheckBox skipcheck;
        private System.Windows.Forms.CheckBox logCheck;
        private System.Windows.Forms.ComboBox listBox1;
    }
}

