using BackUtilsoftcom.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BackUtilsoftcom
{
    public partial class Entrada : Form, IBackupLogger
    {
        public string LinkBkpText ;
        public Entrada()
        {
            InitializeComponent();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }
        public void Log(string s)
        {
            if (InvokeRequired)
                Invoke(new Action(() => txtLog.AppendText(string.Format("{0:T} {1}\r\n", DateTime.Now, s))));
            else
                txtLog.AppendText(string.Format("{0:T} {1}\r\n", DateTime.Now, s));
        }

        private void btnSelecionarFront_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Title = "Selecione o arquivo Front ou atalho";
                    dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                    dialog.Filter = "Arquivos MDB ou Atalhos (*.mdb;*.lnk)|*.mdb;*.lnk|Todos os arquivos (*.*)|*.*";

                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string entrada = dialog.FileName;

                        // 1. Se for um arquivo .lnk (atalho do Windows)
                        if (entrada.EndsWith(".lnk", StringComparison.OrdinalIgnoreCase))
                        {
                            entrada = ResolverAtalho(entrada);
                        }

                        // 2. Extrair o MDB do texto/atalho
                        string caminhoMdb = ExtrairCaminhoMdb(entrada);

                        if (caminhoMdb != null && File.Exists(caminhoMdb))
                        {
                            TxLocalFront.Text = caminhoMdb;
                            linkBkp.Text = null;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Não foi possível identificar um arquivo MDB válido.",
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocorreu um erro ao selecionar o arquivo:\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string ResolverAtalho(string caminhoAtalho)
        {
            try
            {
                // Usa COM para ler atalhos
                Type wshShell = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(wshShell);
                dynamic shortcut = shell.CreateShortcut(caminhoAtalho);

                return shortcut.Arguments != null && shortcut.Arguments.ToString().Length > 0
                    ? string.Format("{0} {1}", shortcut.TargetPath, shortcut.Arguments)
                    : shortcut.TargetPath;
            }
            catch
            {
                return caminhoAtalho; // se falhar, devolve o próprio caminho
            }
        }


        private string ExtrairCaminhoMdb(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            // Remove aspas e espaços
            input = input.Trim().Trim('"');

            // Caso seja um arquivo MDB direto
            if (input.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase) && File.Exists(input))
                return input;

            // Verifica se existe algum .mdb na string
            if (input.IndexOf(".mdb", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                // Divide a linha por aspas
                string[] partes = input.Split('"');

                foreach (var p in partes)
                {
                    string path = p.Trim();

                    if (path.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase) && File.Exists(path))
                        return path;
                }
            }

            return null;
        }


        private async void btnIniciarCopia_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxLocalFront.Text))
            {
                MessageBox.Show("Selecione o arquivo Front primeiro!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnIniciarCopia.Enabled = false;
            btnSelecionarFront.Enabled = false;
            btnFechar.Enabled = false;
            progressBarBackup.Visible = true;
            progressBarBackup.Value = 0;
            lblStatus.Text = "Iniciando...";

            try
            {
                var backup = new BackupManager(this, TxLocalFront.Text);

                await Task.Run(() =>
                {
                    backup.RealizarBackup((progress, status) =>
                    {
                        // Atualiza UI na thread principal
                        this.Invoke(new Action(() =>
                        {
                            progressBarBackup.Value = progress;
                            lblStatus.Text = status;
                        }));
                    });
                });

                LinkBkpText = backup.GetCaminhoPastaBackup();

                if (LinkBkpText != null && LinkBkpText.Length > 55)
                    linkBkp.Text = LinkBkpText.Substring(0, 55) + "...";
                else
                    linkBkp.Text = LinkBkpText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar backup: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Erro no backup.";
            }
            finally
            {
                btnIniciarCopia.Enabled = true;
                btnSelecionarFront.Enabled = true;
                btnFechar.Enabled = true;
                // progressBarBackup.Visible = false; // Opcional: manter visível para mostrar 100%
            }
        }


        private void linkBkp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Caminho da pasta que você quer abrir:
            if (string.IsNullOrEmpty(LinkBkpText)) return;
            var parent = Directory.GetParent(LinkBkpText);
            if (parent == null) return;
            string pasta = parent.FullName;

            if (Directory.Exists(pasta))
            {
                // Abre o Explorer na pasta
                Process.Start("explorer.exe", pasta);
            }
            else
            {
                MessageBox.Show("A pasta não existe: " + pasta);
            }

        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void rbTipoOperacao_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRestaurarBackup.Checked)
            {
                btnIniciarCopia.Text = "RESTAURAR BACKUP";
                gpBoxLocalFront.Text = "Destino do Arquivo (Pasta)";
            }
            else
            {
                btnIniciarCopia.Text = "INICIAR BACKUP";
                gpBoxLocalFront.Text = "Origem dos Dados";
            }
        }

    }
}
