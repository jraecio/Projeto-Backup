using System;
using System.Data.SqlClient;
using System.IO;


namespace BackUtilsoftcom.Core
{
    public class SqlBackupService
    {
        private readonly ILogger _logger;

        public SqlBackupService(ILogger logger)
        {
            _logger = logger;
        }

        public bool ExecutarBackup(InfoSeguranca info)
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
                    servidorCompleto += $",{info.SqlPorta}";

                // String de conexão
                string connectionString =
                    $"Server={servidorCompleto};" +
                    $"Database={info.SqlBase};" +
                    $"User Id={info.SqlLogin};" +
                    $"Password={info.SqlSenha};";

                _logger.Log($"Conectando ao SQL Server: {servidorCompleto} ...");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    _logger.Log("Conexão com SQL Server estabelecida com sucesso.");

                    // Pasta destino: dentro da pasta do EXE
                    string pastaBackup = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "BackUtilsoftcom");

                    if (!Directory.Exists(pastaBackup))
                    {
                        Directory.CreateDirectory(pastaBackup);
                        _logger.Log($"Criando pasta de backup: {pastaBackup}");
                    }

                    string data = DateTime.Now.ToString("ddMMyy");
                    string nomeArquivo = $"{info.SqlBase}-bkp-{data}.bak";
                    string caminhoArquivo = Path.Combine(pastaBackup, nomeArquivo);

                    _logger.Log($"Arquivo de destino: {caminhoArquivo}");

                    string sqlCommand =
                        $"BACKUP DATABASE [{info.SqlBase}] TO DISK = '{caminhoArquivo}' " +
                        $"WITH INIT, SKIP, STATS = 10;";

                    _logger.Log("Executando comando BACKUP DATABASE...");

                    using (SqlCommand cmd = new SqlCommand(sqlCommand, conn))
                    {
                        cmd.CommandTimeout = 0; // sem timeout
                        cmd.ExecuteNonQuery();
                    }

                    _logger.Log("✅ Backup concluído com sucesso!");
                    _logger.Log($"📁 Arquivo gerado em: {caminhoArquivo}");
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
