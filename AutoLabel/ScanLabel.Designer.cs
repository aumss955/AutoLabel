namespace AutoLabel
{
    partial class ScanLabel
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
            this.btnOK = new System.Windows.Forms.Button();
            this.tbInputSN = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 61);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbInputSN
            // 
            this.tbInputSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tbInputSN.Location = new System.Drawing.Point(12, 29);
            this.tbInputSN.Name = "tbInputSN";
            this.tbInputSN.Size = new System.Drawing.Size(296, 26);
            this.tbInputSN.TabIndex = 1;
            this.tbInputSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInputSN_KeyDown);
            this.tbInputSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInputSN_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please input S/N for checking";
            // 
            // ScanLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 94);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbInputSN);
            this.Controls.Add(this.btnOK);
            this.Name = "ScanLabel";
            this.Text = "ScanLabel";
            this.Load += new System.EventHandler(this.ScanLabel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbInputSN;
        private System.Windows.Forms.Label label1;
    }
}