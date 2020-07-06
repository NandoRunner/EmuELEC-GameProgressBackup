using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using EmuELEC_GameProgressBackup.Model;
using FAndradeTI.Util.Network;
using FAndradeTI.Util.FileSystem;

namespace EmuELEC_GameProgressBackup
{
    public partial class FrmMain : Form
    {
        public readonly string BASE_EMUELEC;

        private bool isProcessing;
        private bool isLoaded;

        public FrmMain()
        {
            InitializeComponent();

            BASE_EMUELEC = "\\emuelec";

            isProcessing = false;
            isLoaded = false;

            btnGetInputFolder.Tag = "Input";
            btnGetInputFolder.Click += EventDisableFields;
            btnGetInputFolder.Click += EventGetFolder;
            btnGetInputFolder.Click += EventEnableFields;

            btnGetOutputFolder.Tag = "Output";
            btnGetOutputFolder.Click += EventDisableFields;
            btnGetOutputFolder.Click += EventGetFolder;
            btnGetOutputFolder.Click += EventEnableFields;

            btnList.Click += EventDisableFields;
            btnList.Click += EventList;

            btnBackup.Click += EventDisableFields;
            btnBackup.Click += EventBackup;

            btnOpenInputFolder.Tag = "Input";
            btnOpenInputFolder.Click += EventRunExplorer;

            btnOpenOutputFolder.Tag = "Output";
            btnOpenOutputFolder.Click += EventRunExplorer;

            rdoSrm.CheckedChanged += EventChangeExtension;
            rdoStates.CheckedChanged += EventChangeExtension;
            rdoAll.CheckedChanged += EventChangeExtension;

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"{Application.ProductName}{new String(' ', 10)}Version: {Application.ProductVersion}";

            rdoStates.Checked = true;
            btnBackup.Enabled = false;

            if (!Internet.ConnectionExists()) Application.Exit();

            var ret = DialogResult.Retry;

            while (ret == DialogResult.Retry)
            {
                if (!Internet.HostExists(BASE_EMUELEC.Replace("\\", "")))
                {
                    ret = MessageBox.Show($"{BASE_EMUELEC} is unavailable\r\n\r\nPlease check if EmuELEC is running and its wifi is properly setup", "EmuELEC communication error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                else ret = DialogResult.OK;
            }

            if (ret == DialogResult.Cancel) Application.Exit();
        }

        private async void EventList(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            isProcessing = true;
            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 0;
            btnBackup.Enabled = false;

            tvwInput.Nodes.Clear();
            tvwInput.Enabled = true;

            string[] listPath = { txtInput.Text };

            UI.selectedExtension = rdoAll.Checked ? FileExtensions.All : (rdoSrm.Checked ? FileExtensions.Srm : FileExtensions.State);

            var myTasks = (from path in listPath select (Task.Run(() => Business.ListDirectory(tvwInput, path)))).ToArray();

            var t = Task.WhenAll(myTasks);
            await t.ContinueWith(x => EndTask());
        }

        public async Task EndTask()
        {
            tvwInput.SafeInvoke(c => c.ExpandAll());
            EnableFields(true);
            btnBackup.SafeInvoke(c => c.Enabled = true);
            isProcessing = false;
            isLoaded = true;
            this.Cursor = Cursors.Default;
        }

        public async Task EndTaskBackup()
        {
            tvwInput.SafeInvoke(c => c.Nodes.Clear());
            EnableFields(true);
            isProcessing = false;
            isLoaded = false;
            this.Cursor = Cursors.Default;
        }

        public async Task EndTaskRecursive()
        {
            await EndTask();
        }

        private void EventChangeExtension(object sender, EventArgs e)
        {
           
            var selectedRadio = UI.searchPatternDic.FirstOrDefault(x => x.Value == "*" + ((RadioButton)sender).Text).Key;

            if (selectedRadio == UI.selectedExtension && isLoaded)
            {
                btnBackup.Enabled = true;
                tvwInput.Enabled = true;
            }
            else
            {
                btnBackup.Enabled = false;
                tvwInput.Enabled = false;
            }
        }

        private async void EventBackup(object sender, EventArgs e)
        {
            var outputFolder = txtOutput.Text;

            if (!FS.FolderExists(outputFolder))
            {
                if (MessageBox.Show($"{outputFolder} does not exist\r\n\r\nDo you want to create it?", "EmuELEC Output Folder verification", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                else
                    FS.CreateFolder(outputFolder);
            }

            this.Cursor = Cursors.WaitCursor;
            isProcessing = true;
            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 0;

            string[] listPath = { outputFolder };

            var myTasks = (from path in listPath select (Task.Run(() => Business.BackupFiles(tvwInput, path)))).ToArray();

            var t = Task.WhenAll(myTasks);
            await t.ContinueWith(x => EndTaskBackup());
        }

        private void EventDisableFields(object sender, EventArgs e)
        {
            EnableFields(false);
        }

        private void EnableFields(bool allow)
        {
            btnList.SafeInvoke(c => c.Enabled = allow);
            btnBackup.SafeInvoke(c => c.Enabled = !allow);
            btnGetInputFolder.SafeInvoke(c => c.Enabled = allow);
            btnGetOutputFolder.SafeInvoke(c => c.Enabled = allow);
            txtOutput.SafeInvoke(c => c.Enabled = allow);
            txtInput.SafeInvoke(c => c.Enabled = allow);
            grpExtensions.SafeInvoke(c => c.Enabled = allow);
            this.SafeInvoke(c => c.ControlBox = allow);

        }

        private void EventEnableFields(object sender, EventArgs e)
        {
            EnableFields(true);
        }

        private void EventGetFolder(object sender, EventArgs e)
        {
            var tag = ((Button)sender).Tag;

            using (FolderBrowserDialog diag = new FolderBrowserDialog
            {
                Description = $"Select {tag} Folder...",
                SelectedPath = ((TextBox)this.Controls["txt" + tag]).Text
            })
            {
                DialogResult result = diag.ShowDialog();

                if (result == DialogResult.OK)
                    ((TextBox)this.Controls["txt" + tag]).Text = diag.SelectedPath;
            }
        }



        private void EventRunExplorer(object sender, EventArgs e)
        {
            var tag = ((Button)sender).Tag;

            Run("explorer.exe", ((TextBox)this.Controls["txt" + tag]).Text);
        }

        private void Run(string command, string args = "")
        {
            var proc = new ProcessStartInfo
            {
                FileName = command,
                Arguments = args
            };
            Process.Start(proc);
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isProcessing && !isLoaded) return;

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var msg = isProcessing ? "This will abort processing!\r\n\r\n" : string.Empty;

                dynamic result = MessageBox.Show($"{msg}Do You Want To Exit?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }


    }
}