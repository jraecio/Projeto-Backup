using System;
using System.Data.SqlClient;
using System.IO;


namespace BackUtilsoftcom.Core
{
    public class BackupServiceSql
    {
        private readonly IBackupLogger _logger;

        public BackupServiceSql(IBackupLogger logger)
        {
            _logger = logger;
        }

        public bool ExecutarBackup(DatabaseConnectionInfo info, string pastaBackup)
        {
            try
            {
                if (info == null)
                {
                    _logger.Log("❌ Objeto InfoSeguranca está nulo. Cancelando backup.");
                    return false;
                }

                _logger.Log("Iniciando preparação do backup SQL...");

                // Verifica campos essenciais
                if (string.IsNullOrWhiteSpace(info.SqlServidor) ||
                    string.IsNullOrWhiteSpace(info.SqlBase) ||
                    string.IsNullOrWhiteSpace(info.SqlLogin))
                {
                    _logger.Log("❌ Dados de conexão incompletos. Verifique o MDB.");
                    return false;
                }

                // Monta servidor + porta
                string servidorCompleto = info.SqlServidor;
                if (!string.IsNullOrWhiteSpace(info.SqlPorta))
                    servidorCompleto += "," + info.SqlPorta;

                // String de conexão
                string connectionString =
                    string.Format("Server={0};Database={1};User Id={2};Password={3};",
                    servidorCompleto, info.SqlBase, info.SqlLogin, info.SqlSenha);

                _logger.Log(string.Format("Conectando ao SQL Server: {0} ...", servidorCompleto));

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    _logger.Log("Conexão com SQL Server estabelecida com sucesso.");

                    // Pasta destino: dentro da pasta do EXE

                    if (!Directory.Exists(pastaBackup))
                    {
                        Directory.CreateDirectory(pastaBackup);
                        _logger.Log(string.Format("Criando pasta de backup: {0}", pastaBackup));
                    }

                    string data = DateTime.Now.ToString("ddMMyy");
                    string nomeArquivo = string.Format("{0}-bkp-{1}.bak", info.SqlBase, data);
                    string caminhoArquivo = Path.Combine(pastaBackup, nomeArquivo);

                    _logger.Log(string.Format("Arquivo de destino: {0}", caminhoArquivo));

                    string sqlCommand =
                        string.Format("BACKUP DATABASE [{0}] TO DISK = '{1}' WITH INIT, SKIP, STATS = 10;", info.SqlBase, caminhoArquivo);

                    _logger.Log("Executando comando BACKUP DATABASE...");

                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    {
                        cmd.CommandTimeout = 0; // sem timeout
                        cmd.ExecuteNonQuery();
                    }

                    _logger.Log("✅ Backup concluído com sucesso!");
                    _logger.Log(string.Format("📁 Arquivo gerado em: {0}", caminhoArquivo));
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Log("❌ ERRO durante o backup: " + ex.Message);
                return false;
            }
        }
    }
}
