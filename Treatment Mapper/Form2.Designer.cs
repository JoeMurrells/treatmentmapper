namespace Treatment_Mapper
{
    partial class Form2
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
            this.resultsBox = new System.Windows.Forms.TextBox();
            this.inputdescBox = new System.Windows.Forms.TextBox();
            this.matchBox = new System.Windows.Forms.TextBox();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.codeBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.scottishcheck = new System.Windows.Forms.CheckBox();
            this.bupacheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // resultsBox
            // 
            this.resultsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultsBox.Location = new System.Drawing.Point(12, 248);
            this.resultsBox.Multiline = true;
            this.resultsBox.Name = "resultsBox";
            this.resultsBox.ReadOnly = true;
            this.resultsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultsBox.Size = new System.Drawing.Size(1018, 321);
            this.resultsBox.TabIndex = 0;
            this.resultsBox.WordWrap = false;
            // 
            // inputdescBox
            // 
            this.inputdescBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputdescBox.Location = new System.Drawing.Point(12, 30);
            this.inputdescBox.Name = "inputdescBox";
            this.inputdescBox.Size = new System.Drawing.Size(776, 26);
            this.inputdescBox.TabIndex = 1;
            // 
            // matchBox
            // 
            this.matchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchBox.Location = new System.Drawing.Point(12, 75);
            this.matchBox.Name = "matchBox";
            this.matchBox.Size = new System.Drawing.Size(776, 26);
            this.matchBox.TabIndex = 2;
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(12, 200);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(269, 20);
            this.searchBox.TabIndex = 3;
            this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Results;";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Input Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Match Description";
            // 
            // codeBox
            // 
            this.codeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeBox.Location = new System.Drawing.Point(12, 120);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(269, 26);
            this.codeBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Code";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 152);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(269, 29);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // scottishcheck
            // 
            this.scottishcheck.AutoSize = true;
            this.scottishcheck.Location = new System.Drawing.Point(898, 208);
            this.scottishcheck.Name = "scottishcheck";
            this.scottishcheck.Size = new System.Drawing.Size(92, 17);
            this.scottishcheck.TabIndex = 11;
            this.scottishcheck.Text = "scottishcheck";
            this.scottishcheck.UseVisualStyleBackColor = true;
            this.scottishcheck.Visible = false;
            // 
            // bupacheck
            // 
            this.bupacheck.AutoSize = true;
            this.bupacheck.Location = new System.Drawing.Point(898, 228);
            this.bupacheck.Name = "bupacheck";
            this.bupacheck.Size = new System.Drawing.Size(80, 17);
            this.bupacheck.TabIndex = 12;
            this.bupacheck.Text = "bupacheck";
            this.bupacheck.UseVisualStyleBackColor = true;
            this.bupacheck.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 581);
            this.Controls.Add(this.bupacheck);
            this.Controls.Add(this.scottishcheck);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.codeBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.matchBox);
            this.Controls.Add(this.inputdescBox);
            this.Controls.Add(this.resultsBox);
            this.Name = "Form2";
            this.Text = "Confirm Code";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox codeBox;
        public System.Windows.Forms.TextBox inputdescBox;
        public System.Windows.Forms.TextBox matchBox;
        public System.Windows.Forms.Button okButton;
        public System.Windows.Forms.TextBox resultsBox;
        public System.Windows.Forms.CheckBox scottishcheck;
        public System.Windows.Forms.CheckBox bupacheck;
    }
}