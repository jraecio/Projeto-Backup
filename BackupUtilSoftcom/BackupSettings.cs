using System;
using System.IO;

namespace BackupUtilSoftcom
{
    internal class BackupSettings
    {
        private readonly string _baseFolder;
        private readonly IBackupLogger _logger;

        public BackupSettings(IBackupLogger logger, string mdbPath)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            _logger = logger;

            // 1) Checagens básicas
            if (string.IsNullOrWhiteSpace(mdbPath))
            {
                _logger.Log("Erro: o caminho do arquivo MDB está nulo ou vazio.");
                throw new ArgumentException("O caminho do arquivo MDB não pode ser nulo ou vazio.", "mdbPath");
            }

            // 2) Caminho precisa ser absoluto
            if (!Path.IsPathRooted(mdbPath))
            {
                _logger.Log(string.Format(
                    "Erro: o caminho do arquivo MDB não é absoluto. Valor: {0}", mdbPath));
                throw new ArgumentException("O caminho do arquivo MDB deve ser absoluto.", "mdbPath");
            }

            // 3) Extensão precisa ser .mdb
            string extension = Path.GetExtension(mdbPath);
            if (!string.Equals(extension, ".mdb", StringComparison.OrdinalIgnoreCase))
            {
                _logger.Log(string.Format(
                    "Erro: o caminho informado não aponta para um arquivo .mdb. Valor: {0}", mdbPath));
                throw new ArgumentException("O caminho informado não aponta para um arquivo .mdb.", "mdbPath");
            }

            try
            {
                // 4) Extrai a pasta base: ex.: C:\Softcom
                _baseFolder = Path.GetDirectoryName(mdbPath);
            }
            catch (Exception ex)
            {
                _logger.Log(string.Format(
                    "Erro ao obter a pasta base a partir do caminho do arquivo MDB: {0}. Exception: {1}",
                    mdbPath,
                    ex));

                throw new ArgumentException(
                    "Erro ao obter a pasta base a partir do caminho do arquivo MDB.",
                    "mdbPath",
                    ex);
            }

            if (string.IsNullOrEmpty(_baseFolder))
            {
                _logger.Log(string.Format(
                    "Erro: não foi possível determinar a pasta base a partir do caminho do arquivo MDB: {0}",
                    mdbPath));

                throw new ArgumentException(
                    "Não foi possível determinar a pasta base a partir do caminho do arquivo MDB.",
                    "mdbPath");
            }

            _logger.Log(string.Format(
                "BackupSettings inicializado com sucesso. Pasta base: {0}", _baseFolder));
        }

        /// <summary>
        /// Retorna a pasta de backup do dia atual e garante que ela exista.
        /// Ex.: C:\Softcom\Backup\2025\11\21
        /// </summary>
        public string GetBackupFolder()
        {
            try
            {
                DateTime today = DateTime.Today;

                string folder = Path.Combine(
                    _baseFolder,
                    "Backup",
                    today.Year.ToString("0000"),
                    today.Month.ToString("00"),
                    today.Day.ToString("00"),
                    "BackUtilsoftcom");

                _logger.Log(string.Format(
                    "Gerando pasta de backup para a data {0:dd/MM/yyyy}: {1}",
                    today,
                    folder));

                EnsureBackupFolder(folder);

                _logger.Log(string.Format(
                    "Pasta de backup pronta para uso: {0}", folder));

                return folder;
            }
            catch (PathTooLongException ex)
            {
                _logger.Log(string.Format(
                    "Erro: o caminho da pasta de backup ficou muito longo. Exception: {0}", ex));

                throw new IOException("O caminho da pasta de backup ficou muito longo.", ex);
            }
            catch (Exception ex)
            {
                _logger.Log(string.Format(
                    "Erro inesperado ao montar o caminho da pasta de backup. Exception: {0}", ex));

                throw new IOException("Erro ao montar o caminho da pasta de backup.", ex);
            }
        }

        /// <summary>
        /// Garante que a pasta de backup exista fisicamente.
        /// </summary>
        private void EnsureBackupFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                _logger.Log("Erro: o caminho da pasta de backup está nulo ou vazio.");
                throw new ArgumentException("O caminho da pasta de backup não pode ser nulo ou vazio.", "folder");
            }

            try
            {
                if (!Directory.Exists(folder))
                {
                    _logger.Log(string.Format(
                        "Criando pasta de backup: {0}", folder));
                }

                Directory.CreateDirectory(folder);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log(string.Format(
                    "Erro: sem permissão para criar a pasta de backup. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "Sem permissão para criar a pasta de backup. Execute o aplicativo com mais privilégios ou escolha outra pasta.",
                    ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                _logger.Log(string.Format(
                    "Erro: parte do caminho da pasta de backup não foi encontrada. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "Parte do caminho da pasta de backup não foi encontrada. Verifique se o caminho base existe.",
                    ex);
            }
            catch (PathTooLongException ex)
            {
                _logger.Log(string.Format(
                    "Erro: o caminho da pasta de backup é muito longo. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "O caminho da pasta de backup é muito longo. Tente usar um caminho base mais curto.",
                    ex);
            }
            catch (NotSupportedException ex)
            {
                _logger.Log(string.Format(
                    "Erro: o caminho da pasta de backup possui um formato inválido. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "O caminho da pasta de backup possui um formato inválido.",
                    ex);
            }
            catch (IOException ex)
            {
                _logger.Log(string.Format(
                    "Erro de E/S ao criar a pasta de backup. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "Erro de E/S ao criar a pasta de backup.",
                    ex);
            }
            catch (Exception ex)
            {
                _logger.Log(string.Format(
                    "Erro inesperado ao criar a pasta de backup. Exception: {0}", ex));

                throw new InvalidOperationException(
                    "Erro inesperado ao criar a pasta de backup.",
                    ex);
            }
        }
    }
}
