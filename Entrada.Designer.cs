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
            this.progressBarBackup = new System.Windows.Forms.ProgressBar();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.gpBoxLocalFront = new System.Windows.Forms.GroupBox();
            this.lblLocalTx = new System.Windows.Forms.Label();
            this.TxLocalFront = new System.Windows.Forms.TextBox();
            this.btnSelecionarFront = new System.Windows.Forms.Button();
            this.gpBoxTipoOperacao = new System.Windows.Forms.GroupBox();
            this.rbRestaurarBackup = new System.Windows.Forms.RadioButton();
            this.rbGerarBackup = new System.Windows.Forms.RadioButton();
            this.gpBoxIniciarCopia = new System.Windows.Forms.GroupBox();
            this.btnIniciarCopia = new System.Windows.Forms.Button();
            this.lblLinckNuvem = new System.Windows.Forms.Label();
            this.linkBkp = new System.Windows.Forms.LinkLabel();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.gpBoxLocalFront.SuspendLayout();
            this.gpBoxTipoOperacao.SuspendLayout();
            this.gpBoxIniciarCopia.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.Color.White;
            this.panelContainer.Controls.Add(this.linkBkp);
            this.panelContainer.Controls.Add(this.lblLinckNuvem);
            this.panelContainer.Controls.Add(this.progressBarBackup);
            this.panelContainer.Controls.Add(this.txtLog);
            this.panelContainer.Controls.Add(this.picLogo);
            this.panelContainer.Controls.Add(this.gpBoxLocalFront);
            this.panelContainer.Controls.Add(this.gpBoxTipoOperacao);
            this.panelContainer.Controls.Add(this.gpBoxIniciarCopia);
            this.panelContainer.Location = new System.Drawing.Point(8, 9);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(500, 420);
            this.panelContainer.TabIndex = 1;
            this.panelContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContainer_Paint);
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
            this.txtLog.Location = new System.Drawing.Point(20, 370);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(460, 40);
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
            this.gpBoxTipoOperacao.Controls.Add(this.rbRestaurarBackup);
            this.gpBoxTipoOperacao.Controls.Add(this.rbGerarBackup);
            this.gpBoxTipoOperacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gpBoxTipoOperacao.ForeColor = System.Drawing.Color.DimGray;
            this.gpBoxTipoOperacao.Location = new System.Drawing.Point(20, 150);
            this.gpBoxTipoOperacao.Name = "gpBoxTipoOperacao";
            this.gpBoxTipoOperacao.Size = new System.Drawing.Size(460, 60);
            this.gpBoxTipoOperacao.TabIndex = 3;
            this.gpBoxTipoOperacao.TabStop = false;
            this.gpBoxTipoOperacao.Text = "Tipo de Operação";
            // 
            // rbRestaurarBackup
            // 
            this.rbRestaurarBackup.AutoSize = true;
            this.rbRestaurarBackup.Location = new System.Drawing.Point(119, 25);
            this.rbRestaurarBackup.Name = "rbRestaurarBackup";
            this.rbRestaurarBackup.Size = new System.Drawing.Size(116, 19);
            this.rbRestaurarBackup.TabIndex = 1;
            this.rbRestaurarBackup.Text = "Restaurar Backup";
            this.rbRestaurarBackup.UseVisualStyleBackColor = true;
            this.rbRestaurarBackup.CheckedChanged += new System.EventHandler(this.rbTipoOperacao_CheckedChanged);
            // 
            // rbGerarBackup
            // 
            this.rbGerarBackup.AutoSize = true;
            this.rbGerarBackup.Checked = true;
            this.rbGerarBackup.Location = new System.Drawing.Point(20, 25);
            this.rbGerarBackup.Name = "rbGerarBackup";
            this.rbGerarBackup.Size = new System.Drawing.Size(95, 19);
            this.rbGerarBackup.TabIndex = 0;
            this.rbGerarBackup.TabStop = true;
            this.rbGerarBackup.Text = "Gerar Backup";
            this.rbGerarBackup.UseVisualStyleBackColor = true;
            this.rbGerarBackup.CheckedChanged += new System.EventHandler(this.rbTipoOperacao_CheckedChanged);
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
            // lblLinckNuvem
            // 
            this.lblLinckNuvem.AutoSize = true;
            this.lblLinckNuvem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLinckNuvem.ForeColor = System.Drawing.Color.DimGray;
            this.lblLinckNuvem.Location = new System.Drawing.Point(20, 330);
            this.lblLinckNuvem.Name = "lblLinckNuvem";
            this.lblLinckNuvem.Size = new System.Drawing.Size(0, 15);
            this.lblLinckNuvem.TabIndex = 10;
            // 
            // linkBkp
            // 
            this.linkBkp.AutoSize = true;
            this.linkBkp.LinkColor = System.Drawing.Color.Orange;
            this.linkBkp.Location = new System.Drawing.Point(20, 310);
            this.linkBkp.Name = "linkBkp";
            this.linkBkp.Size = new System.Drawing.Size(0, 13);
            this.linkBkp.TabIndex = 1;
            this.linkBkp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBkp_LinkClicked);
            // 
            // Entrada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(516, 437);
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
            this.gpBoxTipoOperacao.ResumeLayout(false);
            this.gpBoxTipoOperacao.PerformLayout();
            this.gpBoxIniciarCopia.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.GroupBox gpBoxLocalFront;
        private System.Windows.Forms.Label lblLocalTx;
        private System.Windows.Forms.TextBox TxLocalFront;
        private System.Windows.Forms.Button btnSelecionarFront;
        private System.Windows.Forms.GroupBox gpBoxTipoOperacao;
        private System.Windows.Forms.RadioButton rbGerarBackup;
        private System.Windows.Forms.RadioButton rbRestaurarBackup;
        private System.Windows.Forms.GroupBox gpBoxIniciarCopia;
        private System.Windows.Forms.Button btnIniciarCopia;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.ProgressBar progressBarBackup;
        private System.Windows.Forms.Label lblLinckNuvem;
        private System.Windows.Forms.LinkLabel linkBkp;
    }
}