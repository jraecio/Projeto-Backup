namespace BackUtilsoftcom
{
    partial class Entrada
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Entrada));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.chB_BackupEmNuvem = new System.Windows.Forms.CheckBox();
            this.lblLinckNuvem = new System.Windows.Forms.LinkLabel();
            this.lblLinkBkp = new System.Windows.Forms.LinkLabel();
            this.progressBarBackup = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.gpBoxLocalFront = new System.Windows.Forms.GroupBox();
            this.lblLocalTx = new System.Windows.Forms.Label();
            this.TxLocalFront = new System.Windows.Forms.TextBox();
            this.btnSelecionarFront = new System.Windows.Forms.Button();
            this.gpBoxTipoOperacao = new System.Windows.Forms.GroupBox();
            this.gpBoxIniciarCopia = new System.Windows.Forms.GroupBox();
            this.btnIniciarCopia = new System.Windows.Forms.Button();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.gpBoxLocalFront.SuspendLayout();
            this.gpBoxIniciarCopia.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Controls.Add(this.chB_BackupEmNuvem);
            this.panelContainer.Controls.Add(this.lblLinckNuvem);
            this.panelContainer.Controls.Add(this.lblLinkBkp);
            this.panelContainer.Controls.Add(this.progressBarBackup);
            this.panelContainer.Controls.Add(this.txtLog);
            this.panelContainer.Controls.Add(this.picLogo);
            this.panelContainer.Controls.Add(this.gpBoxLocalFront);
            this.panelContainer.Controls.Add(this.gpBoxTipoOperacao);
            this.panelContainer.Controls.Add(this.gpBoxIniciarCopia);
            this.panelContainer.Location = new System.Drawing.Point(8, 9);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(500, 463);
            this.panelContainer.TabIndex = 1;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
            // 
            // chB_BackupEmNuvem
            // 
            this.chB_BackupEmNuvem.AutoSize = true;
            this.chB_BackupEmNuvem.Checked = true;
            this.chB_BackupEmNuvem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chB_BackupEmNuvem.Location = new System.Drawing.Point(35, 175);
            this.chB_BackupEmNuvem.Name = "chB_BackupEmNuvem";
            this.chB_BackupEmNuvem.Size = new System.Drawing.Size(117, 17);
            this.chB_BackupEmNuvem.TabIndex = 12;
            this.chB_BackupEmNuvem.Text = "Backup em Nuvem";
            this.chB_BackupEmNuvem.UseVisualStyleBackColor = true;
            // 
            // lblLinckNuvem
            // 
            this.lblLinckNuvem.AutoSize = true;
            this.lblLinckNuvem.LinkColor = System.Drawing.Color.Orange;
            this.lblLinckNuvem.Location = new System.Drawing.Point(20, 325);
            this.lblLinckNuvem.Name = "lblLinckNuvem";
            this.lblLinckNuvem.Size = new System.Drawing.Size(0, 13);
            this.lblLinckNuvem.TabIndex = 11;
            this.lblLinckNuvem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLinckNuvem_LinkClicked);
            // 
            // lblLinkBkp
            // 
            this.lblLinkBkp.AutoSize = true;
            this.lblLinkBkp.LinkColor = System.Drawing.Color.Orange;
            this.lblLinkBkp.Location = new System.Drawing.Point(20, 298);
            this.lblLinkBkp.Name = "lblLinkBkp";
            this.lblLinkBkp.Size = new System.Drawing.Size(0, 13);
            this.lblLinkBkp.TabIndex = 1;
            this.lblLinkBkp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBkp_LinkClicked);
            // 
            // progressBarBackup
            // 
            this.progressBarBackup.Location = new System.Drawing.Point(20, 350);
            this.progressBarBackup.Name = "progressBarBackup";
            this.progressBarBackup.Size = new System.Drawing.Size(460, 10);
            this.progressBarBackup.TabIndex = 8;
            this.progressBarBackup.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.txtLog.ForeColor = System.Drawing.Color.Gray;
            this.txtLog.Location = new System.Drawing.Point(20, 366);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(460, 90);
            this.txtLog.TabIndex = 7;
            // 
            // picLogo
            // 
            this.picLogo.Image = global::BackUtilsoftcom.Properties.Resources.Logo_Softcom_Pequena;
            this.picLogo.Location = new System.Drawing.Point(20, 20);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 28);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // gpBoxLocalFront
            // 
            this.gpBoxLocalFront.Controls.Add(this.lblLocalTx);
            this.gpBoxLocalFront.Controls.Add(this.TxLocalFront);
            this.gpBoxLocalFront.Controls.Add(this.btnSelecionarFront);
            this.gpBoxLocalFront.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gpBoxLocalFront.ForeColor = System.Drawing.Color.DimGray;
            this.gpBoxLocalFront.Location = new System.Drawing.Point(20, 70);
            this.gpBoxLocalFront.Name = "gpBoxLocalFront";
            this.gpBoxLocalFront.Size = new System.Drawing.Size(460, 70);
            this.gpBoxLocalFront.TabIndex = 2;
            this.gpBoxLocalFront.TabStop = false;
            this.gpBoxLocalFront.Text = "Origem dos Dados";
            // 
            // lblLocalTx
            // 
            this.lblLocalTx.AutoSize = true;
            this.lblLocalTx.Location = new System.Drawing.Point(15, 25);
            this.lblLocalTx.Name = "lblLocalTx";
            this.lblLocalTx.Size = new System.Drawing.Size(38, 15);
            this.lblLocalTx.TabIndex = 0;
            this.lblLocalTx.Text = "Local:";
            // 
            // TxLocalFront
            // 
            this.TxLocalFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxLocalFront.Location = new System.Drawing.Point(60, 22);
            this.TxLocalFront.Name = "TxLocalFront";
            this.TxLocalFront.Size = new System.Drawing.Size(340, 23);
            this.TxLocalFront.TabIndex = 1;
            // 
            // btnSelecionarFront
            // 
            this.btnSelecionarFront.BackColor = System.Drawing.Color.Orange;
            this.btnSelecionarFront.FlatAppearance.BorderSize = 0;
            this.btnSelecionarFront.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionarFront.ForeColor = System.Drawing.Color.White;
            this.btnSelecionarFront.Location = new System.Drawing.Point(410, 21);
            this.btnSelecionarFront.Name = "btnSelecionarFront";
            this.btnSelecionarFront.Size = new System.Drawing.Size(35, 25);
            this.btnSelecionarFront.TabIndex = 2;
            this.btnSelecionarFront.Text = "...";
            this.btnSelecionarFront.UseVisualStyleBackColor = false;
            this.btnSelecionarFront.Click += new System.EventHandler(this.btnSelecionarFront_Click);
            // 
            // gpBoxTipoOperacao
            // 
            this.gpBoxTipoOperacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gpBoxTipoOperacao.ForeColor = System.Drawing.Color.DimGray;
            this.gpBoxTipoOperacao.Location = new System.Drawing.Point(20, 150);
            this.gpBoxTipoOperacao.Name = "gpBoxTipoOperacao";
            this.gpBoxTipoOperacao.Size = new System.Drawing.Size(460, 60);
            this.gpBoxTipoOperacao.TabIndex = 3;
            this.gpBoxTipoOperacao.TabStop = false;
            this.gpBoxTipoOperacao.Text = "Tipo de Operação";
            // 
            // gpBoxIniciarCopia
            // 
            this.gpBoxIniciarCopia.Controls.Add(this.btnIniciarCopia);
            this.gpBoxIniciarCopia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gpBoxIniciarCopia.ForeColor = System.Drawing.Color.DimGray;
            this.gpBoxIniciarCopia.Location = new System.Drawing.Point(20, 220);
            this.gpBoxIniciarCopia.Name = "gpBoxIniciarCopia";
            this.gpBoxIniciarCopia.Size = new System.Drawing.Size(460, 69);
            this.gpBoxIniciarCopia.TabIndex = 4;
            this.gpBoxIniciarCopia.TabStop = false;
            this.gpBoxIniciarCopia.Text = "Ação";
            // 
            // btnIniciarCopia
            // 
            this.btnIniciarCopia.BackColor = System.Drawing.Color.Orange;
            this.btnIniciarCopia.FlatAppearance.BorderSize = 0;
            this.btnIniciarCopia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIniciarCopia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIniciarCopia.ForeColor = System.Drawing.Color.White;
            this.btnIniciarCopia.Location = new System.Drawing.Point(15, 20);
            this.btnIniciarCopia.Name = "btnIniciarCopia";
            this.btnIniciarCopia.Size = new System.Drawing.Size(430, 35);
            this.btnIniciarCopia.TabIndex = 0;
            this.btnIniciarCopia.Text = "INICIAR BACKUP";
            this.btnIniciarCopia.UseVisualStyleBackColor = false;
            this.btnIniciarCopia.Click += new System.EventHandler(this.btnIniciarCopia_Click);
            // 
            // Entrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(516, 484);
            this.Controls.Add(this.panelContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Entrada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup Utility";
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.gpBoxLocalFront.ResumeLayout(false);
            this.gpBoxLocalFront.PerformLayout();
            this.gpBoxIniciarCopia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.GroupBox gpBoxLocalFront;
        private System.Windows.Forms.Label lblLocalTx;
        private System.Windows.Forms.TextBox TxLocalFront;
        private System.Windows.Forms.Button btnSelecionarFront;
        private System.Windows.Forms.GroupBox gpBoxIniciarCopia;
        private System.Windows.Forms.Button btnIniciarCopia;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ProgressBar progressBarBackup;
        private System.Windows.Forms.LinkLabel lblLinkBkp;
        private System.Windows.Forms.LinkLabel lblLinckNuvem;
        private System.Windows.Forms.CheckBox chB_BackupEmNuvem;
        private System.Windows.Forms.GroupBox gpBoxTipoOperacao;
    }
}