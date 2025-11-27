using System;
using System.IO;

namespace BackUtilsoftcom.Core
{
    public class AccessBackupService
    {
        private readonly IBackupLogger _logger;

        public AccessBackupService(IBackupLogger logger)
        {
            _logger = logger;
        }

        public DatabaseConnectionInfo ProcessarBackupAccess(string caminhoMdb)
        {

            _logger.Log("🔧 Iniciando backup do arquivo MDB...");

            var helper = new AccessBackupHandler(_logger, caminhoMdb);

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
            _logger.Log("-------------------------------------------------");
            return info;
        }
    }
}
