using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIL_Lab_Test.Helper
{
    public  static class AppHelper
    {
        public static bool CheckIfDate(string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey) && searchKey.Contains('/'))
            {
                return true;
            }
            return false;
        }
        public static DateTime? GetDate(string searchKey)
        {
            var result = DateTime.TryParse(searchKey, out DateTime dateTime);
            if (result)
                return dateTime;
            return null;
        }
    }
}
