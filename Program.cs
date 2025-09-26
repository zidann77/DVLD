using DVLDProject.Applications.Renew_Local_License;
using DVLDProject.Licenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLDProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmloginForm());
           // Application.Run(new frmRenewLocalDrivingLicenseApplication());

        }
    }
}
