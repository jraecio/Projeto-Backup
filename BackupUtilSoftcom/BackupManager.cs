using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BackupUtilSoftcom
{
    public class BackupManager
    {
        private readonly IBackupLogger _logger;
        private readonly AccessBackupService _accessBackupService;
        private readonly SqlBackupService _sqlBackupService;
        private string _caminhoMdb;
        private string _caminhoPastaBackup;
        private string _caminhoArquivoZip;


        public BackupManager(IBackupLogger logger, string caminhoMdb)
        {
            _logger = logger;
            _caminhoMdb = caminhoMdb;

            _caminhoPastaBackup = new BackupSettings(_logger, caminhoMdb).GetBackupFolder();

            // Dependências separadas e claras
            _accessBackupService = new AccessBackupService(logger);
            _sqlBackupService = new SqlBackupService(logger);
        }
        public async Task RealizarBackup(Action<int, string> onProgress, bool enviarnuvem)
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

            if (onProgress != null) onProgress.Invoke(45, "Iniciando backup do SQL Server...");

            bool sucesso = new bool();

            sucesso =  _sqlBackupService.ExecutarBackup(infoSeguranca, _caminhoPastaBackup);

            if (!sucesso)
            {
                _logger.Log("Falha ao executar backup do SQL Server.");
                return; // finaliza o método
            }

            if (onProgress != null) onProgress.Invoke(50, "Backup do SQL Server concluído.");

            if (onProgress != null) onProgress.Invoke(60, "Compactando arquivos...");

            sucesso =  CompactarTudoEmZip(_caminhoMdb);

            if (!sucesso)
            {
                _logger.Log("Falha ao Compactando arquivos.");
                return; // finaliza o método
            }

            if (enviarnuvem)
            {
                if (onProgress != null) onProgress.Invoke(75, "Enviando arquivos para nuvem...");
                sucesso = await enviarCloudflire();

                if (!sucesso)
                {
                    _logger.Log("❌ Upload para nuvem falhou! Processo interrompido.");
                    return; 
                }


            }
            _logger.Log("✅ Backup Finalizado.");
            if (onProgress != null) onProgress.Invoke(100, "Backup Finalizado!");
        }

        public string GetCaminhoPastaBackup()
        {
            return _caminhoPastaBackup;
        }
        public string GetArquivozip()
        {
            return Path.GetFileName(_caminhoArquivoZip);
        }
        private bool CompactarTudoEmZip(string caminhoMdb)
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
                     return false;
                }
                string pastaPai = parentInfo.FullName;

                // 📁 Caminho do arquivo ZIP no mesmo nível da pasta
                _caminhoArquivoZip = Path.Combine(
                    pastaPai,
                    string.Format("BackUtilsoftcom_{0:ddMMyy_HHmmss}.zip", DateTime.Now)
                );

                _logger.Log(string.Format("🔍 Compactando conteúdo da pasta: {0}", _caminhoPastaBackup));

                // Cria o arquivo zip
                ZipFile.CreateFromDirectory(_caminhoPastaBackup, _caminhoArquivoZip, CompressionLevel.Optimal, includeBaseDirectory: false);
                _logger.Log("🎉 Compactação concluída com sucesso!");
                _logger.Log(string.Format("📁 Arquivo gerado: {0}", _caminhoArquivoZip));
                _logger.Log("-------------------------------------------------");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log("❌ ERRO ao compactar ZIP: " + ex.Message);
                return false;
            }
        }
        private async Task<bool> enviarCloudflire()
        {
            _logger.Log("Enviando arquivo para nuvem");
            CloudflareConfig cfg = CloudflareConfigDecoder.LoadConfig();
            //CloudflareConfig cfg = new CloudflareConfig();
            //cfg.Endpoint = "https://9f2c0aa4f354e316da08c86b629f9d13.r2.cloudflarestorage.com";
            //cfg.AccessKey = "840e62385cb5237cd41597ed899883c8";
            //cfg.SecretKey = "327cb08a3bbe820b62ff1bb2931837bbd64de0fb4227345c48c73b3b5e72b917";
            //cfg.Bucket = "upsoftware";
            cfg.ObjectKey = $"{GetArquivozip()}";
            cfg.FilePath = $@"{_caminhoArquivoZip}";
            var result = await CloudflareApi.UploadFileAsync(cfg);

            if (result.Item1)
            {
                _logger.Log("📦 arquivo salvo em nuvem");
                _logger.Log("-------------------------------------------------");
                return true;
            }
            else
            { 
                _logger.Log("FALHOU!");
                _logger.Log("Código HTTP: " + result.Item2);
                _logger.Log("Erro: " + result.Item3);
                _logger.Log("-------------------------------------------------");
                return false;
            }
        }
    }
}
