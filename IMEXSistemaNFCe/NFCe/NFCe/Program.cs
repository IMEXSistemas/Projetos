using BmsSoftware;
using BmsSoftware.Modulos.Operacional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NFCe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmLogin FrmLogin = new FrmLogin();

            if (FrmLogin.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }

        }
    }
}
