using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyar.Business.Utilities
{
    public static class Helper
    {
        public static bool Delete(params string[] folders)
        {
            string filePath=string.Empty;
            foreach (var folder in folders)
            {
                filePath = Path.Combine(filePath,folder);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
