using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static ANtivirus.DbContext;

namespace ANtivirus
{

    public class ScanHelper
    {
        public enum ScanTypes
        {
            Single,
            Mutil
        }

        public static bool Scan(string read, ScanTypes typeScan, Dictionary<string, MalwareTypes> signatures)
        {
            var isMalware = false;
            switch (typeScan)
            {
                case ScanTypes.Single:
                    isMalware = ScanSingle(read, signatures);
                    break;
                case ScanTypes.Mutil:
                    isMalware = ScanMutil(read, signatures);
                    break;
                default:
                    break;
            }

            return isMalware;
            
        }

        private static bool ScanSingle(string read, Dictionary<string, MalwareTypes> signatures)
        {
            foreach (var signature in signatures)
            {
                if (read.Contains(signature.Key))
                {
                    return true;
                }

            }
            return false;
        }

        private static bool ScanMutil(string read, Dictionary<string, MalwareTypes> signatures)
        {
            bool isMalware = false;
            ParallelLoopResult result = Parallel.ForEach(signatures, signature =>
            {
                if (read.Contains(signature.Key))
                {

                    isMalware = true;
                }
            });
            return isMalware;
        }
        
       
    }
}
