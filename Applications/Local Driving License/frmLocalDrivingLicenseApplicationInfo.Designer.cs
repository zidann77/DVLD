namespace DVLDProject.Applications.Local_Driving_License
{
    partial class frmLocalDrivingLicenseApplicationInfo
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
            this.ctrlDrivingLicneseApplicationInfo1 = new DVLDProject.Applications.Local_Driving_License.ctrlDrivingLicneseApplicationInfo();
            this.SuspendLayout();
            // 
            // ctrlDrivingLicneseApplicationInfo1
            // 
            this.ctrlDrivingLicneseApplicationInfo1.Location = new System.Drawing.Point(12, 3);
            this.ctrlDrivingLicneseApplicationInfo1.Name = "ctrlDrivingLicneseApplicationInfo1";
            this.ctrlDrivingLicneseApplicationInfo1.Size = new System.Drawing.Size(892, 363);
            this.ctrlDrivingLicneseApplicationInfo1.TabIndex = 0;
            this.ctrlDrivingLicneseApplicationInfo1.Load += new System.EventHandler(this.ctrlDrivingLicneseApplicationInfo1_Load);
            // 
            // frmLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 373);
            this.Controls.Add(this.ctrlDrivingLicneseApplicationInfo1);
            this.Name = "frmLocalDrivingLicenseApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLocalDrivingLicenseApplicationInfo";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDrivingLicneseApplicationInfo ctrlDrivingLicneseApplicationInfo1;
    }
}