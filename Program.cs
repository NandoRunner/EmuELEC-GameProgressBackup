using System;
using System.Threading;
using System.Windows.Forms;

namespace EmuELEC_GameProgressBackup
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex m = new Mutex(true, $"my{Application.ProductName}", out bool createdNew);
            m.Dispose();

            if (!createdNew)
            {
                // myApp is already running...
                MessageBox.Show(Properties.Resources.MessageBoxText01,
                                Application.ProductName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (FrmMain frm = new FrmMain())
            {
                Application.Run(frm);
            }
        }
    }
}
