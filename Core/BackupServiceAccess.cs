using System;
using System.IO;

namespace BackUtilsoftcom.Core
{
    public class BackupServiceAccess
    {
        private readonly IBackupLogger _logger;

        public BackupServiceAccess(IBackupLogger logger)
        {
            _logger = logger;
        }

        public DatabaseConnectionInfo ProcessarBackupAccess(string caminhoMdb)
        {
            _logger.Log("-------------------------------------------------");
            _logger.Log("🔧 Iniciando backup do arquivo MDB...");
            _logger.Log("-------------------------------------------------");

            var helper = new BackupHandlerAccess(_logger, caminhoMdb);

            // 1. Criar cópia de backup
            string arquivoBackup = helper.CopiarArquivoMdbParaBackup();

            if (arquivoBackup == null)
            {
                _logger.Log("❌ Não foi possível copiar o MDB para a pasta de backup.");
                return null;
            }

            // 2. Compactar e reparar (no arquivo original)
            helper.CompactarRepararAccess();

            // 3. Ler dados de segurança
            var info = helper.LerDadosSeguranca();

            if (info == null)
            {
                _logger.Log("❌ Não foi possível ler os dados de segurança do MDB.");
                return null;
            }

            _logger.Log("✅ Backup e compactação do MDB concluídos.");
            return info;
        }
    }
}
