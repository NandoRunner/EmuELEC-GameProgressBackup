using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using EmuELEC_GameProgressBackup.Model;
using FAndradeTI.Util.Network;
using FAndradeTI.Util.FileSystem;
using FAndradeTI.Util.WinForms;

namespace EmuELEC_GameProgressBackup
{
    public partial class FrmMain : Form
    {
        public readonly string BASE_EMUELEC;

        private bool isProcessing;
        private bool isLoaded;
        private bool isDeleteEnabled;

        public FrmMain()
        {
            InitializeComponent();

            BASE_EMUELEC = "\\\\emuelec";

            isProcessing = false;
            isLoaded = false;
            isDeleteEnabled = false;

            btnGetInputFolder.Tag = "Input";
            btnGetInputFolder.Click += EventDisableFields;
            btnGetInputFolder.Click += EventGetFolder;
            btnGetInputFolder.Click += EventEnableFields;

            btnGetOutputFolder.Tag = "Output";
            btnGetOutputFolder.Click += EventDisableFields;
            btnGetOutputFolder.Click += EventGetFolder;
            btnGetOutputFolder.Click += EventEnableFields;

            btnList.Click += BeginTask;
            btnList.Click += EventDisableFields;
            btnList.Click += EventList;

            btnBackup.Click += EventDisableFields;
            btnBackup.Click += EventBackup;

            btnDelete.Click += EventDisableFields;
            btnDelete.Click += EventDelete;

            btnOpenInputFolder.Tag = "Input";
            btnOpenInputFolder.Click += EventRunExplorer;

            btnOpenOutputFolder.Tag = "Output";
            btnOpenOutputFolder.Click += EventRunExplorer;

            rdoSrm.Click += EventChangeExtension;
            rdoStates.Click += EventChangeExtension;
            rdoAll.Click += EventChangeExtension;

        }

        private void SetTitle(bool online = true)
        {
            var aux = string.Empty;

            if (!online) aux = $"{new String(' ', 10)}(Offline)";

            this.Text = $"{Application.ProductName}{new String(' ', 10)}Version: {Application.ProductVersion}{aux}";
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            rdoStates.Checked = true;
            btnBackup.Enabled = false;
            btnDelete.Enabled = false;
            toolStripMenuItem1.Enabled = false;

            toolStripMenuItem1.Image = imageList1.Images[2];

            CheckConnectivity();

        }
        private bool CheckConnectivity()
        {
            var ret = true;
            if (!Internet.ConnectionExists())
            {
                MessageBox.Show($"Internet connection is unavailable\r\n\r\nEmuELEC communication cannot be done", "Internet connection error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                ret = false;
            }

            if (!Internet.HostExists(BASE_EMUELEC.Replace("\\", "")))
            {
                ret = false;
                MessageBox.Show($"{BASE_EMUELEC} is unavailable\r\n\r\nPlease check if EmuELEC is running and its wifi is properly setup", "EmuELEC communication error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            SetTitle(ret);

            return ret;
        }

        private async void EventList(object sender, EventArgs e)
        {
            if (!CheckConnectivity())
            {
                EventEnableFields(sender, e);
                return;
            }
            
            this.Cursor = Cursors.WaitCursor;

            MenuItemClick();

            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 0;

            tvwInput.Nodes.Clear();
            toolStripMenuItem1.Enabled = true;
            tvwInput.Enabled = false;

            string[] listPath = { txtInput.Text };

            UI.selectedExtension = rdoAll.Checked ? FileExtensions.All : (rdoSrm.Checked ? FileExtensions.Srm : FileExtensions.State);

            var myTasks = (from path in listPath select (Task.Run(() => Business.ListDirectory(tvwInput, path)))).ToArray();

            var t = Task.WhenAll(myTasks);
            await t.ContinueWith(x => EndTask());
        }

        private async void BeginTask(object sender, EventArgs e)
        {
            isLoaded = false;
            isProcessing = true;
            isDeleteEnabled = false;
        }

        public async Task EndTask()
        {
            tvwInput.SafeInvoke(c => c.Enabled = true);
            tvwInput.SafeInvoke(c => c.ExpandAll());
            EnableFields(true);
            //toolStripMenuItem1.Enabled = true;
            isProcessing = false;
            isLoaded = true;
            this.Cursor = Cursors.Arrow;
        }

        public async Task EndTaskBackup()
        {
            tvwInput.SafeInvoke(c => c.Nodes.Clear());
            EnableFields(true);
            isProcessing = false;
            isLoaded = false;
            this.Cursor = Cursors.Arrow;
        }

        public async Task EndTaskDelete()
        {
            tvwInput.SafeInvoke(c => c.Refresh());
            EnableFields(true);
            isProcessing = false;
            this.Cursor = Cursors.Arrow;
        }

        public async Task EndTaskRecursive()
        {
            await EndTask();
        }

        private async void EventBackup(object sender, EventArgs e)
        {
            MenuItemClick();

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

            string[] listPath = { outputFolder };

            var myTasks = (from path in listPath select (Task.Run(() => Business.BackupFiles(tvwInput, path)))).ToArray();

            var t = Task.WhenAll(myTasks);
            await t.ContinueWith(x => EndTaskBackup());
        }

        private async void EventDelete(object sender, EventArgs e)
        {
            MenuItemClick();

            this.Cursor = Cursors.WaitCursor;
            isProcessing = true;
            tsProgressBar.Value = 0;
            tsProgressBar.Maximum = 0;

            int[] listRecentFiles = { 10 };

            var myTasks = (from recentFiles in listRecentFiles select (Task.Run(() => Business.DeleteFiles(tvwInput, recentFiles)))).ToArray();

            var t = Task.WhenAll(myTasks);
            await t.ContinueWith(x => EndTaskDelete());
        }

        private void EventDisableFields(object sender, EventArgs e)
        {
            EnableFields(false);
        }

        private void EnableFields(bool allow)
        {
            btnList.SafeInvoke(c => c.Enabled = allow);
            
            if (isLoaded) { 
            btnBackup.SafeInvoke(c => c.Enabled = allow);
            }
            else
            {
                btnBackup.SafeInvoke(c => c.Enabled = false);
            }
            btnGetInputFolder.SafeInvoke(c => c.Enabled = allow);
            btnGetOutputFolder.SafeInvoke(c => c.Enabled = allow);
            txtOutput.SafeInvoke(c => c.Enabled = allow);
            txtInput.SafeInvoke(c => c.Enabled = allow);
            grpExtensions.SafeInvoke(c => c.Enabled = allow);
            this.SafeInvoke(c => c.ControlBox = allow);
            //toolStripMenuItem1.Enabled = allow;

        }

        private void EventEnableFields(object sender, EventArgs e)
        {
            EnableFields(true);
        }

        private void EventChangeExtension(object sender, EventArgs e)
        {

            var selectedRadio = UI.searchPatternDic.FirstOrDefault(x => x.Value == "*" + ((RadioButton)sender).Text).Key;

            if (selectedRadio == UI.selectedExtension && isLoaded)
            {
                btnBackup.Enabled = true;

                btnDelete.Enabled = isDeleteEnabled;
                toolStripMenuItem1.Enabled = true;
                tvwInput.Enabled = true;
            }
            else
            {
                btnBackup.Enabled = false;
                btnDelete.Enabled = false;
                toolStripMenuItem1.Enabled = false;
                tvwInput.Enabled = false;
            }
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

            ProcManager.RunExplorer(((TextBox)this.Controls["txt" + tag]).Text);
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

        private void MenuItemClick()
        {
            if (!isDeleteEnabled)
            {
                toolStripMenuItem1.Text = "Enable Delete";
                toolStripMenuItem1.Image = imageList1.Images[2];
            }
            else
            {
                toolStripMenuItem1.Text = "Disable Delete";
                toolStripMenuItem1.Image = imageList1.Images[1];
            }

            tvwInput.ExpandAll();
            btnDelete.Enabled = isDeleteEnabled;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            isDeleteEnabled = !isDeleteEnabled;
            MenuItemClick();

        }
    }
}