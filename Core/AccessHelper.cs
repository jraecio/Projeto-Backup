using System;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace BackUtilsoftcom.Core
{
    public class AccessHelper
    {
        private readonly ILogger _logger;
        private  string _mdbPath;

        public AccessHelper(ILogger logger, string mdbPath)
        {
            _logger = logger;
            _mdbPath = mdbPath;
        }

        public InfoSeguranca LerDadosSeguranca()
        {
            try
            {
                _logger.Log($"📄 Lendo arquivo MDB em: {_mdbPath}");

                if (string.IsNullOrWhiteSpace(_mdbPath))
                    throw new ArgumentException("O caminho do MDB não foi informado.");

                if (!File.Exists(_mdbPath))
                    throw new FileNotFoundException("Arquivo MDB não encontrado.", _mdbPath);

                string connectionString =
                    $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={_mdbPath};";

                _logger.Log("🔌 Conectando ao banco Access...");

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM seguranca", conn))
                {
                    conn.Open();
                    _logger.Log("✅ Conexão estabelecida.");

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            _logger.Log("⚠ A tabela 'seguranca' está vazia.");
                            return null;
                        }

                        reader.Read();

                        _logger.Log("📥 Registro encontrado. Lendo dados...");

                        var info = new InfoSeguranca
                        {
                            SqlDriver = reader["SqlDriver"]?.ToString(),
                            SqlServidor = reader["SqlServidor"]?.ToString(),
                            SqlPorta = reader["SqlPorta"]?.ToString(),
                            SqlBase = reader["SqlBase"]?.ToString(),
                            SqlLogin = reader["SqlLogin"]?.ToString(),
                            SqlSenha = reader["SqlSenha"]?.ToString()
                        };

                        _logger.Log("🔎 Dados carregados:");
                        _logger.Log($"Driver:   {info.SqlDriver}");
                        _logger.Log($"Servidor: {info.SqlServidor}");
                        _logger.Log($"Porta:    {info.SqlPorta}");
                        _logger.Log($"Banco:    {info.SqlBase}");
                        _logger.Log($"Login:    {info.SqlLogin}");
                        _logger.Log("✅ Dados lidos com sucesso.");

                        return info;
                    }
                }
            }
            catch (OleDbException ex)
            {
                _logger.Log($"❌ ERRO OLEDB: {ex.Message}");
                return null;
            }
            catch (FileNotFoundException ex)
            {
                _logger.Log($"❌ Arquivo não encontrado: {ex.FileName}");
                return null;
            }
            catch (ArgumentException ex)
            {
                _logger.Log($"❌ Caminho inválido: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.Log($"❌ ERRO inesperado ao ler MDB: {ex.Message}");
                return null;
            }
        }

        // ----------------------------------------------------------
        //  COMPACTAR / REPARAR MDB
        // ----------------------------------------------------------
        public void CompactarRepararAccess()
        {
            //try
            //{
            //    _logger.Log("🔧 Iniciando processo de compactação do MDB...");

            //    // 1. Validação
            //    if (string.IsNullOrWhiteSpace(_mdbPath))
            //        throw new ArgumentException("O caminho do arquivo MDB não foi definido.");

            //    if (!File.Exists(_mdbPath))
            //        throw new FileNotFoundException("Arquivo MDB não encontrado.", _mdbPath);

            //    // 2. Verifica JRO
            //    Type jroType = Type.GetTypeFromProgID("JRO.JetEngine");
            //    if (jroType == null)
            //        throw new Exception("O componente JRO não está instalado.");

            //    _logger.Log("🔍 JRO encontrado. Preparando compactação...");

            //    dynamic jro = Activator.CreateInstance(jroType);

            //    string caminhoCompactado = _mdbPath + "_compactado.mdb";

            //    if (File.Exists(caminhoCompactado))
            //    {
            //        try { File.Delete(caminhoCompactado); }
            //        catch (Exception ex)
            //        {
            //            throw new IOException("Não foi possível remover o arquivo compactado antigo.", ex);
            //        }
            //    }

            //    string connOrigem = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={_mdbPath};";
            //    string connDestino = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={caminhoCompactado};";

            //    _logger.Log("📦 Compactando banco...");

            //    jro.CompactDatabase(connOrigem, connDestino);
            //    _mdbPath = caminhoCompactado;

            //    if (!File.Exists(caminhoCompactado))
            //        throw new Exception("Falha ao compactar o MDB: arquivo não foi criado.");

            //    _logger.Log("📁 Substituindo arquivo original...");

            //    try
            //    {
            //        File.Delete(_mdbPath);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new IOException("Não foi possível excluir o MDB original.", ex);
            //    }

            //    File.Move(caminhoCompactado, _mdbPath);

            //    _logger.Log("✅ Compactação e reparação concluídas com sucesso!");
            //}
            //catch (COMException ex)
            //{
            //    _logger.Log($"❌ Erro COM / JRO: {ex.Message}");
            //    throw;
            //}
            //catch (FileNotFoundException ex)
            //{
            //    _logger.Log($"❌ MDB não encontrado: {ex.FileName}");
            //    throw;
            //}
            //catch (IOException ex)
            //{
            //    _logger.Log($"❌ ERRO ao manipular arquivos: {ex.Message}");
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    _logger.Log($"❌ ERRO inesperado ao compactar o MDB: {ex.Message}");
            //    throw;
            //}
        }
        public string CopiarArquivoMdbParaBackup()
        {
            try
            {
                _logger.Log("📁 Iniciando cópia do arquivo MDB para a pasta de backup...");

                if (string.IsNullOrWhiteSpace(_mdbPath))
                    throw new ArgumentException("O caminho do arquivo MDB não foi informado.");

                if (!File.Exists(_mdbPath))
                    throw new FileNotFoundException("Arquivo MDB não encontrado.", _mdbPath);

                // Pasta destino
                string pastaBackup = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "BackUtilsoftcom");

                // Garante que a pasta existe
                if (!Directory.Exists(pastaBackup))
                {
                    _logger.Log("📂 Criando pasta de backup...");
                    Directory.CreateDirectory(pastaBackup);
                }

                // Nome final do arquivo com timestamp para evitar sobrescrever
                string nomeArquivo = Path.GetFileNameWithoutExtension(_mdbPath)
                                     + "_" + DateTime.Now.ToString("ddMMyy")
                                     + ".mdb";

                string destino = Path.Combine(pastaBackup, nomeArquivo);

                // Se o arquivo já existir, excluir antes
                if (File.Exists(destino))
                {
                    _logger.Log("⚠️ Arquivo já existe no destino. Removendo antes de copiar...");
                    File.Delete(destino);
                }

                // Copia o arquivo
                File.Copy(_mdbPath, destino, overwrite: false);

                _logger.Log($"✅ Arquivo copiado com sucesso para: {destino}");

                return destino;
            }
            catch (FileNotFoundException ex)
            {
                _logger.Log($"❌ Arquivo MDB não encontrado: {ex.FileName}");
                return null;
            }
            catch (IOException ex)
            {
                _logger.Log($"❌ Erro ao copiar arquivo: {ex.Message}");
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.Log($"❌ Acesso negado ao copiar o arquivo: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                _logger.Log($"❌ Erro inesperado ao copiar MDB: {ex.Message}");
                return null;
            }
        }

    }
}
