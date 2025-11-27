using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackUtilsoftcom.Core
{
    public class CloudflareConfig
    {
        public string Endpoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Bucket { get; set; }
        public string ObjectKey { get; set; }
        public string FilePath { get; set; }
    }
}
