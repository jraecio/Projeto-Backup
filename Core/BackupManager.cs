using BackUtilsoftcom.Core;
using System;
using System.IO;
using System.IO.Compression;

namespace BackUtilsoftcom
{
    internal class BackupManager
    {
        private readonly IBackupLogger _logger;
        private readonly AccessBackupService _accessBackupService;
        private readonly SqlBackupService _sqlBackupService;
        private string _caminhoMdb;
        private string _caminhoPastaBackup;


        public BackupManager(IBackupLogger logger, string caminhoMdb)
        {
            _logger = logger;
            _caminhoMdb = caminhoMdb;

            _caminhoPastaBackup = new BackupSettings(_logger, caminhoMdb).GetBackupFolder();

            // Dependências separadas e claras
            _accessBackupService = new AccessBackupService(logger);
            _sqlBackupService = new SqlBackupService(logger);
        }

        public string GetCaminhoPastaBackup()
        {
            return _caminhoPastaBackup;
        }
        public void RealizarBackup(Action<int, string> onProgress)
        {
            if (onProgress != null) onProgress.Invoke(10, "Iniciando backup...");

            // 1. Backup do Access (gera o arquivo compactado e uma cópia backup)
            if (onProgress != null) onProgress.Invoke(20, "Processando banco de dados Access...");
            var infoSeguranca = _accessBackupService.ProcessarBackupAccess(_caminhoMdb);

            if (infoSeguranca == null)
            {
                _logger.Log("❌ Falha ao obter dados de segurança do MDB. Abortando backup SQL.");
                if (onProgress != null) onProgress.Invoke(0, "Falha no backup.");
                return;
            }

            if (onProgress != null) onProgress.Invoke(40, "Backup do Access concluído.");

            // 2. Backup do SQL Server com base nas informações lidas do Access
            if (onProgress != null) onProgress.Invoke(50, "Iniciando backup do SQL Server...");
            _sqlBackupService.ExecutarBackup(infoSeguranca, _caminhoPastaBackup);
            if (onProgress != null) onProgress.Invoke(70, "Backup do SQL Server concluído.");

            if (onProgress != null) onProgress.Invoke(80, "Compactando arquivos...");
            CompactarTudoEmZip(_caminhoMdb);

            _logger.Log("✅ Backup Finalizado.");
            if (onProgress != null) onProgress.Invoke(100, "Backup Finalizado!");
        }

        private void CompactarTudoEmZip(string caminhoMdb)
        {
            try
            {
                _logger.Log("📦 Iniciando compactação completa em ZIP...");

                
        

                if (!Directory.Exists(_caminhoPastaBackup))
                    Directory.CreateDirectory(_caminhoPastaBackup);

                // 📌 Pega o diretório PAI (um nível acima da pasta)
                var parentInfo = Directory.GetParent(_caminhoPastaBackup);
                if (parentInfo == null)
                {
                     _logger.Log("❌ Erro: Não foi possível obter o diretório pai.");
                     return;
                }
                string pastaPai = parentInfo.FullName;

                // 📁 Caminho do arquivo ZIP no mesmo nível da pasta
                string arquivoZip = Path.Combine(
                    pastaPai,
                    string.Format("BackUtilsoftcom_{0:ddMMyy_HHmmss}.zip", DateTime.Now)
                );

                _logger.Log(string.Format("🔍 Compactando conteúdo da pasta: {0}", _caminhoPastaBackup));

                // Cria o arquivo zip
                ZipFile.CreateFromDirectory(_caminhoPastaBackup, arquivoZip, CompressionLevel.Optimal, includeBaseDirectory: false);

                _logger.Log("-------------------------------------------------");
                _logger.Log("🎉 Compactação concluída com sucesso!");
                _logger.Log(string.Format("📁 Arquivo gerado: {0}", arquivoZip));
                _logger.Log("-------------------------------------------------");
            }
            catch (Exception ex)
            {
                _logger.Log("❌ ERRO ao compactar ZIP: " + ex.Message);
            }
        }
    }
}
