using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupUtilSoftcom
{
    public interface IBackupLogger
    {
        void Log(string message);
    }
}
