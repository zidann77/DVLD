namespace DVLDProject.Licenses
{
    partial class frmShowPersonLicenseHistory
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbPersonImage = new System.Windows.Forms.PictureBox();
            this.ctrlDriverLicenses1 = new DVLDProject.Licenses.Controls.ctrlDriverLicenses();
            this.personCardWithFilter1 = new DVLDProject.PersonFolder.Controls.PersonCardWithFilter();
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(52, -7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(960, 64);
            this.lblTitle.TabIndex = 131;
            this.lblTitle.Text = "License History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbPersonImage
            // 
            this.pbPersonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbPersonImage.Image = global::DVLDProject.Properties.Resources.PersonLicenseHistory_512;
            this.pbPersonImage.InitialImage = null;
            this.pbPersonImage.Location = new System.Drawing.Point(13, 53);
            this.pbPersonImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPersonImage.Name = "pbPersonImage";
            this.pbPersonImage.Size = new System.Drawing.Size(258, 362);
            this.pbPersonImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPersonImage.TabIndex = 132;
            this.pbPersonImage.TabStop = false;
            this.pbPersonImage.Click += new System.EventHandler(this.pbPersonImage_Click);
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(12, 412);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(1084, 308);
            this.ctrlDriverLicenses1.TabIndex = 134;
            // 
            // personCardWithFilter1
            // 
            this.personCardWithFilter1.EnableFilter = true;
            this.personCardWithFilter1.Location = new System.Drawing.Point(277, 44);
            this.personCardWithFilter1.Name = "personCardWithFilter1";
            this.personCardWithFilter1.ShowAddPerson = true;
            this.personCardWithFilter1.Size = new System.Drawing.Size(777, 371);
            this.personCardWithFilter1.TabIndex = 133;
            this.personCardWithFilter1.OnPersonSelected += new System.Action<int>(this.personCardWithFilter1_OnPersonSelected);
            // 
            // frmShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 669);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.Controls.Add(this.personCardWithFilter1);
            this.Controls.Add(this.pbPersonImage);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmShowPersonLicenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowPersonLicenseHistory";
            this.Load += new System.EventHandler(this.frmShowPersonLicenseHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPersonImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPersonImage;
        private System.Windows.Forms.Label lblTitle;
        private PersonFolder.Controls.PersonCardWithFilter personCardWithFilter1;
        private Controls.ctrlDriverLicenses ctrlDriverLicenses1;
    }
}