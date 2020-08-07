namespace EmuELEC_GameProgressBackup
{
    partial class FrmMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tvwInput = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnList = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGetInputFolder = new System.Windows.Forms.Button();
            this.grpExtensions = new System.Windows.Forms.GroupBox();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.rdoSrm = new System.Windows.Forms.RadioButton();
            this.rdoStates = new System.Windows.Forms.RadioButton();
            this.btnGetOutputFolder = new System.Windows.Forms.Button();
            this.btnOpenInputFolder = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOpenOutputFolder = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpExtensions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Output Folder:";
            // 
            // txtInput
            // 
            this.txtInput.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInput.Location = new System.Drawing.Point(146, 29);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(522, 23);
            this.txtInput.TabIndex = 2;
            this.txtInput.Text = "\\\\emuelec\\roms";
            // 
            // txtOutput
            // 
            this.txtOutput.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(146, 61);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(522, 23);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.Text = "c:\\temp\\EmuELEC-GameProgressBackup";
            // 
            // btnBackup
            // 
            this.btnBackup.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.ImageIndex = 1;
            this.btnBackup.ImageList = this.imageList2;
            this.btnBackup.Location = new System.Drawing.Point(376, 99);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(112, 48);
            this.btnBackup.TabIndex = 4;
            this.btnBackup.Text = "&Backup";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackup.UseVisualStyleBackColor = true;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "exit.png");
            this.imageList2.Images.SetKeyName(1, "backup.png");
            this.imageList2.Images.SetKeyName(2, "log.png");
            this.imageList2.Images.SetKeyName(3, "doc-delete.png");
            // 
            // tvwInput
            // 
            this.tvwInput.ContextMenuStrip = this.contextMenuStrip1;
            this.tvwInput.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tvwInput.Location = new System.Drawing.Point(32, 156);
            this.tvwInput.Name = "tvwInput";
            this.tvwInput.Size = new System.Drawing.Size(712, 359);
            this.tvwInput.TabIndex = 5;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem1.Text = "Enable delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // btnList
            // 
            this.btnList.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnList.ImageIndex = 2;
            this.btnList.ImageList = this.imageList2;
            this.btnList.Location = new System.Drawing.Point(258, 99);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(112, 48);
            this.btnList.TabIndex = 6;
            this.btnList.Text = "&List";
            this.btnList.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // tsInfo
            // 
            this.tsInfo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsInfo.Name = "tsInfo";
            this.tsInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // btnGetInputFolder
            // 
            this.btnGetInputFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetInputFolder.Location = new System.Drawing.Point(674, 27);
            this.btnGetInputFolder.Name = "btnGetInputFolder";
            this.btnGetInputFolder.Size = new System.Drawing.Size(32, 26);
            this.btnGetInputFolder.TabIndex = 28;
            this.btnGetInputFolder.Text = "...";
            // 
            // grpExtensions
            // 
            this.grpExtensions.Controls.Add(this.rdoAll);
            this.grpExtensions.Controls.Add(this.rdoSrm);
            this.grpExtensions.Controls.Add(this.rdoStates);
            this.grpExtensions.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpExtensions.Location = new System.Drawing.Point(32, 90);
            this.grpExtensions.Name = "grpExtensions";
            this.grpExtensions.Size = new System.Drawing.Size(220, 57);
            this.grpExtensions.TabIndex = 29;
            this.grpExtensions.TabStop = false;
            this.grpExtensions.Text = "File Extension";
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Enabled = false;
            this.rdoAll.Location = new System.Drawing.Point(159, 23);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(52, 20);
            this.rdoAll.TabIndex = 2;
            this.rdoAll.Tag = "2";
            this.rdoAll.Text = "[all]";
            this.rdoAll.UseVisualStyleBackColor = true;
            this.rdoAll.Visible = false;
            // 
            // rdoSrm
            // 
            this.rdoSrm.AutoSize = true;
            this.rdoSrm.Location = new System.Drawing.Point(99, 23);
            this.rdoSrm.Name = "rdoSrm";
            this.rdoSrm.Size = new System.Drawing.Size(54, 20);
            this.rdoSrm.TabIndex = 1;
            this.rdoSrm.Tag = "1";
            this.rdoSrm.Text = ".srm";
            this.rdoSrm.UseVisualStyleBackColor = true;
            // 
            // rdoStates
            // 
            this.rdoStates.AutoSize = true;
            this.rdoStates.Checked = true;
            this.rdoStates.Location = new System.Drawing.Point(17, 23);
            this.rdoStates.Name = "rdoStates";
            this.rdoStates.Size = new System.Drawing.Size(66, 20);
            this.rdoStates.TabIndex = 0;
            this.rdoStates.TabStop = true;
            this.rdoStates.Tag = "0";
            this.rdoStates.Text = ".state";
            this.rdoStates.UseVisualStyleBackColor = true;
            // 
            // btnGetOutputFolder
            // 
            this.btnGetOutputFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetOutputFolder.Location = new System.Drawing.Point(674, 57);
            this.btnGetOutputFolder.Name = "btnGetOutputFolder";
            this.btnGetOutputFolder.Size = new System.Drawing.Size(32, 26);
            this.btnGetOutputFolder.TabIndex = 30;
            this.btnGetOutputFolder.Text = "...";
            // 
            // btnOpenInputFolder
            // 
            this.btnOpenInputFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenInputFolder.ImageIndex = 0;
            this.btnOpenInputFolder.ImageList = this.imageList1;
            this.btnOpenInputFolder.Location = new System.Drawing.Point(712, 27);
            this.btnOpenInputFolder.Name = "btnOpenInputFolder";
            this.btnOpenInputFolder.Size = new System.Drawing.Size(32, 26);
            this.btnOpenInputFolder.TabIndex = 31;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder-open.png");
            this.imageList1.Images.SetKeyName(1, "cross-uncheck.png");
            this.imageList1.Images.SetKeyName(2, "tick-check.png");
            // 
            // btnOpenOutputFolder
            // 
            this.btnOpenOutputFolder.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenOutputFolder.ImageIndex = 0;
            this.btnOpenOutputFolder.ImageList = this.imageList1;
            this.btnOpenOutputFolder.Location = new System.Drawing.Point(712, 57);
            this.btnOpenOutputFolder.Name = "btnOpenOutputFolder";
            this.btnOpenOutputFolder.Size = new System.Drawing.Size(32, 26);
            this.btnOpenOutputFolder.TabIndex = 32;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.ImageIndex = 0;
            this.btnExit.ImageList = this.imageList2;
            this.btnExit.Location = new System.Drawing.Point(632, 99);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(112, 48);
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.ImageIndex = 3;
            this.btnDelete.ImageList = this.imageList2;
            this.btnDelete.Location = new System.Drawing.Point(494, 99);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 48);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnDelete, "Delete all but the last 10 recent sates of each game");
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Delete";
            // 
            // FrmMain
            // 
            this.AcceptButton = this.btnList;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOpenOutputFolder);
            this.Controls.Add(this.btnOpenInputFolder);
            this.Controls.Add(this.btnGetOutputFolder);
            this.Controls.Add(this.grpExtensions);
            this.Controls.Add(this.btnGetInputFolder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.tvwInput);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmuELEC Game Progress Backup ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpExtensions.ResumeLayout(false);
            this.grpExtensions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.TreeView tvwInput;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsInfo;
        internal System.Windows.Forms.Button btnGetInputFolder;
        private System.Windows.Forms.GroupBox grpExtensions;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.RadioButton rdoSrm;
        private System.Windows.Forms.RadioButton rdoStates;
        internal System.Windows.Forms.Button btnGetOutputFolder;
        internal System.Windows.Forms.Button btnOpenInputFolder;
        internal System.Windows.Forms.Button btnOpenOutputFolder;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

