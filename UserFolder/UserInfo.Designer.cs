namespace DVLDProject.UserFolder
{
    partial class UserInfo
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
            this.userCard1 = new DVLDProject.UserFolder.UserCard();
            this.SuspendLayout();
            // 
            // userCard1
            // 
            this.userCard1.Location = new System.Drawing.Point(12, 12);
            this.userCard1.Name = "userCard1";
            this.userCard1.Size = new System.Drawing.Size(766, 359);
            this.userCard1.TabIndex = 0;
            this.userCard1.Load += new System.EventHandler(this.userCard1_Load);
            // 
            // UserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 394);
            this.Controls.Add(this.userCard1);
            this.Name = "UserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserInfo";
            this.Load += new System.EventHandler(this.UserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserCard userCard1;
    }
}