using System.Windows.Forms;

namespace HTM_1st_Experience
{
    public partial class HTM_Form
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
            output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // output
            // 
            output.Location = new System.Drawing.Point(5, 5);
            output.Multiline = true;
            output.Name = "output";
            output.ReadOnly = true;
            output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            output.Size = new System.Drawing.Size(1140, 590);
            output.TabIndex = 0;
            // 
            // HTM_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 600);
            this.Controls.Add(output);
            this.Name = "HTM_Form";
            this.Text = "HTM First Experience";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public static TextBox output;
    }
}

